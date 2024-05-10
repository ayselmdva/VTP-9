using VTP_9.Models;

namespace VTP_9.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly AppDbContext _context;
        public ParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Participants.Include(u=>u.University).Include(q=>q.Qualification).Include(j => j.Job).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifications.ToListAsync();
            ViewBag.Jobs = await _context.Jobs.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Participant participant)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifications.ToListAsync();
            ViewBag.Jobs = await _context.Jobs.ToListAsync();
            if (!ModelState.IsValid) { return View(); }
            if (participant == null) { ModelState.AddModelError("", "Error"); return View(); }
            await _context.AddAsync(participant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifications.ToListAsync();
            ViewBag.Jobs = await _context.Jobs.ToListAsync();
            Participant? participant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
            if (participant == null) { NotFound(); return View(); }
            return View(participant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Participant participant)
        {
            ViewBag.Universities = await _context.Universities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifications.ToListAsync();
            ViewBag.Jobs = await _context.Jobs.ToListAsync();
            Participant? exists = await _context.Participants.FirstOrDefaultAsync(x => x.Id == participant.Id);
            if (exists == null) { NotFound(); return View(); }
            if (participant == null) { NotFound(); return View(); }
            exists.Name = participant.Name;
            exists.Surname = participant.Surname;
            exists.University = participant.University;
            exists.UniversityId = participant.UniversityId;
            exists.Job=participant.Job;
            exists.JobId = participant.JobId;
            exists.Qualification = participant.Qualification;
            exists.QualificationId= participant.QualificationId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Participant? participant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
            if (participant == null) { NotFound(); return View(); }
            _context.Remove(participant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
