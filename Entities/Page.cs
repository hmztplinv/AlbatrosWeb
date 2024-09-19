public class Page : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }

    // Alt sayfalar için Parent-Child ilişkisi
    public int? ParentPageId { get; set; }
    public Page? ParentPage { get; set; }
    public List<Page>? SubPages { get; set; }  // Alt sayfalar
}