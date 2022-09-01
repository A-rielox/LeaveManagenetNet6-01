using LeaveManagenet.Web.Constants;
using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagenet.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        // UserManager es la API para magejar los users
        public LeaveAllocationRepository(
                ApplicationDbContext context,
                UserManager<Employee> userManager,
                ILeaveTypeRepository leaveTypeRepository
            )
            : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<bool> AllocationExist(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(q => 
                                                    q.EmployeeId == employeeId 
                                                    && q.LeaveTypeId == leaveTypeId 
                                                    && q.Period == period
                                                    );
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
    }
}
