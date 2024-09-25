using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class JournalController : Controller
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var journals = await _journalService.GetAllJournalsAsync();
        return View(journals);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JournalDto journalDto, IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            journalDto.FilePath = "/files/" + file.FileName;
        }


        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return View(journalDto);
        }


        await _journalService.AddJournalAsync(journalDto);
        TempData["Message"] = "Bülten başarıyla eklendi.";
        TempData["MessageType"] = "success";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _journalService.DeleteJournalAsync(id);
        TempData["Message"] = "Bülten başarıyla silindi.";
        TempData["MessageType"] = "success";
        return RedirectToAction("Index");
    }
}
