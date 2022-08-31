namespace LeaveManagenet.Web.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }

        public int LeaveTypeId { get; set; } //--> para FK
        public LeaveType LeaveType { get; set; } //--> para FK

        public string EmployeeId { get; set; }
    }
}
