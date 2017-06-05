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
    public class VehicleController : Controller
    {
        protected IVehicleService service;
        public VehicleController(IVehicleService service)
        {
            this.service = service;
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var vehiclePagedList = await service.GetAllPagedListAsync(searchString, sortOrder, pageNumber, pageSize, null);
            return View(vehiclePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            Expression<Func<VehicleEntity, bool>> filter = v => v.EmployeeID == null;
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var vehiclePagedList = await service.GetAllPagedListAsync(searchString, sortOrder, pageNumber, pageSize, filter);
            return View(vehiclePagedList);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicle vehicle = await service.GetByIdAsync(ID);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
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
            IAssignViewModel vehicle = await service.CreateAssignViewModelAsync(ID);
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
                    IAssignViewModel measuringDevice = assignedVehicle; // Not sure this is allowed, use automapper for new?
                    await service.AssignVehicleAsync(measuringDevice);
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
                    await service.CreateAsync(vehicle);
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
        public async Task<ActionResult> Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicle vehicle = await service.GetByIdAsync(ID);
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
                    await service.UpdateAsync(vehicle);
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
            IVehicle vehicle = await service.GetByIdAsync(ID);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
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

