using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult TestError()
    {
        throw new Exception("Test Exception: Bu bir hata testi.");
    }
}