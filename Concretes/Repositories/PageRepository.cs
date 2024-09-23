using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PageRepository : GenericRepository<Page>, IPageRepository
{
    private readonly AlbatrosPortfoyPortalDbContext _context;

    public PageRepository(AlbatrosPortfoyPortalDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Page>> GetByConditionAsync(Expression<Func<Page, bool>> predicate)
    {
        return await _context.Pages.Where(predicate).ToListAsync();
    }


    public async Task<IEnumerable<Page>> GetPagesWithSubPagesAsync()
    {
        return await _context.Pages.Include(p => p.SubPages).ToListAsync();
    }
}
