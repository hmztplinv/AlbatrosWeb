public class UpdateAnnouncementDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public bool IsPublished { get; set; }
}
