public class Page : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }

    // Alt sayfalar için Parent-Child ilişkisi
    public int? ParentPageId { get; set; }
    public Page? ParentPage { get; set; }
    public List<Page>? SubPages { get; set; }  // Alt sayfalar

    // Menü ile ilgili alanlar
    public bool IsInMenu { get; set; } = true;  // Menüde görünür mü?
    public int? MenuPosition { get; set; }      // Menüdeki sırası
    public bool IsVisible { get; set; } = true; // Görünür mü?
}