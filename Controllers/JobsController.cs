using Microsoft.AspNetCore.Mvc;
using VTP_9.Models;

namespace VTP_9.Controllers
{
    public class JobsController : Controller
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Jobs.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Job job)
        {
            Job? exist = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == job.Id);
            if (exist == null)
            {
                ModelState.AddModelError("", "This job not found");
                return View();
            }
            exist.Name = job.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Job? exist = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                ModelState.AddModelError("", "Invalid Input");
                return View();
            }
            _context.Jobs.Remove(exist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}