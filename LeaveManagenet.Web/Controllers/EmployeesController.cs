using AutoMapper;
using LeaveManagenet.Web.Constants;
using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagenet.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public EmployeesController(UserManager<Employee> userManager,
                                    IMapper mapper,
                                    ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }


        // GET: EmployeesController
        public async Task<IActionResult> Index()
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var model = mapper.Map<List<EmployeeListVM>>(employees);

            return View(model);
        }

        //
        // GET: EmployeesController/ViewAllocations/5
        public async Task<IActionResult> ViewAllocations(string id)
        { // (string id) es la id del empleado
            var model =await leaveAllocationRepository.GetEmployeeAllocations(id);
            return View(model);
        }

        //
        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: EmployeesController/EditAllocation/5
        public async Task<IActionResult> EditAllocation(int id)
        { // (int id) de leaveAllocation
            var model = await leaveAllocationRepository.GetEmployeeAllocation(id);
            
            if (model == null)
                return NotFound();

            return View(model);
        }

        //
        // POST: EmployeesController/EditAllocation/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(int id, IFormCollection collection)
        {
            var leaveAllocation = await leaveAllocationRepository.GetAsync(id);
            if (leaveAllocation == null)
                return NotFound();

            var leaveTypeVM = mapper.Map<LeaveAllocationVM>(leaveAllocation);

            return View(leaveTypeVM);
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
