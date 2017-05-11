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
    public class MeasuringDeviceController : Controller
    {
        protected IMeasuringDeviceService service;
        public MeasuringDeviceController(IMeasuringDeviceService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            List<IMeasuringDevice> measuringDevice = await service.GetAllAsync(null, null, null);
            return View(measuringDevice);
        }

        [HttpGet]
        public async Task<ActionResult> OnStock()
        {
            Expression<Func<MeasuringDeviceEntity, bool>> filter = e => e.EmployeeID == null;

            List<IMeasuringDevice> measuringDevice = await service.GetAllAsync(filter, null, null);
            return View(measuringDevice);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMeasuringDevice measuringDevice = await service.GetByIdAsync(ID);
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
        }
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
                    await service.CreateAsync(measuringDevice);
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
        public async Task<ActionResult> Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMeasuringDevice measuringDevice = await service.GetByIdAsync(ID);
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
                    await service.UpdateAsync(measuringDevice);
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
            IMeasuringDevice measuringDevice = await service.GetByIdAsync(ID);
            if (measuringDevice == null)
            {
                return HttpNotFound();
            }
            return View(measuringDevice);
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
