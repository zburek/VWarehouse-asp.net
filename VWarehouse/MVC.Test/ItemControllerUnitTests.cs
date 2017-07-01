using MVC.Controllers;
using NUnit.Framework;
using Service.Common.Inventory;
using Model.Common.Inventory;
using System.Collections.Generic;
using Moq;
using Model.Inventory;
using System.Threading.Tasks;
using System.Web.Mvc;

//namespace MVC.Tests
//{
//    [TestFixture]
//    public class ItemControllerUnitTests
//    {
//        [Test]
//        public void ItemIndexViewContainsListOfItemsModel()
//        {
//            // Arrange
//            Mock<IItemService> mock = new Mock<IItemService>();

//            mock.Setup(m => m.GetAllAsync(null, null, null)).ReturnsAsync(new List<IItem>
//                {
//                new Item { ID = 1, Name = "Hammer", Description = "Green", SerialNumber = "111", EmployeeID = 1 },
//                new Item { ID = 2, Name = "Hammer", Description = "Black", SerialNumber = "222", EmployeeID = 1 },
//                new Item { ID = 3, Name = "Phone", Description = "Samsung", SerialNumber = "333", EmployeeID = 2 }
//                });

//            ItemController controller = new ItemController(mock.Object)
//            // Act
//            var actual = (List<IItem>)controller.Index().Model as Task<ActionResult>;


//            // Assert
//            Assert.IsInstanceOf<List<IItem>>(actual);

//        }
//    }
//}
