public class Announcement : BaseEntity
{
    public string Title { get; set; }=null!;
    public string? Content { get; set; }
    public bool IsPublished { get; set; }
}