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
            kernel.Bind<VWarehouseContext>().ToSelf().InRequestScope();
            kernel.Bind<UnitOfWork>().ToSelf().InRequestScope();

            kernel.Bind<IEmployeeService>().To<EmployeeService>().InRequestScope();

            kernel.Bind<IItemService>().To<ItemService>().InRequestScope();
            kernel.Bind<IMeasuringDeviceService>().To<MeasuringDeviceService>().InRequestScope();
            kernel.Bind<IVehicleService>().To<VehicleService>().InRequestScope();

            kernel.Bind<IParameters<EmployeeEntity>>().To<Parameters<EmployeeEntity>>().InRequestScope();
            kernel.Bind<IParameters<ItemEntity>>().To<Parameters<ItemEntity>>().InRequestScope();
            kernel.Bind<IParameters<MeasuringDeviceEntity>>().To<Parameters<MeasuringDeviceEntity>>().InRequestScope();
            kernel.Bind<IParameters<VehicleEntity>>().To<Parameters<VehicleEntity>>().InRequestScope();

            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>().InRequestScope();
            kernel.Bind<IItemRepository>().To<ItemRepository>().InRequestScope();
            kernel.Bind<IMeasuringDeviceRepository>().To<MeasuringDeviceRepository>().InRequestScope();
            kernel.Bind<IVehicleRepository>().To<VehicleRepository>().InRequestScope();

            kernel.Bind<IGenericRepository<EmployeeEntity>>().To<GenericRepository<EmployeeEntity>>().InRequestScope();
            kernel.Bind<IGenericRepository<ItemEntity>>().To<GenericRepository<ItemEntity>>().InRequestScope();
            kernel.Bind<IGenericRepository<MeasuringDeviceEntity>>().To<GenericRepository<MeasuringDeviceEntity>>().InRequestScope();
            kernel.Bind<IGenericRepository<VehicleEntity>>().To<GenericRepository<VehicleEntity>>().InRequestScope();
        }        
    }
}
