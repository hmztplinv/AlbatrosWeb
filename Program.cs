using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";  // Eğer giriş yapılmadıysa yönlendirme sayfası
            options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz erişim olduğunda yönlendirme sayfası
            //options.LogoutPath = "/Account/Logout"; // Çıkış yapıldığında yönlendirme sayfası
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration süresi
            options.SlidingExpiration = true;  // Oturum süresini her talep ile yeniler
        });

// Yetkilendirme işlemleri için
builder.Services.AddAuthorization(options =>
 {
     options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
 });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AlbatrosPortfoyPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
