using System.ComponentModel.DataAnnotations;

namespace CeramicsPortfolio.Blazor.Models;

public class ContactForm
{
    [Required(ErrorMessage = "Musisz podać imię")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Musisz podać email")]
    [EmailAddress(ErrorMessage = "Podany email nie wydaje się być prawidłowy")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Podany numer nie wydaje się być prawidłowy")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Musisz napisać jakąś wiadomość...")]
    public string Message { get; set; }

    public string Captcha { get; set; }
}