using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AlbatrosPortfoyPortal.Models;

namespace AlbatrosPortfoyPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPageService _pageService;

    public HomeController(ILogger<HomeController> logger, IPageService pageService)
    {
        _logger = logger;
        _pageService = pageService;
    }


    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> About()
    {
        var aboutPage = await _pageService.GetPageByIdAsync(1);
        if (aboutPage == null)
        {
            return NotFound();
        }

        return View(aboutPage);
    }

    public IActionResult Contact()
    {
        ViewBag.Title = "İletişim";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
