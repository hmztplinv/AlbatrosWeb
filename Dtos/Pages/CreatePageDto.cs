public class CreatePageDto
{
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public int? ParentPageId { get; set; }

    public bool IsInMenu { get; set; }
    public int? MenuPosition { get; set; }
    public bool IsVisible { get; set; }
}