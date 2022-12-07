using LeaveManagenet.Web.Contracts;
using LeaveManagenet.Web.Data;

namespace LeaveManagenet.Web.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(ApplicationDbContext context)
        : base(context)
    {

    }
}
