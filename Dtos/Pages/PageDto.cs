public class PageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public bool IsDeleted { get; set; }
    public int? ParentPageId { get; set; }
    public List<PageDto> SubPages { get; set; } = new List<PageDto>();
}
