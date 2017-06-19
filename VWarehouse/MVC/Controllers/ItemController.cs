using System;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Service.Common.Inventory;
using Model.Common.Inventory;
using Model.Inventory;
using MVC.Models.ViewModels;
using AutoMapper;
using Model.Common;
using Service.Common;
using System.Collections.Generic;
using DAL;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;

namespace MVC.Controllers
{
    public class ItemController : Controller
    {
        protected IItemService Service;
        protected IEmployeeService EmployeeService;
        protected IParameters<ItemEntity> parameters;
        protected IParameters<EmployeeEntity> employeeParameters;
        public ItemController(IItemService service, IEmployeeService employeeService, IParameters<ItemEntity> parameters, IParameters<EmployeeEntity> employeeParameters)
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
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "description_desc" : "Description";
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

            var itemPagedList = await Service.GetAllPagedListAsync(parameters);
            return View(itemPagedList);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            parameters.Filter = i => i.EmployeeID == null;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "description_desc" : "Description";
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

            var itemPagedList = await Service.GetAllPagedListAsync(parameters);
            return View(itemPagedList);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IItem item = await Service.GetByIdAsync(ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
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
            // Other option without using viewmodel is by using ViewBag to store EmployeeList for DropDownList
            IAssignViewModel item = Mapper.Map<IAssignViewModel>(await Service.GetByIdAsync(ID));
            item.EmployeeList = Mapper.Map<List<IEmployee>>(await EmployeeService.GetAllAsync(employeeParameters));
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign([Bind(Include = "ID,Name,EmployeeID")] AssignViewModel assignedItem)
        {
            try
            {
                if (assignedItem.EmployeeID == null)
                {
                    return RedirectToAction("Assign", new { ID = assignedItem.ID });
                }
                if (ModelState.IsValid)
                {
                    Guid itemID = assignedItem.ID;
                    Guid? employeeID = assignedItem.EmployeeID;
                    await Service.AssignItemAsync(itemID, employeeID);
                    return RedirectToAction("OnStock");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Assign", new { ID = assignedItem.ID });
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
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,SerialNumber,EmployeeID")] Item createdItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IItem item = createdItem; // Not sure this is allowed, use automapper for new?
                    await Service.CreateAsync(item);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(createdItem);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IItem item = await Service.GetByIdAsync(ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,SerialNumber,EmployeeID")] Item editedItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IItem item = editedItem; // Not sure this is allowed, use automapper for new?
                    await Service.UpdateAsync(item);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editedItem);
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
            IItem item = await Service.GetByIdAsync(ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
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
