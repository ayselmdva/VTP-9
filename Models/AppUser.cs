using Microsoft.AspNetCore.Identity;

namespace VTP_9.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
