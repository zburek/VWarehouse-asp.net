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
    public class MeasuringDeviceController : Controller
    {
        protected IMeasuringDeviceService Service;
        protected IEmployeeService EmployeeService;
        protected IParameters<MeasuringDeviceEntity> parameters;
        protected IParameters<EmployeeEntity> employeeParameters;
        public MeasuringDeviceController(IMeasuringDeviceService service, IEmployeeService employeeService, IParameters<MeasuringDeviceEntity> parameters, IParameters<EmployeeEntity> employeeParameters)
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
            ViewBag.CalibrationExpirationDateSortParm = sortOrder == "CalibrationExpirationDate" ? "calibrationExpirationDate_desc" : "CalibrationExpirationDate";
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

            var measuringDevicePagedList = await Service.GetAllPagedListAsync(parameters);
            return View(measuringDevicePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            parameters.Filter = i => i.EmployeeID == null;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CalibrationExpirationDateSortParm = sortOrder == "CalibrationExpirationDate" ? "calibrationExpirationDate_desc" : "CalibrationExpirationDate";
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

            var measuringDevicePagedList = await Service.GetAllPagedListAsync(parameters);
            return View(measuringDevicePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMeasuringDevice measuringDevice = await Service.GetByIdAsync(ID);
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
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
            IAssignViewModel measuringDevice = Mapper.Map<IAssignViewModel>(await Service.GetByIdAsync(ID));
            measuringDevice.EmployeeList = Mapper.Map<List<IEmployee>>(await EmployeeService.GetAllAsync(employeeParameters));
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign([Bind(Include = "ID,Name,EmployeeID")] AssignViewModel assignedMeasuringDevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid itemID = assignedMeasuringDevice.ID;
                    Guid? employeeID = assignedMeasuringDevice.EmployeeID;
                    await Service.AssignMeasuringDeviceAsync(itemID, employeeID);
                    return RedirectToAction("OnStock");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(assignedMeasuringDevice);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,SerialNumber,CalibrationExpirationDate,EmployeeID")] MeasuringDevice createdMeasuringDevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IMeasuringDevice measuringDevice = createdMeasuringDevice; // Not sure this is allowed, use automapper for new?
                    await Service.CreateAsync(measuringDevice);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(createdMeasuringDevice);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMeasuringDevice measuringDevice = await Service.GetByIdAsync(ID);
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,SerialNumber,CalibrationExpirationDate,EmployeeID")] MeasuringDevice editedMeasuringDevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IMeasuringDevice measuringDevice = editedMeasuringDevice; // Not sure this is allowed, use automapper for new?
                    await Service.UpdateAsync(measuringDevice);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editedMeasuringDevice);
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
            IMeasuringDevice measuringDevice = await Service.GetByIdAsync(ID);
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
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
