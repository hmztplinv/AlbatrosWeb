public class Journal : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? FilePath { get; set; } // PDF dosya yolu
    public DateTime PublishedDate { get; set; }
}
