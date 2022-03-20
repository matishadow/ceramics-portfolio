using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using CeramicsPortfolio.Blazor.Models;


namespace ceramics_portfolio.Server.Controllers;

public class FormSubmissionController : Controller
{
    [HttpPost]
    [Route("api/formSubmission")]
    public async Task<IActionResult> SendForm([FromBody]ContactForm contactForm)
    {
        await SendEmail(contactForm.Name, contactForm.Email, contactForm.Phone, contactForm.Message);

        return Ok();
    }

    private async Task SendEmail(string name, string email, string phone, string message)
    {
        string emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
        string emailAddressTo = Environment.GetEnvironmentVariable("EMAIL_ADDRESS_TO");
        var fromAddress = new MailAddress("sisterhoodceramics@gmail.com", "Strona sisterhood-ceramic");
        var toAddress = new MailAddress(emailAddressTo, "Olka");

        const string subject = "Wiadomość z formularza kontaktowego";
        string body = $"Wiadomość od: {name}\n\n" +
                      $"{message}\n\n\n" +
                      $"Adres email: {email}\n";
        if (!string.IsNullOrWhiteSpace(phone))
            body += $"Numer telefonu: {phone}";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, emailPassword)
        };
        using var mailMessage = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        await smtp.SendMailAsync(mailMessage);
    }
}