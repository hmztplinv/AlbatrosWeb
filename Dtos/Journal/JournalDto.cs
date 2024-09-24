public class JournalDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime PublishedDate { get; set; }
    public string? FilePath { get; set; }
}
