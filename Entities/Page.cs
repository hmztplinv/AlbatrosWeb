public class Page : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }

    public int? ParentPageId { get; set; }
    public Page? ParentPage { get; set; }
    public List<Page>? SubPages { get; set; }  

    public bool IsInMenu { get; set; } = true;  
    public int? MenuPosition { get; set; }      
    public bool IsVisible { get; set; } = true; 
}