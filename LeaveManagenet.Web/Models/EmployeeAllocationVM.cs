namespace LeaveManagenet.Web.Models;

public class EmployeeAllocationVM : EmployeeListVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; }
}
/*
* public class LeaveAllocationVM
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public LeaveTypeVM LeaveType { get; set; }
}
* 
*/