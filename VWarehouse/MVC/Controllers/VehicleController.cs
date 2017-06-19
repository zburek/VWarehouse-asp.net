using System;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Service.Common.Inventory;
using Model.Common.Inventory;
using Model.Inventory;
using MVC.Models.ViewModels;
using Service.Common;
using AutoMapper;
using Model.Common;
using System.Collections.Generic;
using DAL;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;

namespace MVC.Controllers
{
    public class VehicleController : Controller
    {
        protected IVehicleService Service;
        protected IEmployeeService EmployeeService;
        protected IParameters<VehicleEntity> parameters;
        protected IParameters<EmployeeEntity> employeeParameters;
        public VehicleController(IVehicleService service, IEmployeeService employeeService, IParameters<VehicleEntity> parameters, IParameters<EmployeeEntity> employeeParameters)
        {
            this.Service = service;
            this.EmployeeService = employeeService;
            this.parameters = parameters;
            this.employeeParameters = employeeParameters;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult> Index(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.LicenseExpirationDateSortParm = sortOrder == "LicenseExpirationDate" ? "licenseExpirationDate_desc" : "LicenseExpirationDate";
            ViewBag.MileageSortParm = sortOrder == "Mileage" ? "mileage_desc" : "Mileage";
            ViewBag.NextServiceSortParm = sortOrder == "NextService" ? "nextService_desc" : "NextService";
            ViewBag.EmployeeSortParm = sortOrder == "Employee" ? "employee_desc" : "Employee";
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

            var vehiclePagedList = await Service.GetAllPagedListAsync(parameters);
            return View(vehiclePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            parameters.Filter = v => v.EmployeeID == null;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.LicenseExpirationDateSortParm = sortOrder == "LicenseExpirationDate" ? "licenseExpirationDate_desc" : "LicenseExpirationDate";
            ViewBag.MileageSortParm = sortOrder == "Mileage" ? "mileage_desc" : "Mileage";
            ViewBag.NextServiceSortParm = sortOrder == "NextService" ? "nextService_desc" : "NextService";
            ViewBag.EmployeeSortParm = sortOrder == "Employee" ? "employee_desc" : "Employee";
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

            var vehiclePagedList = await Service.GetAllPagedListAsync(parameters);
            return View(vehiclePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicle vehicle = await Service.GetByIdAsync(ID);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }
        #endregion

        #region Assign
        [HttpGet]
        public async Task<ActionResult> Assign(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IAssignViewModel vehicle = Mapper.Map<IAssignViewModel>(await Service.GetByIdAsync(ID));
            vehicle.EmployeeList = Mapper.Map<List<IEmployee>>(await EmployeeService.GetAllAsync(employeeParameters));
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign([Bind(Include = "ID,Name,EmployeeID")] AssignViewModel assignedVehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid itemID = assignedVehicle.ID;
                    Guid? employeeID = assignedVehicle.EmployeeID;
                    await Service.AssignVehicleAsync(itemID, employeeID);
                    return RedirectToAction("OnStock");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(assignedVehicle);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,Type,LicensePlate,LicenseExpirationDate,Mileage,NextService,EmployeeID")] Vehicle createdVehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IVehicle vehicle = createdVehicle; // Not sure this is allowed, use automapper for new?
                    await Service.CreateAsync(vehicle);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(createdVehicle);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicle vehicle = await Service.GetByIdAsync(ID);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Type,LicensePlate,LicenseExpirationDate,Mileage,NextService,EmployeeID")] Vehicle editedVehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IVehicle vehicle = editedVehicle; // Not sure this is allowed, use automapper for new?
                    await Service.UpdateAsync(vehicle);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editedVehicle);
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
            IVehicle vehicle = await Service.GetByIdAsync(ID);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid ID)
        {
            try
            {
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

