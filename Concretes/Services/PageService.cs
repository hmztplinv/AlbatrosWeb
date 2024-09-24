using System.Linq.Expressions;
using AutoMapper;

public class PageService : IPageService
{
    private readonly IPageRepository _pageRepository;
    private readonly IMapper _mapper;

    public PageService(IPageRepository pageRepository, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _mapper = mapper;
    }

    public async Task AddPageAsync(CreatePageDto createPageDto)
    {
        var page = _mapper.Map<Page>(createPageDto);
        await _pageRepository.AddAsync(page);
    }

    public async Task<IEnumerable<PageDto>> GetAllPagesAsync()
    {
        var pages = await _pageRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PageDto>>(pages);
    }

    public async Task<PageDto> GetPageByIdAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        return _mapper.Map<PageDto>(page);
    }

    public async Task<UpdatePageDto> GetUpdatePageByIdAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
        {
            return null;
        }

        var updatePageDto = _mapper.Map<UpdatePageDto>(page);
        return updatePageDto;
    }

    public async Task<List<PageDto>> GetParentPagesAsync()
    {
        var pages = await _pageRepository.GetAllAsync();
        return _mapper.Map<List<PageDto>>(pages);
    }

    public async Task<List<PageDto>> GetTopLevelPagesAsync()
    {
        var pages = await _pageRepository.FindAsync(p => p.ParentPageId == null);
        return _mapper.Map<List<PageDto>>(pages);
    }

    public async Task UpdatePageAsync(UpdatePageDto updatePageDto)
    {
        var page = _mapper.Map<Page>(updatePageDto);
        await _pageRepository.UpdateAsync(page);
    }

    public async Task DeletePageAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page != null)
        {
            await _pageRepository.DeleteAsync(page);
        }
    }

    public async Task<IEnumerable<PageDto>> GetSubPagesAsync(int parentPageId)
    {
        var subPages = await _pageRepository.FindAsync(p => p.ParentPageId == parentPageId);
        return _mapper.Map<IEnumerable<PageDto>>(subPages);
    }

    public async Task<bool> HasChildPagesAsync(int parentId)
    {
        var childPages = await _pageRepository.GetByConditionAsync(p => p.ParentPageId == parentId);
        return childPages.Any();
    }

    public async Task<List<PageDto>> GetMenuPagesAsync()
{
    var allPages = await _pageRepository.GetAllAsync();
    var menuPages = allPages
        .Where(p => p.IsInMenu && p.IsVisible)
        .OrderBy(p => p.MenuPosition)
        .ToList();

    var topLevelPages = menuPages.Where(p => p.ParentPageId == null).ToList();

    foreach (var topLevelPage in topLevelPages)
    {
        topLevelPage.SubPages = menuPages.Where(p => p.ParentPageId == topLevelPage.Id).ToList();
    }

    return _mapper.Map<List<PageDto>>(topLevelPages);
}

}
