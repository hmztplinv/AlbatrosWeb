using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PageController : Controller
{
    private readonly IPageService _pageService;

    public PageController(IPageService pageService)
    {
        _pageService = pageService;
    }

    public async Task<IActionResult> Index()
    {
        var pages = await _pageService.GetAllPagesAsync();
        return View(pages);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {

        var updatePageDto = await _pageService.GetUpdatePageByIdAsync(id); // Servisten doğrudan UpdatePageDto geliyor
        if (updatePageDto == null)
        {
            return NotFound();
        }

        // Tüm sayfaları alarak üst sayfa seçimini sağlamak için
        var parentPages = await _pageService.GetTopLevelPagesAsync();
        ViewBag.ParentPages = parentPages;

        return View(updatePageDto);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePageDto updatePageDto)
    {
        if (ModelState.IsValid)
        {
            await _pageService.UpdatePageAsync(updatePageDto);
            return RedirectToAction(nameof(Index));
        }

        return View(updatePageDto);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // Sadece ana sayfaları getir
        var pages = await _pageService.GetTopLevelPagesAsync();

        var model = new CreatePageViewModel
        {
            ParentPages = pages.ToList(),
            CreatePageDto = new CreatePageDto()
        };

        return View(model);
    }



    [HttpPost]
    public async Task<IActionResult> Create(CreatePageDto createPageDto)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine($"IsInMenu: {createPageDto.IsInMenu}");
        Console.WriteLine($"IsVisible: {createPageDto.IsVisible}");
            await _pageService.AddPageAsync(createPageDto);
            return RedirectToAction("Index");
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        // Model geçersizse tekrar formu yükleyeceğiz, fakat ViewModel nesnesi ile.
        var pages = await _pageService.GetTopLevelPagesAsync(); // Parent sayfaları tekrar yüklemek gerekiyor
        var model = new CreatePageViewModel
        {
            ParentPages = pages.ToList(),
            CreatePageDto = createPageDto
        };

        return View(model); // View'e CreatePageViewModel döndürülmeli
    }


    public async Task<IActionResult> Delete(int id)
    {
        var hasChildPages = await _pageService.HasChildPagesAsync(id);

        if (hasChildPages)
        {
            // Alt sayfaları olduğu için uyarı göster
            TempData["ErrorMessage"] = "Bu sayfa altında bağlı alt sayfalar bulunmaktadır. Lütfen önce alt sayfaları silin.";
            return RedirectToAction(nameof(Index));
        }

        // Eğer alt sayfalar yoksa sayfayı sil
        await _pageService.DeletePageAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // Menüde sayfa gösterme
    public async Task<IActionResult> Menu()
    {
        var menuPages = await _pageService.GetMenuPagesAsync();
        return View(menuPages);
    }

}