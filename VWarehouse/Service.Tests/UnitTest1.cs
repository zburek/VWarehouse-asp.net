
using NUnit.Framework;
using Model.Common.Inventory;
using System.Collections.Generic;
using Moq;
using Common;
using DAL.DbEntities.Inventory;
using DAL.DbEntities;
using PagedList;
using System;
using AutoMapper;
using Repository.Common;
using Service.Inventory;
using Model.Common;

namespace Service.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestFixture] 
        public class ItemServiceUnitTests
        {
            [Test]
            public void Item_GetAllPagedList_Returns_PagedList()
            {
                // Arrange
                Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();


                IParameters<ItemEntity> parameters = new Parameters<ItemEntity>
                {
                    PageSize = 5,
                    PageNumber = 1
                };

                IEnumerable<ItemEntity> itemList = new List<ItemEntity>
                {
                    new ItemEntity { ID = Guid.NewGuid(), Name = "Hammer", Description = "Green", SerialNumber = "111", EmployeeID = Guid.NewGuid() },
                    new ItemEntity { ID = Guid.NewGuid(), Name = "Hammer", Description = "Black", SerialNumber = "222", EmployeeID = Guid.NewGuid() },
                    new ItemEntity { ID = Guid.NewGuid(), Name = "Phone", Description = "Samsung", SerialNumber = "333", EmployeeID = Guid.NewGuid() }
                };

                mockUnitOfWork.Setup(m => m.Items.GetAllAsync(parameters)).ReturnsAsync(itemList);
                ItemService service = new ItemService(mockUnitOfWork.Object);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<EmployeeEntity, IEmployee>().ReverseMap();
                    cfg.CreateMap<ItemEntity, IItem>().ReverseMap();
                });

                // Act
                var actual = service.GetAllPagedListAsync(parameters).Result;

                // Assert
                Assert.IsInstanceOf<StaticPagedList<IItem>>(actual);
            }
        }
    }
}
