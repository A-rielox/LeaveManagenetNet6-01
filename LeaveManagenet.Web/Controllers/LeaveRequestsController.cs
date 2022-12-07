using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Models;
using LeaveManagenet.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagenet.Web.Constants;

namespace LeaveManagenet.Web.Controllers;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public LeaveRequestsController(
        ApplicationDbContext context, 
        ILeaveRequestRepository leaveRequestRepository)
    {
        _context = context;
        this.leaveRequestRepository = leaveRequestRepository;
    }

    // GET: LeaveRequests
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Index()
    {
        var model = await leaveRequestRepository.GetAdminLeaveRequestList();

        return View(model);
    }

    //
    // GET: LeaveRequests/
    public async Task<IActionResult> MyLeave()
    {
        var model = await leaveRequestRepository.GetMyLeaveDetails();
        return View(model);
    }


    //
    // GET: LeaveRequests/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var model = await leaveRequestRepository.GetLeaveRequestAsync(id);
        if (model == null)
            return NotFound();

        return View(model);
    }

    //
    //
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveRequest(int id, bool approved)
    {
        try
        {
            await leaveRequestRepository.ChangeApprovalStatus(id, approved);
        }
        catch (Exception ex)
        {
            throw;
        }

        return Redirect(nameof(Index));
    }

    //
    // GET: LeaveRequests/Create
    //
    //-------------------------------> PARA EL DROPDOWN
    //
    // ViewData["LeaveTypeId"] es una data temporal q se manda a la view, esta con la key "LeaveTypeId", para
    // ser usada solo temporalmente en la view
    // new SelectList(_context.LeaveTypes, "Id", "Name") manda un dropdown-list; el 1er id es el valuefield, q
    // es lo q necesito trackear para saber a cual se refiere, el segundo es el textfield q s lo q despliega como opcion
    //      <option value = "1" > sick </ option >
    //      <option value="2">Vacations</option>
    // en la view accedo a ella a traves de ViewBag.LeaveTypeId
    // <select asp-for="LeaveTypeId" class ="form-select" asp-items="ViewBag.LeaveTypeId"></select>
    //public IActionResult Create()
    //{
    //    ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
    //    return View();
    //}
    public IActionResult Create()
    {
        var model = new LeaveRequestCreateVM
        {
            LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name")
        };

        return View(model);
    }

    // POST: LeaveRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( LeaveRequestCreateVM model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await leaveRequestRepository.CreateLeaveRequest(model);
                return RedirectToAction(nameof(MyLeave));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error has occurred. Please try later.");
        }
        

        model.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name", model.LeaveTypeId);
        return View(model);
    }

    //
    // GET: LeaveRequests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.LeaveRequests == null)
        {
            return NotFound();
        }

        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest == null)
        {
            return NotFound();
        }
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
        return View(leaveRequest);
    }

    // POST: LeaveRequests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,Approved,Cancelled,RequestingEmployeeId,Id,DateCreated,DateModified")] LeaveRequest leaveRequest)
    {
        if (id != leaveRequest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(leaveRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(leaveRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
        return View(leaveRequest);
    }

    //
    // GET: LeaveRequests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.LeaveRequests == null)
        {
            return NotFound();
        }

        var leaveRequest = await _context.LeaveRequests
            .Include(l => l.LeaveType)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaveRequest == null)
        {
            return NotFound();
        }

        return View(leaveRequest);
    }

    // POST: LeaveRequests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.LeaveRequests == null)
        {
            return Problem("Entity set 'ApplicationDbContext.LeaveRequests'  is null.");
        }
        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest != null)
        {
            _context.LeaveRequests.Remove(leaveRequest);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveRequestExists(int id)
    {
      return (_context.LeaveRequests?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
