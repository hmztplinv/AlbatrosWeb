public class JournalRepository : GenericRepository<Journal>, IJournalRepository
{
    public JournalRepository(AlbatrosPortfoyPortalDbContext context) : base(context)
    {
    }
}