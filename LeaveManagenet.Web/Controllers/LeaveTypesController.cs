using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagenet.Web.Data;
using AutoMapper;
using LeaveManagenet.Web.Models;
using LeaveManagenet.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagenet.Web.Constants;

namespace LeaveManagenet.Web.Controllers;

[Authorize(Roles = Roles.Administrator)] // requiere estar autenticado ( logeado ) y ser "Admin" para poder acceder
public class LeaveTypesController : Controller
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;

    public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository,
                                IMapper mapper,
                                ILeaveAllocationRepository leaveAllocationRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
        this.leaveAllocationRepository = leaveAllocationRepository;
    }

    // GET: LeaveTypes
    public async Task<IActionResult> Index()
    {
        var leaveTypes = mapper.Map<List<LeaveTypeVM>>(await leaveTypeRepository.GetAllAsync());
        return View(leaveTypes);
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var leaveType = await leaveTypeRepository.GetAsync(id);
        if (leaveType == null)
            return NotFound();

        var leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);

        return View(leaveTypeVM);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
    {
        // el "ModelState.IsValid" checa q sea valido con lo q puse en el modelo ( como los [Required] y eso )
        if (ModelState.IsValid)
        {
            var leaveType = mapper.Map<LeaveType>(leaveTypeVM);
            await leaveTypeRepository.AddAsync(leaveType);

            return RedirectToAction(nameof(Index));
        }

        return View(leaveTypeVM);
    }

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var leaveType = await leaveTypeRepository.GetAsync(id);
        if (leaveType == null)
            return NotFound();

        var leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);

        return View(leaveTypeVM);
    }

    // POST: LeaveTypes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LeaveTypeVM leaveTypeVM)
    {
        if (id != leaveTypeVM.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var leaveType = mapper.Map<LeaveType>(leaveTypeVM);
                await leaveTypeRepository.UpdateAsync(leaveType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await leaveTypeRepository.Exist(leaveTypeVM.Id))
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

        return View(leaveTypeVM);
    }


    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await leaveTypeRepository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    // POST: LeaveTypes/AllocateLeave/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(int id)
    { // "int id" es la LeaveAllocation Id
        await leaveAllocationRepository.LeaveAllocation(id);

        return RedirectToAction(nameof(Index));
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagenet.Web.Data;

namespace LeaveManagenet.Web.Controllers
{
public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;

    public LeaveTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: LeaveTypes
    public async Task<IActionResult> Index()
    {
          return _context.LeaveTypes != null ? 
                      View(await _context.LeaveTypes.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.LeaveTypes == null)
        {
            return NotFound();
        }

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,DefaultDays,Id,DateCreated,DateModified")] LeaveType leaveType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(leaveType);
    }

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.LeaveTypes == null)
        {
            return NotFound();
        }

        var leaveType = await _context.LeaveTypes.FindAsync(id);
        if (leaveType == null)
        {
            return NotFound();
        }
        return View(leaveType);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,DefaultDays,Id,DateCreated,DateModified")] LeaveType leaveType)
    {
        if (id != leaveType.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(leaveType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(leaveType.Id))
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
        return View(leaveType);
    }

    // GET: LeaveTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.LeaveTypes == null)
        {
            return NotFound();
        }

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.LeaveTypes == null)
        {
            return Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
        }
        var leaveType = await _context.LeaveTypes.FindAsync(id);
        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveTypeExists(int id)
    {
      return (_context.LeaveTypes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
}
*/