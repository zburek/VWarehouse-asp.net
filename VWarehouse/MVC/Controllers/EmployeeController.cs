using System;
using System.Data;
using System.Net;
using System.Web.Mvc;
using Model;
using System.Threading.Tasks;
using Model.Common;
using Service.Common;
using MVC.Models.EmployeeViewModels;
using AutoMapper;
using PagedList;
using System.Collections.Generic;
using Common.Parameters;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService Service;
        protected IEmployeeParameters employeeParameters;
        public EmployeeController(IEmployeeService service, IEmployeeParameters employeeParameters)
        { 
            this.Service = service;
            this.employeeParameters = employeeParameters;
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
            employeeParameters.PageSize = 5;
            employeeParameters.PageNumber = (page ?? 1);
            employeeParameters.SearchString = searchString;
            employeeParameters.SortOrder = sortOrder;
            employeeParameters.Paged = true;

            var employeePagedList = await Service.GetAllPagedListAsync(employeeParameters);
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
            employeeParameters.ID = ID;
            employeeParameters.IncludeProperties = "Items, MeasuringDevices, Vehicles";
            var employeeViewModel = Mapper.Map<EmployeeInventoryViewModel>(await Service.GetOneAsync(employeeParameters));
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
            await Service.ReturnOneItemAsync(itemID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllItems(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllItemsAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneMeasuringDevice(Guid? MDeviceID, Guid? empID)
        {
            if (MDeviceID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneMeasuringDeviceAsync(MDeviceID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllMeasuringDevices(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllMeasuringDevicesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneVehicle(Guid? vhID, Guid? empID)
        {
            if (vhID == null || empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneVehicleAsync(vhID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllVehicles(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllVehiclesAsync(empID);
            return RedirectToAction("Inventory", new { ID = empID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllInventory(Guid? empID)
        {
            if (empID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllInventory(empID);
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
