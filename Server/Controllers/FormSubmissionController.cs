using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using AngleSharp.Common;
using BitArmory.ReCaptcha;
using CeramicsPortfolio.Blazor.Models;
using Microsoft.Extensions.Primitives;


namespace ceramics_portfolio.Server.Controllers;

public class FormSubmissionController : Controller
{
    [HttpPost]
    [Route("api/formSubmission")]
    public async Task<IActionResult> SendForm([FromBody]ContactForm contactForm)
    {
        string clientIp = GetClientIpAddress();
        string token = contactForm.Captcha;
        string secret = Environment.GetEnvironmentVariable("RECAPTCHA_SECRET");

        var captchaApi = new ReCaptchaService();
        var result = await captchaApi.Verify3Async(token, clientIp, secret);

        if( !result.IsSuccess || result.Action != "formSubmission" || result.Score < 0.5 )
        {
            return new BadRequestObjectResult(result);
        }

        await SendEmail(contactForm.Name, contactForm.Email, contactForm.Phone, contactForm.Message);

        return Ok();
    }

    public string GetClientIpAddress()
    {
        var proxyIp = HttpContext.Request.Headers.GetOrDefault("x-forwarded-for", "");

        if (!string.IsNullOrWhiteSpace(proxyIp))
            return proxyIp;

        return HttpContext.Connection.RemoteIpAddress.ToString();
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