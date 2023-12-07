using Vezeeta.Sevices.Models;

namespace Vezeeta.Sevices.Services.Interfaces
{
    public interface IMailService
    {
        void TestSendEmail(string type, string username,
            string password, string token, string link);
        void SendEmail(Message message);
    }
}
