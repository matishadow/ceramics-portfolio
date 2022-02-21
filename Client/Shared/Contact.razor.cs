using CeramicsPortfolio.Blazor.Models;
using Microsoft.AspNetCore.Components;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CeramicsPortfolio.Blazor.Shared
{
    public class ContactBase : ComponentBase
    {
        protected ContactForm ContactForm = new();

        [Inject]
        private ISendGridClient Client { get; set; }


        public ContactBase()
        {
        }

        protected async Task HandleValidSubmit()
        {
            ContactForm.Success = null;
            ContactForm.Error = null;
            try
            {
                // Compose a message
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(ContactForm.Email),
                    Subject = $"Wiadomość od {ContactForm.Name}"
                };
                msg.AddContent(MimeType.Html, $"{ContactForm.Message}{$"<br />Numer telefonu: {ContactForm.Phone}"}");
                msg.AddTo(new EmailAddress("receiver@me.com"));

                // Send the message
                var response = await Client.SendEmailAsync(msg);

                // Display status
                ContactForm.Success = "Wiadomość wysłana!";
            }
            catch (Exception ex)
            {
                ContactForm.Error = ex.Message;
            }
            StateHasChanged();
        }

    }
}
