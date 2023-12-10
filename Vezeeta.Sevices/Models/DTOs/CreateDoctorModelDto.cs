using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class CreateDoctorModelDto : AccountModelDto
    {
        [Required]
        public required IFormFile Photo { get; set; }
        [Required]
        public required string Specialization { get; set; }
    }
}
