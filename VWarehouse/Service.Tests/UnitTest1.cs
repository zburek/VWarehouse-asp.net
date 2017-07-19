
using NUnit.Framework;
using Model.Common.Inventory;
using System.Collections.Generic;
using Moq;
using DAL.DbEntities.Inventory;
using DAL.DbEntities;
using PagedList;
using System;
using AutoMapper;
using Service.Inventory;
using Model.Common;
using Common.Parameters;
using Repository.Common.Inventory;
using Model.Inventory;

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
                Mock<IItemRepository> mockItemRepository = new Mock<IItemRepository>();


                IItemParameters parameters = new ItemParameters()
                {
                    PageSize = 5,
                    PageNumber = 1
                };

                IEnumerable<Item> itemList = new List<Item>
                {
                    new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Green", SerialNumber = "111", EmployeeID = Guid.NewGuid() },
                    new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Black", SerialNumber = "222", EmployeeID = Guid.NewGuid() },
                    new Item { ID = Guid.NewGuid(), Name = "Phone", Description = "Samsung", SerialNumber = "333", EmployeeID = Guid.NewGuid() }
                };

                mockItemRepository.Setup(m => m.GetAllAsync(parameters)).ReturnsAsync(itemList);
                ItemService service = new ItemService(mockItemRepository.Object);
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
