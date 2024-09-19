using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
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
        var parentPages = await _pageService.GetAllPagesAsync();
        ViewBag.ParentPages = parentPages;

        Console.WriteLine("Title: " + updatePageDto.Title);
        Console.WriteLine("Content: " + updatePageDto.Content);

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
            await _pageService.AddPageAsync(createPageDto);
            return RedirectToAction("Index");
        }
        return View(createPageDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _pageService.DeletePageAsync(id);
        return RedirectToAction(nameof(Index));
    }
}