using MailKit.Net.Smtp;
using MimeKit;
using Vezeeta.Core.Services;
using Vezeeta.Sevices.Models;

namespace Vezeeta.Sevices.Services
{
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public MailService(EmailConfiguration emailConfiguration)
        {
            this._emailConfiguration = emailConfiguration;
        }
        void IMailService.SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bubraik_Vezeeta", _emailConfiguration.from));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.smtpServer, _emailConfiguration.port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH@");
                client.Authenticate(_emailConfiguration.userName, _emailConfiguration.password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
