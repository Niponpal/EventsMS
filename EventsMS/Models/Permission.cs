namespace EventsMS.Models;

public class Permission:BaseEntities.BaseEntity<long>
{
    public long RoleId { get; set; }

    public long MenuId { get; set; }
    public Menu Menu { get; set; } = null!;

    public bool CanView { get; set; } = false;
    public bool CanCreate { get; set; } = false;
    public bool CanEdit { get; set; } = false;
    public bool CanDelete { get; set; } = false;
    public bool CanApprove { get; set; } = false;
}
