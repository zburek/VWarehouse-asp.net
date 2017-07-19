using AutoMapper;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Model;
using Model.Common;
using Model.Common.Inventory;
using Model.Inventory;
using MVC.Models.EmployeeViewModels;
using MVC.Models.ItemViewModels;
using MVC.Models.AssignViewModels;
using MVC.Models.WarningViewModels;
using MVC.Models.MeasuringDeviceViewModels;
using MVC.Models.VehicleViewModels;
using Common.Parameters;
using Common.Parameters.RepositoryParameters;
using Common;

namespace MVC.Models
{
    public class MapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EmployeeEntity, Employee>().ReverseMap();
                cfg.CreateMap<EmployeeEntity, IEmployee>().ReverseMap();
                cfg.CreateMap<Employee, IEmployee>().ReverseMap();

                cfg.CreateMap<ItemEntity, Item>().ReverseMap();
                cfg.CreateMap<ItemEntity, IBaseEntity>().ReverseMap();
                cfg.CreateMap<ItemEntity, IItem>().ReverseMap();
                cfg.CreateMap<Item, IItem>().ReverseMap();

                cfg.CreateMap<MeasuringDeviceEntity, MeasuringDevice>().ReverseMap();
                cfg.CreateMap<MeasuringDeviceEntity, IBaseEntity>().ReverseMap();
                cfg.CreateMap<MeasuringDeviceEntity, IMeasuringDevice>().ReverseMap();
                cfg.CreateMap<MeasuringDevice, IMeasuringDevice>().ReverseMap();

                cfg.CreateMap<VehicleEntity, Vehicle>().ReverseMap();
                cfg.CreateMap<VehicleEntity, IBaseEntity>().ReverseMap();
                cfg.CreateMap<VehicleEntity, IVehicle>().ReverseMap();
                cfg.CreateMap<Vehicle, IVehicle>().ReverseMap();

                #region Mapping View Models
                cfg.CreateMap<IEmployee, EmployeeIndexViewModel>();
                cfg.CreateMap<IEmployee, EmployeeDetailsViewModel>();
                cfg.CreateMap<IEmployee, EmployeeInventoryViewModel>();
                cfg.CreateMap<EmployeeCreateViewModel, Employee>();
                cfg.CreateMap<IEmployee, EmployeeEditViewModel>();
                cfg.CreateMap<EmployeeEditViewModel, Employee>();
                cfg.CreateMap<IEmployee, EmployeeDeleteViewModel>();

                cfg.CreateMap<IItem, ItemIndexViewModel>();
                cfg.CreateMap<IItem, ItemOnStockViewModel>();
                cfg.CreateMap<IItem, ItemDetailsViewModel>();
                cfg.CreateMap<ItemCreateViewModel, Item>();
                cfg.CreateMap<IItem, ItemEditViewModel>();
                cfg.CreateMap<ItemEditViewModel, Item>();
                cfg.CreateMap<IItem, ItemDeleteViewModel>();

                cfg.CreateMap<IMeasuringDevice, MeasuringDeviceIndexViewModel>();
                cfg.CreateMap<IMeasuringDevice, MeasuringDeviceOnStockViewModel>();
                cfg.CreateMap<IMeasuringDevice, MeasuringDeviceDetailsViewModel>();
                cfg.CreateMap<MeasuringDeviceCreateViewModel, MeasuringDevice>();
                cfg.CreateMap<IMeasuringDevice, MeasuringDeviceEditViewModel>();
                cfg.CreateMap<MeasuringDeviceEditViewModel, MeasuringDevice>();
                cfg.CreateMap<IMeasuringDevice, MeasuringDeviceDeleteViewModel>();

                cfg.CreateMap<IVehicle, VehicleIndexViewModel>();
                cfg.CreateMap<IVehicle, VehicleOnStockViewModel>();
                cfg.CreateMap<IVehicle, VehicleDetailsViewModel>();
                cfg.CreateMap<VehicleCreateViewModel, Vehicle>();
                cfg.CreateMap<IVehicle, VehicleEditViewModel>();
                cfg.CreateMap<VehicleEditViewModel, Vehicle>();
                cfg.CreateMap<IVehicle, VehicleDeleteViewModel>();

                cfg.CreateMap<IAssignViewModel, AssignViewModel>().ReverseMap();
                cfg.CreateMap<IAssignViewModel, IItem>().ReverseMap();
                cfg.CreateMap<IAssignViewModel, IMeasuringDevice>().ReverseMap();
                cfg.CreateMap<IAssignViewModel, IVehicle>().ReverseMap();

                cfg.CreateMap<WarningViewModel, IWarningViewModel>().ReverseMap();
                cfg.CreateMap<IWarningViewModel, IMeasuringDevice>().ReverseMap();
                cfg.CreateMap<IWarningViewModel, IVehicle>().ReverseMap();
                #endregion

                #region Mapping Parameters

                cfg.CreateMap<IEmployeeParameters, IGenericRepositoryParameters<EmployeeEntity>>();
                cfg.CreateMap<IItemParameters, IGenericRepositoryParameters<ItemEntity>>();
                cfg.CreateMap<IMeasuringDeviceParameters, IGenericRepositoryParameters<MeasuringDeviceEntity>>();
                cfg.CreateMap<IVehicleParameters, IGenericRepositoryParameters<VehicleEntity>>();

                #endregion
            });
        }
    }
}