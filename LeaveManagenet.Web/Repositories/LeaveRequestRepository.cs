using AutoMapper;
using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagenet.Web.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly UserManager<Employee> userManager;

        public LeaveRequestRepository(ApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILeaveAllocationRepository leaveAllocationRepository,
            UserManager<Employee> userManager )
            : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.userManager = userManager;
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;

            if (approved)
            {
                var allocation = await leaveAllocationRepository
                    .GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequestId);

                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                allocation.NumberOfDays -= daysRequested;

                await leaveAllocationRepository.UpdateAsync(allocation);
            }

            await UpdateAsync(leaveRequest);
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = user.Id;

            await AddAsync(leaveRequest);
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            //var leaveRequests = await GetAllAsync(); // lo obtiene a traves de : GenericRepository<LeaveRequest>
            var leaveRequests = await context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests),
            };

            foreach (var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee = mapper.Map<EmployeeListVM>( await userManager
                    .FindByIdAsync(leaveRequest.RequestingEmployeeId) );
            }

            return model;
        }

        public async Task<List<LeaveRequest>> GetAllAsync(string employeeId)
        {
            return await context.LeaveRequests
                .Where(p => p.RequestingEmployeeId == employeeId).ToListAsync();
        }

        public async Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

            var allocations = (await leaveAllocationRepository
                                .GetEmployeeAllocations(user.Id)).LeaveAllocations;
            
            var requests = mapper.Map<List<LeaveRequestVM>>(await GetAllAsync(user.Id));

            var model = new EmployeeLeaveRequestViewVM(allocations, requests);

            return model;

        }
    }
}
