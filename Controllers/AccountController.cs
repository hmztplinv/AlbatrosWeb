using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
{
    var adminUsername = "admin";
    var adminPassword = "password123"; // todo : password hashing

    if (username == adminUsername && password == adminPassword)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl); 
        }
        else
        {
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
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
