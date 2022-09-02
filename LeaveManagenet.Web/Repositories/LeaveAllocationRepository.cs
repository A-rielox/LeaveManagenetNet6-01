using AutoMapper;
using LeaveManagenet.Web.Constants;
using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagenet.Web.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<Employee> userManager;
		private readonly ILeaveTypeRepository leaveTypeRepository;
		private readonly IMapper mapper;

		// UserManager es la API para magejar los users
		public LeaveAllocationRepository(
				ApplicationDbContext context,
				UserManager<Employee> userManager,
				ILeaveTypeRepository leaveTypeRepository,
				IMapper mapper
			)
			: base(context)
		{
			this.context = context;
			this.userManager = userManager;
			this.leaveTypeRepository = leaveTypeRepository;
			this.mapper = mapper;
		}

		public async Task<bool> AllocationExist(string employeeId, int leaveTypeId, int period)
		{
			return await context.LeaveAllocations.AnyAsync(q => 
													q.EmployeeId == employeeId 
													&& q.LeaveTypeId == leaveTypeId 
													&& q.Period == period
													);
		}

		public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
		{
			var allocations = await context.LeaveAllocations
				.Include(a => a.LeaveType)
				.Where(a => a.EmployeeId == employeeId).ToListAsync();

			var employee = await userManager.FindByIdAsync(employeeId);

			var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);
			employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocations);

			return employeeAllocationModel;
		}
		/*
		 LeaveAllocation ( var allocations )
			public int NumberOfDays { get; set; }
			public int LeaveTypeId { get; set; }
			public LeaveType LeaveType { get; set; }
			public string EmployeeId { get; set; }
			public int Period { get; set; }

				LeaveType
					public string Name { get; set; }
					public int DefaultDays { get; set; }
					public int Id { get; set; }
					public DateTime DateCreated { get; set; }
					public DateTime DateModified { get; set; }


		EmployeeAllocationVM 
			public List<LeaveAllocationVM> LeaveAllocations
				public int NumberOfDays { get; set; }
				public int Period { get; set; }
				public LeaveTypeVM LeaveType { get; set; }
					LeaveTypeVM
						public int Id { get; set; }
						public string Name { get; set; }
						public int DefaultDays { get; set; }
		 */

		public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
		{
			var allocation = await context.LeaveAllocations
				.Include(a => a.LeaveType)
				.FirstOrDefaultAsync(q => q.Id == id);

			if (allocation == null)
				return null;

			var employee = await userManager.FindByIdAsync(allocation.EmployeeId);

			var model = mapper.Map<LeaveAllocationEditVM>(allocation);
			model.Employee = mapper
				.Map<EmployeeListVM>(await userManager.FindByIdAsync(allocation.EmployeeId));

			return model;
		}
		public async Task LeaveAllocation(int leaveTypeId)
		{
			var employees = await userManager.GetUsersInRoleAsync(Roles.User);
			var period = DateTime.Now.Year;
			var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);
			var allocations = new List<LeaveAllocation>();

			foreach (var employee in employees)
			{
				if (await AllocationExist(employee.Id, leaveTypeId, period))
					continue;

				var allocation = new LeaveAllocation
				{
					EmployeeId = employee.Id,
					LeaveTypeId = leaveTypeId,
					Period = period,
					NumberOfDays = leaveType.DefaultDays,
				};

				allocations.Add(allocation);
			}

			await AddRangeAsync(allocations);
			// EF save q lo q se pasa es de tipo LeaveAllocation, asi que hace las entradas
			// en esa tabla
		}

		public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model)
		{
			// como no tengo todos los datos en LeaveAllocationEditVM para el modelo q voy
			// a actualizar => necesito primero obtener el resto
			// con EF es mejor mandar todos los datos para actualizar, no na mas los q cambian
			var leaveAllocation = await GetAsync(model.Id);

			if (leaveAllocation == null)
				return false;

			leaveAllocation.Period = model.Period;
			leaveAllocation.NumberOfDays = model.NumberOfDays;
			await UpdateAsync(leaveAllocation);

			return true;
		}
	}
}
