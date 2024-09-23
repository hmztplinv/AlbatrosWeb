using System.Linq.Expressions;

public interface IPageRepository : IGenericRepository<Page>
{
    Task<IEnumerable<Page>> GetPagesWithSubPagesAsync();
    Task<IEnumerable<Page>> GetByConditionAsync(Expression<Func<Page, bool>> predicate);
}
