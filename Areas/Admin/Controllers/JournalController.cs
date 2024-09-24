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

    // Journals List
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var journals = await _journalService.GetAllJournalsAsync();
        return View(journals);
    }

    // Create New Journal - GET
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Create New Journal - POST
    [HttpPost]
public async Task<IActionResult> Create(JournalDto journalDto, IFormFile file)
{
    if (file != null && file.Length > 0)
    {
        // Dosyanın yükleneceği dizin
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

        // Eğer yükleme dizini yoksa oluştur
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        // Dosya adı ve tam yolu
        var filePath = Path.Combine(uploads, file.FileName);

        // Dosyayı belirtilen dizine kopyala
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // JournalDto'da FilePath alanına kaydedilen dosya yolunu ekle
        journalDto.FilePath = "/files/" + file.FileName;
    }
    
    // ModelState doğrulaması
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }
        return View(journalDto);
    }

    // Kayıt işlemi
    await _journalService.AddJournalAsync(journalDto);
    return RedirectToAction("Index");
}



    // Edit Journal - GET
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var journal = await _journalService.GetJournalByIdAsync(id);
        if (journal == null)
        {
            return NotFound();
        }

        return View(journal);
    }

    // Edit Journal - POST
    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateJournalDto updateJournalDto)
    {
        if (ModelState.IsValid)
        {
            await _journalService.UpdateJournalAsync(id, updateJournalDto);
            return RedirectToAction("Index");
        }

        return View(updateJournalDto);
    }

    // Delete Journal
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _journalService.DeleteJournalAsync(id);
        return RedirectToAction("Index");
    }
}
