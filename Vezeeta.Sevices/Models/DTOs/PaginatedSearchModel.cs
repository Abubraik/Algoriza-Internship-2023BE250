using System.ComponentModel.DataAnnotations;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class PaginatedSearchModel
    {
        [Required]
        public int pageSize;
        [Required]
        public int pageNumber;
        [Required]
        public required string search;
    }
}
