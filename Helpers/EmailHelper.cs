using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;


namespace StajIlkProje.Helpers
{
    public static class EmailHelper
    {
        private static readonly string _servername= "smtp.gmail.com";
        private static readonly int port=587;
        private static readonly string _username="eng.sevval.bilgin@gmail.com";
        private static readonly string _password="znfuwqbpdwzcpkaq";
        

        
        public static async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your Name", _username));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_servername, port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_username, _password);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    
                    throw new InvalidOperationException("Failed to send email.", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}