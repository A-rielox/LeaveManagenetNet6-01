namespace LeaveManagenet.Web.Data;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }

    public int LeaveTypeId { get; set; } //--> para FK
    public LeaveType LeaveType { get; set; } //--> para FK

    public string EmployeeId { get; set; }
    public int Period { get; set; }
}

/*
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } // audit column
    public DateTime DateModified { get; set; } // audit column
}
*/