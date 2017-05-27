using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Mvc;
using Model;
using Model.DbEntities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Common;
using Service.Common;
using Service.Common.Inventory;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService service;
        protected IItemService itemService;
        protected IMeasuringDeviceService measuringDeviceService;
        protected IVehicleService vehicleService;
        public EmployeeController(IEmployeeService service, IItemService itemService, IMeasuringDeviceService measuringDeviceService, IVehicleService vehicleService)
        { 
            this.service = service;
            this.itemService = itemService;
            this.measuringDeviceService = measuringDeviceService;
            this.vehicleService = vehicleService;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<IEmployee> employee = await service.GetAllAsync(null, null, null);
            return View(employee);
        }
        
        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<ActionResult> Inventory(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expression<Func<EmployeeEntity, bool>> filter = e => e.ID == ID;
            string includeProperties = "Items, MeasuringDevices, Vehicles";
            IEmployee employee = await service.GetOneAsync(filter, includeProperties);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        #endregion

        #region Inventory return
        [HttpGet]
        public async Task<ActionResult> ReturnOneItem(int? itemID, int? empID)
        {
            if (itemID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await itemService.ReturnOneItemAsync(itemID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllItems(int? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await itemService.ReturnAllItemsAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneMeasuringDevice(int? MDeviceID, int? empID)
        {
            if (MDeviceID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await measuringDeviceService.ReturnOneMeasuringDeviceAsync(MDeviceID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllMeasuringDevices(int? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await measuringDeviceService.ReturnAllMeasuringDevicesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneVehicle(int? vhID, int? empID)
        {
            if (vhID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await vehicleService.ReturnOneVehicleAsync(vhID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllVehicles(int? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await vehicleService.ReturnAllVehiclesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllInventory(int? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await itemService.ReturnAllItemsAsync(empID);
            await measuringDeviceService.ReturnAllMeasuringDevicesAsync(empID);
            await vehicleService.ReturnAllVehiclesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }
        #endregion

        #region CRUD

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,PhoneNumber")] Employee createdEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = createdEmployee; // Not sure this is allowed, use automapper for new employee?
                    await service.CreateAsync(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(createdEmployee);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,PhoneNumber")] Employee editedEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = editedEmployee; // Not sure this is allowed, use automapper for new employee?
                    await service.UpdateAsync(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editedEmployee);
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(int? ID, bool? saveChangesError = false)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete faild. Try again and if the problem persists see your system administrator.";
            }
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ID)
        {
            try
            {
                await itemService.ReturnAllItemsAsync(ID);
                await measuringDeviceService.ReturnAllMeasuringDevicesAsync(ID);
                await vehicleService.ReturnAllVehiclesAsync(ID);
                await service.DeleteAsync(ID);
                // Not using unit of work properly? this will make 4 different chunks of changes in DataBase. (not using all succeede or all fail)
                // Save will be used 4 times
                // To change Returning should be made from EmployeeService for all Inventory, so that one unit of work is used
                // But then when adding new tables into Inventory code should be added also to the EmployeeService, instead of only adding newTableService
                // All of this aplies to the ReturnAllInventory method also
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { ID = ID, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
