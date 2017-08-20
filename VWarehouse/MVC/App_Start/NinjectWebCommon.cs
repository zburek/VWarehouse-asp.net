[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVC.App_Start.NinjectWebCommon), "Stop")]

namespace MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DAL;
    using Repository;
    using Service.Common;
    using Service;
    using Service.Common.Inventory;
    using Service.Inventory;
    using DAL.DbEntities;
    using DAL.DbEntities.Inventory;
    using Repository.Common;
    using Repository.Common.Inventory;
    using Repository.Inventory;
    using Common.Parameters;
    using Common.Parameters.RepositoryParameters;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IVWarehouseContext>().To<VWarehouseContext>().InRequestScope(); 
            kernel.Bind<VWarehouseContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork> ().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>().InRequestScope();

            kernel.Bind<IEmployeeService>().To<EmployeeService>().InRequestScope();
            kernel.Bind<IItemService>().To<ItemService>().InRequestScope();
            kernel.Bind<IMeasuringDeviceService>().To<MeasuringDeviceService>().InRequestScope();
            kernel.Bind<IVehicleService>().To<VehicleService>().InRequestScope();
            kernel.Bind<IWarningService>().To<WarningService>().InRequestScope();
            kernel.Bind<IAssignmentService>().To<AssignmentService>().InRequestScope();


            #region Parameters
            kernel.Bind<IEmployeeParameters>().To<EmployeeParameters>().InRequestScope();
            kernel.Bind<IItemParameters>().To<ItemParameters>().InRequestScope();
            kernel.Bind<IMeasuringDeviceParameters>().To<MeasuringDeviceParameters>().InRequestScope();
            kernel.Bind<IVehicleParameters>().To<VehicleParameters>().InRequestScope();
            kernel.Bind<IAssignmentParameters>().To<AssignmentParameters>().InRequestScope();

            kernel.Bind<IGenericRepositoryParameters<EmployeeEntity>>().To<GenericRepositoryParameters<EmployeeEntity>>().InRequestScope();
            kernel.Bind<IGenericRepositoryParameters<ItemEntity>>().To<GenericRepositoryParameters<ItemEntity>>().InRequestScope();
            kernel.Bind<IGenericRepositoryParameters<MeasuringDeviceEntity>>().To<GenericRepositoryParameters<MeasuringDeviceEntity>>().InRequestScope();
            kernel.Bind<IGenericRepositoryParameters<VehicleEntity>>().To<GenericRepositoryParameters<VehicleEntity>>().InRequestScope();
            kernel.Bind<IGenericRepositoryParameters<AssignmentEntity>>().To<GenericRepositoryParameters<AssignmentEntity>>().InRequestScope();
            #endregion

            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>().InRequestScope();
            kernel.Bind<IItemRepository>().To<ItemRepository>().InRequestScope();
            kernel.Bind<IMeasuringDeviceRepository>().To<MeasuringDeviceRepository>().InRequestScope();
            kernel.Bind<IVehicleRepository>().To<VehicleRepository>().InRequestScope();
            kernel.Bind<IAssignmentRepository>().To<AssignmentRepository>().InRequestScope();

            kernel.Bind<IGenericRepository>().To<GenericRepository>().InRequestScope();
        }        
    }
}
