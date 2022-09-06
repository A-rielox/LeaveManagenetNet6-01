namespace LeaveManagenet.Web.Data
{
    public class LeaveRequest : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // FK
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public DateTime DateRequested { get; set; }
        public string? RequestComments { get; set; }

        // nullable, para q cuando este null yo sepa q esta pendiente
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }

        public string RequestingEmployeeId { get; set; }

    }
}
