using System;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Service.Common.Inventory;
using Model.Common.Inventory;
using Model.Inventory;
using MVC.Models.AssignViewModels;
using AutoMapper;
using Model.Common;
using Service.Common;
using System.Collections.Generic;
using MVC.Models.ItemViewModels;
using PagedList;
using Common.Parameters;

namespace MVC.Controllers
{
    public class ItemController : Controller
    {
        protected IItemService Service;
        protected IEmployeeService EmployeeService;
        protected IItemParameters itemParameters;
        protected IEmployeeParameters employeeParameters;
        public ItemController(IItemService service, IEmployeeService employeeService, IItemParameters itemParameters, IEmployeeParameters employeeParameters)
        {
            this.Service = service;
            this.EmployeeService = employeeService;
            this.itemParameters = itemParameters;
            this.employeeParameters = employeeParameters;
        }

        #region Get

        [HttpGet]
        public async Task<ViewResult> Index(string currentFilter, int? page, string searchString = null, string sortOrder = null)
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
            itemParameters.PageSize = 5;
            itemParameters.PageNumber = (page ?? 1);
            itemParameters.SearchString = searchString;
            itemParameters.SortOrder = sortOrder;
            itemParameters.Paged = true;

            var itemPagedList = await Service.GetAllPagedListAsync(itemParameters);
            var viewModel = Mapper.Map<IEnumerable<ItemIndexViewModel>>(itemPagedList);
            var pagedViewModel = new StaticPagedList<ItemIndexViewModel>(viewModel, itemPagedList.GetMetaData());
            return View(pagedViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
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
            itemParameters.PageSize = 5;
            itemParameters.PageNumber = (page ?? 1);
            itemParameters.SearchString = searchString;
            itemParameters.SortOrder = sortOrder;
            itemParameters.Paged = true;
            itemParameters.OnStock = true;

            var itemPagedList = await Service.GetAllPagedListAsync(itemParameters);
            var viewModel = Mapper.Map<IEnumerable<ItemOnStockViewModel>>(itemPagedList);
            var pagedViewModel = new StaticPagedList<ItemOnStockViewModel>(viewModel, itemPagedList.GetMetaData());
            return View(pagedViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemViewModel = Mapper.Map<ItemDetailsViewModel>(await Service.GetByIdAsync(ID));
            if (itemViewModel == null)
            {
                return HttpNotFound();
            }
            return View(itemViewModel);
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
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,SerialNumber,EmployeeID")] ItemCreateViewModel createdItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IItem item = (Mapper.Map<Item>(createdItem));
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
            var itemViewModel = Mapper.Map<ItemEditViewModel>(await Service.GetByIdAsync(ID));
            if (itemViewModel == null)
            {
                return HttpNotFound();
            }
            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,SerialNumber,EmployeeID")] ItemEditViewModel editedItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IItem item = (Mapper.Map<Item>(editedItem));
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
            var itemViewModel = Mapper.Map<ItemDeleteViewModel>(await Service.GetByIdAsync(ID));
            if (itemViewModel == null)
            {
                return HttpNotFound();
            }
            return View(itemViewModel);
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
