using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class TimeSlotDto
    {
        [JsonIgnore]
        public int SlotId { get; set; }
        [Required]
        public required string StartTime { get; set; }
        [Required]
        public required string EndTime { get; set; }
    }

}
