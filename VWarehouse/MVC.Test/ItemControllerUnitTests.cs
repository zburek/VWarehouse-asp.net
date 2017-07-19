using MVC.Controllers;
using NUnit.Framework;
using Service.Common.Inventory;
using Model.Common.Inventory;
using System.Collections.Generic;
using Moq;
using Service.Common;
using PagedList;
using Model.Inventory;
using System;
using AutoMapper;
using MVC.Models.ItemViewModels;
using Common.Parameters;

namespace MVC.Tests
{
    [TestFixture] // Tells NUnit there are tests in this class
    public class ItemControllerUnitTests
    {
        [Test]
        public void Item_Index_View_Contains_List_Of_Items_Model()
        {
            // Arrange
            Mock<IItemService> mockItemService = new Mock<IItemService>();
            Mock<IEmployeeService> mockEmployeeService = new Mock<IEmployeeService>();
            Mock<IItemParameters> mockitemParameters = new Mock<IItemParameters>();
            Mock<IEmployeeParameters> mockEmployeeParameters = new Mock<IEmployeeParameters>();

            IItemParameters parameters = new ItemParameters
            {
                PageSize = 5,
                PageNumber = 1
            };
            IEmployeeParameters employeeParameters = new EmployeeParameters();
            int count = 3;
            List<IItem> itemList = new List<IItem>
            {
                new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Green", SerialNumber = "111", EmployeeID = null },
                new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Black", SerialNumber = "222", EmployeeID = null },
                new Item { ID = Guid.NewGuid(), Name = "Phone", Description = "Samsung", SerialNumber = "333", EmployeeID = null }
            };

            mockItemService.Setup(m => m.GetAllPagedListAsync(parameters)).ReturnsAsync(new StaticPagedList<IItem>(itemList, parameters.PageNumber.Value, parameters.PageSize.Value, count));
            ItemController controller = new ItemController(mockItemService.Object, mockEmployeeService.Object, mockitemParameters.Object, mockEmployeeParameters.Object);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IItem, ItemIndexViewModel>();
            });

            // Act
            var actual = (StaticPagedList<IItem>)controller.Index(null, null, null, null).Result.Model;


            // Assert
            Assert.IsInstanceOf<StaticPagedList<IItem>>(actual);
        }
    }
}
