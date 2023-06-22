using Microsoft.AspNetCore.Identity;

namespace AppointmentMaker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
