﻿using Common;
using DAL.DbEntities.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class MeasuringDeviceRepository : IMeasuringDeviceRepository
    {
        protected IGenericRepository<MeasuringDeviceEntity> Repository { get; private set; }
        public MeasuringDeviceRepository(IGenericRepository<MeasuringDeviceEntity> repository)
        {
            this.Repository = repository;
        }

        #region Get
        public async Task<IEnumerable<MeasuringDeviceEntity>> GetAllAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            return await Repository.GetAllAsync(parameters);
        }
        public async Task<MeasuringDeviceEntity> GetByIdAsync(Guid? ID)
        {
            return await Repository.GetByIdAsync(ID);
        }

        public Task<int> GetCountAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            return Repository.GetCountAsync(parameters);
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(MeasuringDeviceEntity measuringDeviceEntity)
        {
            await Repository.AddAsync(measuringDeviceEntity);
             await Repository.SaveAsync();
        }

        public async Task UpdateAsync(MeasuringDeviceEntity measuringDeviceEntity)
        {
            await Repository.UpdateAsync(measuringDeviceEntity);
            await Repository.SaveAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            await Repository.DeleteAsync(ID);
            await Repository.SaveAsync();
        }
        #endregion
    }
}

