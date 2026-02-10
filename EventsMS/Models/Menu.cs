namespace EventsMS.Models;

public class Menu: BaseEntities.BaseEntity<long>
{
    public string Title { get; set; } = null!;
    public string Url { get; set; } // e.g. "/StudentRegistration/Index"
    public long? ParentId { get; set; }
    public Menu Parent { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public ICollection<Menu> Children { get; set; } = new List<Menu>();
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
