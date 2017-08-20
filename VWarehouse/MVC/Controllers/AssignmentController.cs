using System;
using System.Data;
using System.Net;
using System.Web.Mvc;
using Model;
using System.Threading.Tasks;
using Model.Common;
using Service.Common;
using MVC.Models.AssignmentViewModels;
using AutoMapper;
using PagedList;
using System.Collections.Generic;
using Common.Parameters;
using MVC.Models.AssignViewModels;

namespace MVC.Controllers
{
    public class AssignmentController : Controller
    {

        protected IAssignmentService Service;
        protected IAssignmentParameters AssignmentParameters;
        public AssignmentController(IAssignmentService service, IAssignmentParameters assignmentParameters)
        {
            this.Service = service;
            this.AssignmentParameters = assignmentParameters;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult> Index(string currentFilter, int? page, string searchString = null, string sortOrder = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.NameSortParm = sortOrder == "StartTime" ? "startTime_desc" : "StartTime";
            ViewBag.NameSortParm = sortOrder == "EndTime" ? "endTime_desc" : "EndTime";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            AssignmentParameters.PageSize = 5;
            AssignmentParameters.PageNumber = (page ?? 1);
            AssignmentParameters.SearchString = searchString;
            AssignmentParameters.SortOrder = sortOrder;
            AssignmentParameters.Paged = true;

            var assignmentPagedList = await Service.GetAllPagedListAsync(AssignmentParameters);
            var viewModel = Mapper.Map<IEnumerable<AssignmentIndexViewModel>>(assignmentPagedList);
            var pagedViewModel = new StaticPagedList<AssignmentIndexViewModel>(viewModel, assignmentPagedList.GetMetaData());
            return View(pagedViewModel);
        }
        

        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assignmentViewModel = Mapper.Map<AssignmentDetailsViewModel>(await Service.GetByIdAsync(id));
            if (assignmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(assignmentViewModel);
        }
        #endregion

        #region Inventory/Employee return

        [HttpGet]
        public async Task<ActionResult> ReturnOneEmployee(Guid? empID, Guid? assignmentID)
        {
            if (empID == null || assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneEmployeeAsync(empID, assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAllEmployee(Guid? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllEmployeesAsync(assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneItem(Guid? itemID, Guid? assignmentID)
        {
            if (itemID == null || assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneItemAsync(itemID, assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }


        [HttpGet]
        public async Task<ActionResult> ReturnAllItems(Guid? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllItemsAsync(assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneMeasuringDevice(Guid? MDeviceID, Guid? assignmentID)
        {
            if (MDeviceID == null || assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneMeasuringDeviceAsync(MDeviceID, assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }


        [HttpGet]
        public async Task<ActionResult> ReturnAllMeasuringDevices(Guid? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllMeasuringDevicesAsync(assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnOneVehicle(Guid? vehicleID, Guid? assignmentID)
        {
            if (vehicleID == null || assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnOneVehicleAsync(vehicleID, assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }


        [HttpGet]
        public async Task<ActionResult> ReturnAllVehicles(Guid? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAllVehiclesAsync(assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }

        [HttpGet]
        public async Task<ActionResult> ReturnAll(Guid? assignmentID)
        {
            if (assignmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await Service.ReturnAll(assignmentID);
            return RedirectToAction("Details", new { ID = assignmentID });
        }
        #endregion

        #region Assign    
        
        [HttpGet]
        public async Task<ActionResult> Assign(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assignmentViewModel = await Service.GetByIdAsync(id);
            this.AssignmentParameters.ID = assignmentViewModel.ID;
            this.AssignmentParameters.StartTime = assignmentViewModel.StartTime;
            this.AssignmentParameters.EndTime = assignmentViewModel.EndTime;
            IAssignmentAssignViewModel assignment = new AssignmentAssignViewModel();
            assignment.EmployeeList = await Service.GetAllEmployeesAsync(AssignmentParameters);
            return View(assignment);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign([Bind(Include = "ID,Name,Materials,StartTime,EndTime,Note,AssignedEmployeeIDList,AssignedItemIDList,AssignedMeasuringDeviceIDList,AssignedVehicleIDList,EmployeeList,ItemList,MeasuringDeviceList,VehicleList")] AssignmentAssignViewModel assignment)
        {
            try
            {
                if (assignment == null)
                {
                    return RedirectToAction("Assign", new { ID = assignment.ID });
                }
                if (ModelState.IsValid)
                {
                    Guid itemID = assignedItem.ID;
                    Guid? employeeID = assignedItem.EmployeeID;
                    await Service.AssignAsync(assignment);
                    return RedirectToAction("OnStock");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Assign", new { ID = assignedItem.ID });
        }
        */
        #endregion

        #region CRUD

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Materials,StartTime,EndTime,Note")] AssignmentCreateViewModel createdAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IAssignment assignment = (Mapper.Map<Assignment>(createdAssignment));
                    await Service.CreateAsync(assignment);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(createdAssignment);
        }

        
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assignmentViewModel = Mapper.Map<AssignmentEditViewModel>(await Service.GetByIdAsync(id));
            if (assignmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(assignmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Materials,StartTime,EndTime,Note")] AssignmentEditViewModel editedAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IAssignment assignment = (Mapper.Map<Assignment>(editedAssignment));
                    await Service.UpdateAsync(assignment);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editedAssignment);
        }

        
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete faild. Try again and if the problem persists see your system administrator.";
            }
            var assignmentViewModel = Mapper.Map<AssignmentDeleteViewModel>(await Service.GetByIdAsync(id));
            if (assignmentViewModel == null)
            {
                return HttpNotFound();
            }
            return View(assignmentViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await Service.DeleteAsync(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { ID = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        
        #endregion
        
    }
}
