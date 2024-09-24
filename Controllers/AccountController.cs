using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Sabit admin kullanıcı bilgileri
        var adminUsername = "admin";
        var adminPassword = "password123"; // Bu şifre daha güvenli şekilde hashlenmeli

        if (username == adminUsername && password == adminPassword)
        {
            // Kullanıcı doğrulandı, cookie'yi oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        TempData["ErrorMessage"] = "Invalid username or password";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        try
        {
             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Logout işlemi sırasında bir hata oluştu: " + ex.Message;
            return RedirectToAction("Login", "Account");
        }
       
    }
}
