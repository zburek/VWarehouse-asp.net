using System;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Model.DbEntities.Inventory;
using Service.Common.Inventory;
using System.Linq.Expressions;
using Model.Common.Inventory;
using Model.Inventory;
using Model.Common.ViewModels;
using Model.ViewModels;

namespace MVC.Controllers
{
    public class ItemController : Controller
    {
        protected IItemService service;
        public ItemController(IItemService service)
        {
            this.service = service;
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var itemPagedList = await service.GetAllPagedListAsync(searchString, sortOrder, pageNumber, pageSize, null);
            return View(itemPagedList);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            Expression<Func<ItemEntity, bool>> filter = i => i.EmployeeID == null;
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var itemPagedList = await service.GetAllPagedListAsync(searchString, sortOrder, pageNumber, pageSize, filter);
            return View(itemPagedList);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IItem item = await service.GetByIdAsync(ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        #endregion

        #region Assign

        [HttpGet]
        public async Task<ActionResult> Assign(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Other option without using viewmodel is by using ViewBag to store EmployeeList for DropDownList
            IAssignViewModel item = await service.CreateAssignViewModelAsync(ID);
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
                    IAssignViewModel item = assignedItem; // Not sure this is allowed, use automapper for new?
                    await service.AssignItemAsync(item);
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
                    await service.CreateAsync(item);
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
        public async Task<ActionResult> Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IItem item = await service.GetByIdAsync(ID);
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
                    await service.UpdateAsync(item);
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
            IItem item = await service.GetByIdAsync(ID);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ID)
        {
            try
            {
                await service.DeleteAsync(ID);
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
