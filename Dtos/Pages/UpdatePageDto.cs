public class UpdatePageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public bool IsDeleted { get; set; }= false;
    public int? ParentPageId { get; set; }

    public bool IsInMenu { get; set; }
    public int? MenuPosition { get; set; }
    public bool IsVisible { get; set; }
}