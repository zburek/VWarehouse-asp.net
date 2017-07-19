using System;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Service.Common.Inventory;
using Model.Common.Inventory;
using Model.Inventory;
using MVC.Models.AssignViewModels;
using Service.Common;
using AutoMapper;
using Model.Common;
using System.Collections.Generic;
using MVC.Models.MeasuringDeviceViewModels;
using PagedList;
using Common.Parameters;

namespace MVC.Controllers
{
    public class MeasuringDeviceController : Controller
    {
        protected IMeasuringDeviceService Service;
        protected IEmployeeService EmployeeService;
        protected IMeasuringDeviceParameters measuringDeviceParameters;
        protected IEmployeeParameters employeeParameters;
        public MeasuringDeviceController(IMeasuringDeviceService service, IEmployeeService employeeService, IMeasuringDeviceParameters measuringDeviceParameters, IEmployeeParameters employeeParameters)
        {
            this.Service = service;
            this.EmployeeService = employeeService;
            this.measuringDeviceParameters = measuringDeviceParameters;
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
            measuringDeviceParameters.PageSize = 5;
            measuringDeviceParameters.PageNumber = (page ?? 1);
            measuringDeviceParameters.SearchString = searchString;
            measuringDeviceParameters.SortOrder = sortOrder;
            measuringDeviceParameters.Paged = true;

            var measuringDevicePagedList = await Service.GetAllPagedListAsync(measuringDeviceParameters);
            var viewModel = Mapper.Map<IEnumerable<MeasuringDeviceIndexViewModel>>(measuringDevicePagedList);
            var pagedViewModel = new StaticPagedList<MeasuringDeviceIndexViewModel>(viewModel, measuringDevicePagedList.GetMetaData());
            return View(pagedViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
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
            measuringDeviceParameters.PageSize = 5;
            measuringDeviceParameters.PageNumber = (page ?? 1);
            measuringDeviceParameters.SearchString = searchString;
            measuringDeviceParameters.SortOrder = sortOrder;
            measuringDeviceParameters.Paged = true;
            measuringDeviceParameters.OnStock = true;

            var measuringDevicePagedList = await Service.GetAllPagedListAsync(measuringDeviceParameters);
            var viewModel = Mapper.Map<IEnumerable<MeasuringDeviceOnStockViewModel>>(measuringDevicePagedList);
            var pagedViewModel = new StaticPagedList<MeasuringDeviceOnStockViewModel>(viewModel, measuringDevicePagedList.GetMetaData());
            return View(pagedViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var measuringDeviceViewModel = Mapper.Map<MeasuringDeviceDetailsViewModel>(await Service.GetByIdAsync(ID));
            if (measuringDeviceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(measuringDeviceViewModel);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,SerialNumber,CalibrationExpirationDate,EmployeeID")] MeasuringDeviceCreateViewModel createdMeasuringDevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IMeasuringDevice measuringDevice = (Mapper.Map<MeasuringDevice>(createdMeasuringDevice));
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
            var measuringDeviceViewModel = Mapper.Map<MeasuringDeviceEditViewModel>(await Service.GetByIdAsync(ID));
            if (measuringDeviceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(measuringDeviceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,SerialNumber,CalibrationExpirationDate,EmployeeID")] MeasuringDeviceEditViewModel editedMeasuringDevice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IMeasuringDevice measuringDevice = (Mapper.Map<MeasuringDevice>(editedMeasuringDevice));
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
            var measuringDeviceViewModel = Mapper.Map<MeasuringDeviceDeleteViewModel>(await Service.GetByIdAsync(ID));
            if (measuringDeviceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(measuringDeviceViewModel);
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
