using Microsoft.AspNetCore.Mvc;
using VTP_9.Models;

namespace VTP_9.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly AppDbContext _context;

        public UniversitiesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(University uni)
        {
            await _context.Universities.AddAsync(uni);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Universities.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(University uni)
        {
            University? exist = await _context.Universities.FirstOrDefaultAsync(x => x.Id == uni.Id);
            if (exist == null)
            {
                ModelState.AddModelError("", "This uni not found");
                return View();
            }
            exist.Name = uni.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            University? exist = await _context.Universities.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                ModelState.AddModelError("", "Invalid Input");
                return View();
            }
            _context.Universities.Remove(exist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}