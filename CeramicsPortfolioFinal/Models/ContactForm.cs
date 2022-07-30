using System.ComponentModel.DataAnnotations;

namespace CeramicsPortfolioFinal.Models;

public class ContactForm
{
    [Required(ErrorMessage = "Musisz podać imię")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Musisz podać email")]
    [EmailAddress(ErrorMessage = "Podany email nie wydaje się być prawidłowy")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Podany numer nie wydaje się być prawidłowy")]
    public string Phone { get; set; } = null;

    [Required(ErrorMessage = "Musisz napisać jakąś wiadomość...")]
    public string Message { get; set; } = string.Empty;

    public string Captcha { get; set; } = string.Empty;
}