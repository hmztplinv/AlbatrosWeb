public class SliderRepository : GenericRepository<Slider>, ISliderRepository
{
    public SliderRepository(AlbatrosPortfoyPortalDbContext dbContext) : base(dbContext)
    {
    }
}