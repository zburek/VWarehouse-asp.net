using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Model;
using Model.DbEntities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Common;
using Service.Common;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployeeService service;
        public EmployeeController(IEmployeeService service)
        { 
            this.service = service;
        }
        

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = source => source.OrderByDescending(e => e.Name);
            Expression<Func<EmployeeEntity, bool>> filter = e => e.Name == "Mark";

            List<IEmployee> employee = await service.GetAllAsync(null, null, null);
            return View(employee);
        }
        
        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpGet]

        public async Task<ActionResult> Inventory(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expression<Func<EmployeeEntity, bool>> filter = e => e.ID == ID;
            string includeProperties = "Items, MeasuringDevices, Vehicles";
            IEmployee employee = await service.GetOneAsync(filter, includeProperties);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,PhoneNumber")] Employee createdEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = createdEmployee; // Not sure this is allowed, use automapper for new employee?
                    await service.CreateAsync(employee);
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
        public async Task<ActionResult> Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,PhoneNumber")] Employee editedEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEmployee employee = editedEmployee; // Not sure this is allowed, use automapper for new employee?
                    await service.UpdateAsync(employee);
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
            IEmployee employee = await service.GetByIdAsync(ID);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
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
