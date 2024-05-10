using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VTP_9.Models;

namespace VTP_9.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly AppDbContext _context;

        public QualificationsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Qualifications.Include(u => u.University).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Qualification qualification)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if (qualification == null) { ModelState.AddModelError("", "Error"); return View(); }
            await _context.AddAsync(qualification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            Qualification? qualification = await _context.Qualifications.FirstOrDefaultAsync(x => x.Id == id);
            if (qualification == null) { NotFound(); return View(); }
            return View(qualification);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Qualification qualification)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            Qualification? exists = await _context.Qualifications.FirstOrDefaultAsync(x => x.Id == qualification.Id);
            if (exists == null) { NotFound(); return View(); }
            if (qualification == null) { NotFound(); return View(); }
            exists.Name = qualification.Name;
            exists.University = qualification.University;
            exists.UniversityId = qualification.UniversityId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Qualification? qualification = await _context.Qualifications.FirstOrDefaultAsync(x => x.Id == id);
            if (qualification == null) { NotFound(); return View(); }
            _context.Remove(qualification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
