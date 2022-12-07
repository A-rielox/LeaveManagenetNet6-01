namespace LeaveManagenet.Web.Models;

public class EmployeeLeaveRequestViewVM
{
    public EmployeeLeaveRequestViewVM(List<LeaveAllocationVM> leaveAllocations,
        List<LeaveRequestVM> leaveRequest)
    {
        LeaveAllocations = leaveAllocations;
        LeaveRequest = leaveRequest;

    }
    public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    public List<LeaveRequestVM> LeaveRequest { get; set; }
}

/*  
public class LeaveAllocationVM
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public LeaveTypeVM? LeaveType { get; set; }
}
*/