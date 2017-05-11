using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Model.DbEntities.Inventory;
using Service.Common.Inventory;
using System.Linq.Expressions;
using Model.Common.Inventory;
using Model.Inventory;

namespace MVC.Controllers
{
    public class VehicleController : Controller
    {
        protected IVehicleService service;
        public VehicleController(IVehicleService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            List<IVehicle> vehicle = await service.GetAllAsync(null, null, null);
            return View(vehicle);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock()
        {
            Expression<Func<VehicleEntity, bool>> filter = e => e.EmployeeID == null;

            List<IVehicle> vehicle = await service.GetAllAsync(filter, null, null);
            return View(vehicle);
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
    }
}

