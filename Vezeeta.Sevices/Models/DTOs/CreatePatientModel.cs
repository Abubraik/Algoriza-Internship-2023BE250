using Microsoft.AspNetCore.Http;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class CreatePatientModel : AccountModelDto
    {
        public IFormFile? Photo { get; set; }

    }
}
