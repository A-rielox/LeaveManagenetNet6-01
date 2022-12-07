namespace LeaveManagenet.Web.Data;

public class LeaveType : BaseEntity
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}

//public abstract class BaseEntity
//{
//    public int Id { get; set; }
//    public DateTime DateCreated { get; set; } // audit column
//    public DateTime DateModified { get; set; } // audit column
//}