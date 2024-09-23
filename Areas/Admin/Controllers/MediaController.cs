using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class MediaController : Controller
{
    [HttpPost]
    public IActionResult UploadImage(IFormFile upload)
    {
        if (upload != null && upload.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                upload.CopyTo(fileStream);
            }

            var url = "/images/uploads/" + fileName; // Yüklenen dosyanın URL'si
            return Json(new { uploaded = true, url });
        }

        return Json(new { uploaded = false, error = new { message = "Dosya yüklenemedi." } });
    }
}
