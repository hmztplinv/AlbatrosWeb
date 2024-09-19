public interface IPageRepository : IGenericRepository<Page>
{
    Task<IEnumerable<Page>> GetPagesWithSubPagesAsync();
}
