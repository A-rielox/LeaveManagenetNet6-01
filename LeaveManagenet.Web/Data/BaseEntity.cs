namespace LeaveManagenet.Web.Data;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } // audit column
    public DateTime DateModified { get; set; } // audit column
}
