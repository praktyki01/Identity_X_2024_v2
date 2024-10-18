using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Identity_X_2024_v2.Services
{
    //dodajemy za pomocą Narzędzia->Manager Pakietów NuGet->Zarządzaj Pakietam NuGet rozwiązania bibliotkę o nazwie MailKit
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string emailto, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Praktyki ETI 2024", "etielitarne2024@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailto));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
            //password: ElitarneEtiMail2024
            smtp.Authenticate("etielitarne2024@gmail.com", "shjqlxnaouvowvxt");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Task.CompletedTask;
        }
    }
}
