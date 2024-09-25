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

    public async Task<IActionResult> Index()
    {
        var sliders = await _sliderService.GetAllSlidersAsync();
        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateSliderDto sliderDto, IFormFile ImageFile)
    {
        try
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders");

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
            TempData["Message"] = "Slider başarıyla eklendi.";
            TempData["MessageType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
            return View(sliderDto);
        }
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

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateSliderDto sliderDto, IFormFile ImageFile)
    {
        ModelState.Remove("ImageFile");

        if (ModelState.IsValid)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    if (!string.IsNullOrEmpty(sliderDto.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sliderDto.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    sliderDto.ImageUrl = "/images/sliders/" + fileName;
                }
                else
                {
                    var slider = await _sliderService.GetSliderByIdAsync(sliderDto.Id);
                    sliderDto.ImageUrl = slider.ImageUrl;
                }

                await _sliderService.UpdateSliderAsync(sliderDto);
                TempData["Message"] = "Slider başarıyla güncellendi.";
                TempData["MessageType"] = "info";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
            }
        }

        return View(sliderDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _sliderService.GetSliderByIdAsync(id);
        if (slider == null) return NotFound();
        return View(slider);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _sliderService.DeleteSliderAsync(id);
        TempData["Message"] = "Slider başarıyla silindi.";
        TempData["MessageType"] = "success";
        return RedirectToAction(nameof(Index));
    }
}