using AutoMapper;
using Common;
using Common.Parameters;
using Common.Parameters.RepositoryParameters;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Model.Common;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        protected IGenericRepository Repository { get; private set; }
        protected IGenericRepositoryParameters<AssignmentEntity> GenericParameters { get; private set; }
        public AssignmentRepository(IGenericRepository repository, IGenericRepositoryParameters<AssignmentEntity> parameters)
        {
            this.Repository = repository;
            this.GenericParameters = parameters;
        }

        #region Get
        public async Task<IAssignment> GetByIdAsync(Guid? assignmentID)
        {
            var assignment = Mapper.Map<IAssignment>
                (await Repository.GetByIdAsync<AssignmentEntity>(assignmentID));
            return assignment;
        }

        public async Task<IEnumerable<IAssignment>> GetAllAsync(IAssignmentParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<AssignmentEntity>>(parameters);
            if (GenericParameters.Paged)
            {
                switch (GenericParameters.SortOrder)
                {
                    case "Name":
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.Name);
                        break;
                    case "name_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(e => e.Name);
                        break;
                    case "StartTime":
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.StartTime);
                        break;
                    case "startTime_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(e => e.StartTime);
                        break;
                    case "EndTime":
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.EndTime);
                        break;
                    case "endTime_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(e => e.EndTime);
                        break;
                    default:
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.ID);
                        break;
                }
                GenericParameters.Skip = (GenericParameters.PageNumber - 1) * GenericParameters.PageSize;
                GenericParameters.Take = GenericParameters.PageSize;
            }

            if (!String.IsNullOrEmpty(parameters.SearchString))
            {
                GenericParameters.Filter = e => e.Name.Contains(GenericParameters.SearchString);
            }
            var assignmentList = (Mapper.Map<List<IAssignment>>(await Repository.GetAllAsync(GenericParameters)));
            return assignmentList;
        }
        
        public async Task<IAssignment> GetOneAsync(IAssignmentParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<AssignmentEntity>>(parameters);
            GenericParameters.Filter = e => e.ID == GenericParameters.ID;
            var assignment = (Mapper.Map<IAssignment>(await Repository.GetOneAsync(GenericParameters)));
            return assignment;
        }

        public Task<int> GetCountAsync(IAssignmentParameters parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString))
            {
                GenericParameters.Filter = e => e.Name.Contains(GenericParameters.SearchString);
            }
            return Repository.GetCountAsync(GenericParameters);
        }
        
        #endregion

        #region CRUD
        public async Task CreateAsync(IAssignment assignment)
        {
            var assignmentEntity = Mapper.Map<AssignmentEntity>(assignment);
            await Repository.CreateAsync(assignmentEntity);
        }

        public async Task UpdateAsync(IAssignment assignment)
        {
            var assignmentEntity = Mapper.Map<AssignmentEntity>(assignment);
            await Repository.UpdateAsync(assignmentEntity);
        }

        public async Task DeleteAsync(Guid assignmentID)
        {
            IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
            await unitOfWork.DeleteAsync<AssignmentEntity>(assignmentID);
            await unitOfWork.SaveAsync();
        }
        #endregion

        #region Return
        public async Task ReturnOneEmployeeAsync(Guid? empID, Guid? assignmentID)
        {
            this.GenericParameters.Filter = assignment => assignment.ID == assignmentID;
            this.GenericParameters.IncludeProperties = "Employees";
            var assignmentEntity = await Repository.GetOneAsync(GenericParameters);
            var employeeEntity = await Repository.GetByIdAsync<EmployeeEntity>(empID);
            assignmentEntity.Employees.Remove(employeeEntity);
            await Repository.UpdateAsync(assignmentEntity);
        }

        public async Task ReturnAllEmployeesAsync(Guid? assignmentID)
        {
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            await unitOfWork.ReturnAllEmployeesAsync(assignmentID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneItemAsync(Guid? itemID, Guid? assignmentID)
        {
            this.GenericParameters.Filter = assignment => assignment.ID == assignmentID;
            this.GenericParameters.IncludeProperties = "Items";
            var assignmentEntity = await Repository.GetOneAsync(GenericParameters);
            var itemEntity = await Repository.GetByIdAsync<ItemEntity>(itemID);
            assignmentEntity.Items.Remove(itemEntity);
            await Repository.UpdateAsync(assignmentEntity);
        }

        public async Task ReturnAllItemsAsync(Guid? assignmentID)
        {
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            await unitOfWork.ReturnAllItemsAsync(assignmentID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneMeasuringDeviceAsync(Guid? mdID, Guid? assignmentID)
        {
            this.GenericParameters.Filter = assignment => assignment.ID == assignmentID;
            this.GenericParameters.IncludeProperties = "MeasuringDevices";
            var assignmentEntity = await Repository.GetOneAsync(GenericParameters);
            var measuringDeviceEntity = await Repository.GetByIdAsync<MeasuringDeviceEntity>(mdID);
            assignmentEntity.MeasuringDevices.Remove(measuringDeviceEntity);
            await Repository.UpdateAsync(assignmentEntity);
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? assignmentID)
        {
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            await unitOfWork.ReturnAllMeasuringDevicesAsync(assignmentID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneVehicleAsync(Guid? vehicleID, Guid? assignmentID)
        {
            this.GenericParameters.Filter = assignment => assignment.ID == assignmentID;
            this.GenericParameters.IncludeProperties = "MeasuringDevices";
            var assignmentEntity = await Repository.GetOneAsync(GenericParameters);
            var vehicleEntity = await Repository.GetByIdAsync<VehicleEntity>(vehicleID);
            assignmentEntity.Vehicles.Remove(vehicleEntity);
            await Repository.UpdateAsync(assignmentEntity);
        }

        public async Task ReturnAllVehiclesAsync(Guid? assignmentID)
        {
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            await unitOfWork.ReturnAllVehiclesAsync(assignmentID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAll(Guid? assignmentID)
        {
            try
            {
                IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
                await unitOfWork.ReturnAll(assignmentID);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Assign

        public async Task<IEnumerable<IBaseEntity>> GetEmployeesAsync(IAssignmentParameters assignmentParameters)
        {       
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            return await unitOfWork.GetAllEmployeesAsync(assignmentParameters);
        }
        /*
        public async Task AssignOneMeasuringDeviceAsync(Guid measuringDeviceID, Guid? employeeID)
        {
            IUnitOfWorkAssignment unitOfWork = Repository.CreateUnitOfWorkAssignment();
            await unitOfWork.ReturnAllEmployeesAsync(assignmentID);
            await unitOfWork.SaveAsync();


            var measuringDeviceEntity = await Repository.GetByIdAsync<MeasuringDeviceEntity>(measuringDeviceID);
            measuringDeviceEntity.EmployeeID = employeeID;
            await Repository.UpdateAsync(measuringDeviceEntity);
        }
        */
        #endregion
    }
}
