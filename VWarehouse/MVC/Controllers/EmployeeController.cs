using System;
using System.Data;
using System.Net;
using System.Web.Mvc;
using Model;
using System.Threading.Tasks;
using Model.Common;
using Service.Common;
using Service.Common.Inventory;
using DAL.DbEntities;
using Common;
using MVC.Models.EmployeeViewModels;
using AutoMapper;
using PagedList;
using System.Collections.Generic;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService Service;
        protected IItemService ItemService;
        protected IMeasuringDeviceService MeasuringDeviceService;
        protected IVehicleService VehicleService;
        protected IParameters<EmployeeEntity> parameters;
        public EmployeeController(IEmployeeService service, IItemService itemService, IMeasuringDeviceService measuringDeviceService, IVehicleService vehicleService, IParameters<EmployeeEntity> parameters)
        { 
            this.Service = service;
            this.ItemService = itemService;
            this.MeasuringDeviceService = measuringDeviceService;
            this.VehicleService = vehicleService;
            this.parameters = parameters;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult> Index(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            parameters.PageSize = 5;
            parameters.PageNumber = (page ?? 1);
            parameters.SearchString = searchString;
            parameters.SortOrder = sortOrder;

            var employeePagedList = await Service.GetAllPagedListAsync(parameters);
            var viewModel = Mapper.Map<IEnumerable<EmployeeIndexViewModel>>(employeePagedList);
            var pagedViewModel = new StaticPagedList<EmployeeIndexViewModel>(viewModel, employeePagedList.GetMetaData());
            return View(pagedViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeViewModel = Mapper.Map<EmployeeDetailsViewModel>(await Service.GetByIdAsync(ID));
            if (employeeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Inventory(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parameters.Filter = e => e.ID == ID;
            parameters.IncludeProperties = "Items, MeasuringDevices, Vehicles";
            var employeeViewModel = Mapper.Map<EmployeeInventoryViewModel>(await Service.GetOneAsync(parameters));
            if (employeeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Inventory return
        [HttpGet]
        public async Task<ActionResult> ReturnOneItem(Guid? itemID, Guid? empID)
        {
            if (itemID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await ItemService.ReturnOneItemAsync(itemID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllItems(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await ItemService.ReturnAllItemsAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneMeasuringDevice(Guid? MDeviceID, Guid? empID)
        {
            if (MDeviceID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await MeasuringDeviceService.ReturnOneMeasuringDeviceAsync(MDeviceID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllMeasuringDevices(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await MeasuringDeviceService.ReturnAllMeasuringDevicesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneVehicle(Guid? vhID, Guid? empID)
        {
            if (vhID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await VehicleService.ReturnOneVehicleAsync(vhID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllVehicles(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await VehicleService.ReturnAllVehiclesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllInventory(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await ItemService.ReturnAllItemsAsync(empID);
            await MeasuringDeviceService.ReturnAllMeasuringDevicesAsync(empID);
            await VehicleService.ReturnAllVehiclesAsync(empID);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,PhoneNumber")] EmployeeCreateViewModel createdEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = (Mapper.Map<Employee>(createdEmployee));
                    await Service.CreateAsync(employee);
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
        public async Task<ActionResult> Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeViewModel = Mapper.Map<EmployeeEditViewModel>(await Service.GetByIdAsync(ID));
            if (employeeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,PhoneNumber")] EmployeeEditViewModel editedEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = (Mapper.Map<Employee>(editedEmployee));
                    await Service.UpdateAsync(employee);
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
        public async Task<ActionResult> Delete(Guid? ID, bool? saveChangesError = false)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete faild. Try again and if the problem persists see your system administrator.";
            }
            var employeeViewModel = Mapper.Map<EmployeeDeleteViewModel>(await Service.GetByIdAsync(ID));
            if (employeeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid ID)
        {
            try
            {
                await ItemService.ReturnAllItemsAsync(ID);
                await MeasuringDeviceService.ReturnAllMeasuringDevicesAsync(ID);
                await VehicleService.ReturnAllVehiclesAsync(ID);
                await Service.DeleteAsync(ID);
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
