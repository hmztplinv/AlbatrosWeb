public class Journal : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? FilePath { get; set; }
    public DateTime PublishedDate { get; set; }
}
