using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Sevices.Models;

namespace Vezeeta.Core.Services
{
    public interface IMailService
    {
        void SendEmail(Message message);
    }
}
