using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class TimeSlotDto
    {
        [JsonIgnore]
        public int SlotId { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
    }

}
