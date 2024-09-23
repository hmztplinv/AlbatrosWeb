using System.Linq.Expressions;

public interface IPageService
{
    Task<PageDto> GetPageByIdAsync(int id);
    Task<IEnumerable<PageDto>> GetAllPagesAsync();

     Task<List<PageDto>> GetParentPagesAsync();
    Task AddPageAsync(CreatePageDto createPageDto);
    Task UpdatePageAsync(UpdatePageDto updatePageDto);
    Task DeletePageAsync(int id);
    
    // Alt sayfaları getir
    Task<IEnumerable<PageDto>> GetSubPagesAsync(int parentPageId);
    Task<UpdatePageDto> GetUpdatePageByIdAsync(int id);

    Task<List<PageDto>> GetTopLevelPagesAsync();

    Task<bool> HasChildPagesAsync(int parentId);
    Task<List<PageDto>> GetMenuPagesAsync();

}

