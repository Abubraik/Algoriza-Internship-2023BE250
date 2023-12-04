using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Sevices.Models
{
    public class EmailConfiguration
    {
        public string? from { get; set; } 
        public string? smtpServer { get; set; }
        public int port { get; set; }
        public string? userName { get; set; }
        public string? password{ get; set; }
    }
}
