using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SliderController : Controller
{
    private readonly ISliderService _sliderService;

    public SliderController(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    // Slider listeleme
    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetAllSlidersAsync();
        return View(sliders); // Slider listeleme view'ı
    }

    // Slider ekleme formu
    public IActionResult Create()
    {
        return View();
    }

    // Slider ekleme işlemi
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSliderDto sliderDto, IFormFile ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var fileName = Path.GetFileName(ImageFile.FileName);
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders");

            // Dizin yoksa oluştur
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            sliderDto.ImageUrl = "/images/sliders/" + fileName;
        }
        else
        {
            ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
            return View(sliderDto);
        }

        await _sliderService.AddSliderAsync(sliderDto);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var slider = await _sliderService.GetSliderByIdAsync(id);
        if (slider == null) return NotFound();
        var updateSliderDto = new UpdateSliderDto
        {
            Id = slider.Id,
            Title = slider.Title,
            ImageUrl = slider.ImageUrl,
            Description = slider.Description
        };

        return View(updateSliderDto);
    }

    // Slider güncelleme işlemi
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateSliderDto sliderDto, IFormFile ImageFile)
{
    // ImageFile boş olsa bile ModelState'i geçerli kabul ediyoruz
    ModelState.Remove("ImageFile");

    if (ModelState.IsValid)
    {
        try
        {
            // Eğer yeni bir dosya yüklendiyse, mevcut dosyayı değiştir
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders");

                // Dizin yoksa oluştur
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                // Dosyayı dizine kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Eski resmi silme işlemi (isteğe bağlı)
                if (!string.IsNullOrEmpty(sliderDto.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sliderDto.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni dosya yolunu veritabanına kaydetmek için ayarlıyoruz
                sliderDto.ImageUrl = "/images/sliders/" + fileName;
            }
            // Eğer yeni bir dosya yüklenmediyse, eski ImageUrl değerini koruyoruz
            else
            {
                var slider = await _sliderService.GetSliderByIdAsync(sliderDto.Id);
                sliderDto.ImageUrl = slider.ImageUrl; // Mevcut resmi saklıyoruz
            }

            // Diğer alanları (Title ve Description) güncelleyip Slider'ı kaydediyoruz
            await _sliderService.UpdateSliderAsync(sliderDto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Hata varsa ekrana veya log'a yaz
            ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
        }
    }

    return View(sliderDto);
}
    // Slider silme işlemi
    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _sliderService.GetSliderByIdAsync(id);
        if (slider == null) return NotFound();
        return View(slider);
    }

    [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _sliderService.DeleteSliderAsync(id);
        return RedirectToAction(nameof(Index));
    }
}