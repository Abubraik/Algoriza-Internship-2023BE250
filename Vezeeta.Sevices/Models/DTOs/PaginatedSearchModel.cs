using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class PaginatedSearchModel
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }

        [Required]
        public required string Search { get; set; }

    }
}
