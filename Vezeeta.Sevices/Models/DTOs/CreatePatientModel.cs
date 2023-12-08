using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class CreatePatientModel :AccountModelDto
    {
        public IFormFile? Photo { get; set; }

    }
}
