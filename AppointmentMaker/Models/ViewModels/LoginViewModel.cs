using System.ComponentModel.DataAnnotations;

namespace AppointmentMaker.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Šifra")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
