using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Sevices.Models.DTOs
{
    public class BookingsInfoDto
    {
        public int BookingId { get; set; }
        public string Image { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public string Day { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Price { get; set; }
        public string? DiscountCode { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
        public BookingsInfoDto(DoctorInfoDto doctorInfo,Booking booking)
        {
            BookingId = booking.BookingId;
            Image = doctorInfo.Image;
            DoctorName = doctorInfo.FullName;
            Specialization = doctorInfo.Specialize;
            Day = booking.Doctor.Appointments.DaySchedules.Where(d=>d.TimeSlots.Any(t=>t.StartTime == booking.TimeSlot.StartTime)).FirstOrDefault().DayOfWeek.ToString();
            Time = booking.TimeSlot.StartTime;
            Price = booking.Price;
            DiscountCode = booking.DiscountCode?.Code;
            FinalPrice = booking.FinalPrice;
            Status = booking.Status.ToString();
        }
    }
}
