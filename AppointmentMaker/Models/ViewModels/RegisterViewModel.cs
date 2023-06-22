using System.ComponentModel.DataAnnotations;

namespace AppointmentMaker.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Šifra")]
        [StringLength(100, ErrorMessage ="{0} mora biti barem {2} znaka duga", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Potvrdi šifru")]
        [Compare("Password", ErrorMessage ="Potvrđivanje se mora podudarati sa iznad unesenom šifrom")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Uloga")]
        public string RoleName { get; set; }

    }
}
