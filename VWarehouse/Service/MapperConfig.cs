using AutoMapper;
using Model;
using Model.Common;
using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities;
using Model.DbEntities.Inventory;
using Model.Inventory;
using Model.ViewModels;

namespace Service
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
                    cfg.CreateMap<ItemEntity, IBaseInfo>().ReverseMap();
                    cfg.CreateMap<ItemEntity, IItem>().ReverseMap();
                    cfg.CreateMap<Item, IItem>().ReverseMap();

                    cfg.CreateMap<MeasuringDeviceEntity, MeasuringDevice>().ReverseMap();
                    cfg.CreateMap<MeasuringDeviceEntity, IBaseInfo>().ReverseMap();
                    cfg.CreateMap<MeasuringDeviceEntity, IMeasuringDevice>().ReverseMap();
                    cfg.CreateMap<MeasuringDevice, IMeasuringDevice>().ReverseMap();

                    cfg.CreateMap<VehicleEntity, Vehicle>().ReverseMap();
                    cfg.CreateMap<VehicleEntity, IBaseInfo>().ReverseMap();
                    cfg.CreateMap<VehicleEntity, IVehicle>().ReverseMap();
                    cfg.CreateMap<Vehicle, IVehicle>().ReverseMap();

                    cfg.CreateMap<IAssignViewModel, AssignViewModel>().ReverseMap();
                    cfg.CreateMap<IAssignViewModel, ItemEntity>().ReverseMap();
                    cfg.CreateMap<IAssignViewModel, MeasuringDeviceEntity>().ReverseMap();
                    cfg.CreateMap<IAssignViewModel, VehicleEntity>().ReverseMap();

                    cfg.CreateMap<WarningViewModel, IWarningViewModel>().ReverseMap();
                    cfg.CreateMap<IWarningViewModel, IMeasuringDevice>().ReverseMap();
                    cfg.CreateMap<IWarningViewModel, IVehicle>().ReverseMap();
                });
        }

    }
}

