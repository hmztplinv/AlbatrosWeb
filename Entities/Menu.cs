public class Menu : BaseEntity
{
    public string Name { get; set; }=null!;
    public string Url { get; set; }=null!;
    public int? ParentMenuId { get; set; }
    public Menu? ParentMenu { get; set; }
    public ICollection<Menu>? SubMenus { get; set; }
}