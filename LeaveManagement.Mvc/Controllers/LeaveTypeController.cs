using LeaveManagement.Domain;
using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Mvc.Controllers
{
    //[Authorize]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveAllocationService _leaveAllocationService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, ILeaveAllocationService leaveAllocationService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveAllocationService = leaveAllocationService;
        }
        // GET: LeaveTypeController
        public async Task<ActionResult> Index()
        {
            var leaveTypeList =await _leaveTypeService.GetLeaveTypes();
            return View(leaveTypeList);
        }

        // GET: LeaveTypeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveType(id);
            return View(leaveType);
        }

        // GET: LeaveTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeVm leaveType)
        {
            try
            {
                var response= await _leaveTypeService.CreateLeaveType(leaveType);
                if (response.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("",response.ValidationErrors);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(leaveType);
        }

        // GET: LeaveTypeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveType(id);
            return View(leaveType);
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVm leaveType)
        {
            try
            {
                var response = await _leaveTypeService.UpdateLeaveType(leaveType);
                if (response.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(leaveType);
        }

        // GET: LeaveTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveType(id);
            return View(leaveType);
        }

        // POST: LeaveTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var leaveType = await _leaveTypeService.GetLeaveType(id);
                var response = await _leaveTypeService.DeleteLeaveType(id);
                if (response.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int Id)
        {
            try
            {
                var response = await _leaveAllocationService.CreateLeaveAllocations(Id);
                if (response.Success)
                    return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
             ModelState.AddModelError("",e.Message);
            }

            return BadRequest();
        }
    }
}
