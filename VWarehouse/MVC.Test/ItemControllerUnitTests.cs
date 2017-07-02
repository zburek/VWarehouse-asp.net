using MVC.Controllers;
using NUnit.Framework;
using Service.Common.Inventory;
using Model.Common.Inventory;
using System.Collections.Generic;
using Moq;
using Common;
using DAL.DbEntities.Inventory;
using DAL.DbEntities;
using Service.Common;
using PagedList;
using Model.Inventory;
using System;
using AutoMapper;
using MVC.Models.ItemViewModels;

namespace MVC.Tests
{
    //[TestFixture] // Tells NUnit there are tests in this class
    //public class ItemControllerUnitTests
    //{
    //    [Test]
    //    public void Item_Index_View_Contains_List_Of_Items_Model()
    //    {
    //        // Arrange
    //        Mock<IItemService> mockItemService = new Mock<IItemService>();
    //        Mock<IEmployeeService> mockEmployeeService = new Mock<IEmployeeService>();
    //        Mock<IParameters<ItemEntity>> itemParameters = new Mock<IParameters<ItemEntity>>();
    //        Mock<IParameters<EmployeeEntity>> mockEmployeeParameters = new Mock<IParameters<EmployeeEntity>>();

    //        IParameters<ItemEntity> parameters = new Parameters<ItemEntity>
    //        {
    //            PageSize = 5,
    //            PageNumber = 1
    //        };
    //        int count = 3;
    //        List<IItem> itemList = new List<IItem>
    //        {
    //            new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Green", SerialNumber = "111", EmployeeID = null },
    //            new Item { ID = Guid.NewGuid(), Name = "Hammer", Description = "Black", SerialNumber = "222", EmployeeID = null },
    //            new Item { ID = Guid.NewGuid(), Name = "Phone", Description = "Samsung", SerialNumber = "333", EmployeeID = null }
    //        };

    //        mockItemService.Setup(m => m.GetAllPagedListAsync(parameters)).ReturnsAsync(new StaticPagedList<IItem>(itemList, parameters.PageNumber.Value, parameters.PageSize.Value, count));
    //        ItemController controller = new ItemController(mockItemService.Object, mockEmployeeService.Object, itemParameters.Object, mockEmployeeParameters.Object);
    //        Mapper.Initialize(cfg =>
    //        {
    //            cfg.CreateMap<IItem, ItemIndexViewModel>();
    //        });

    //        // Act
    //        var actual = (StaticPagedList<IItem>)controller.Index(null, null, null, null).Result.Model;


    //        // Assert
    //        Assert.IsInstanceOf<StaticPagedList<IItem>>(actual);

    //    }
    //}
}
