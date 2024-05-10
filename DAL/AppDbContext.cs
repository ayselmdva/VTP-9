using VTP_9.Models;

namespace VTP_9.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }



    }
}
