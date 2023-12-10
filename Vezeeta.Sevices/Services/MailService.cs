using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.Routing;
using MimeKit;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Services.Interfaces;

namespace Vezeeta.Sevices.Services
{
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public MailService(EmailConfiguration emailConfiguration, IUrlHelperFactory urlHelperFactory)
        {
            this._emailConfiguration = emailConfiguration;
            this._urlHelperFactory = urlHelperFactory;
        }
        void IMailService.SendEmail(string type, string? username = "",
            string? password = "", string? token = "", string? link = "")
        {
            Message message = new Message(new string[] { username },
                $"Vezeeta {type}",
                MessageContent(type, username, password, token, link));
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bubraik_Vezeeta", _emailConfiguration.From));
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
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH@");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

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

        private string MessageContent(string type, string username, string? password, string? token, string? link = "")
        {
            var content = "";
            if (type == "Confirmation")
            {
                content =
                $@"<h2>Your UserName: {username}</h2><br>
            <h2>Your Password: {password}</h2><br>
            <h3>Please use this <a href=""{link}"">Link</a> to confirm your email</h3>";
            }
            else if (type == "Booking Confirmation")
            {
                content = $@"<h3>Your Booking Number {password} has been successfully booked</h3>
<h4> ThankYou for trusting Vezeeta.";
            }
            else if (type == "BookingCancelation")
            {
                content = $"<h3>Your Booking Number {password} has been cancelled</h3>";
            }
            return content;
        }
    }

}

