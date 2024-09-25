using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AnnouncementController : Controller
{
    private readonly IAnnouncementService _announcementService;
    private readonly IMapper _mapper;

    public AnnouncementController(IAnnouncementService announcementService, IMapper mapper)
    {
        _announcementService = announcementService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var announcements = await _announcementService.GetAllAnnouncementsAsync();
        return View(announcements);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateAnnouncementDto();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnouncementDto createAnnouncementDto)
    {
        if (ModelState.IsValid)
        {
            await _announcementService.AddAnnouncementAsync(createAnnouncementDto);
            TempData["Message"] = "Duyuru başarıyla eklendi.";
            TempData["MessageType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        return View(createAnnouncementDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
        if (announcement == null)
        {
            return NotFound();
        }
        var updateAnnouncementDto = _mapper.Map<UpdateAnnouncementDto>(announcement);

        return View(updateAnnouncementDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateAnnouncementDto updateAnnouncementDto)
    {
        if (ModelState.IsValid)
        {
            await _announcementService.UpdateAnnouncementAsync(updateAnnouncementDto);
            TempData["Message"] = "Duyuru başarıyla güncellendi.";
            TempData["MessageType"] = "info";
            return RedirectToAction(nameof(Index));
        }
        return View(updateAnnouncementDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _announcementService.DeleteAnnouncementAsync(id);
        TempData["Message"] = "Duyuru başarıyla silindi.";
        TempData["MessageType"] = "success";
        return RedirectToAction(nameof(Index));
    }
}