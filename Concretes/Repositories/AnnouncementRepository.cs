public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
{
    public AnnouncementRepository(AlbatrosPortfoyPortalDbContext context) : base(context)
    {
    }
}
