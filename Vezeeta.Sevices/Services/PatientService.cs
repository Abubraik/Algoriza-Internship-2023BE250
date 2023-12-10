using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vezeeta.Core.Models;
using Vezeeta.Core.Models.Users;
using Vezeeta.Core.Repositories;
using Vezeeta.Sevices.Models;
using Vezeeta.Sevices.Models.DTOs;
using Vezeeta.Sevices.Services.Interfaces;
using static Vezeeta.Core.Enums.Enums;

namespace Vezeeta.Sevices.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<int> NumOfPatients() => await _unitOfWork.Users.GetAll().OfType<Patient>().CountAsync();
        public async Task<List<PatientModelDto>> GetAllPatients(PaginatedSearchModel paginatedSearch)
        {
            paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);
            var patientQuery = _unitOfWork.Patients.FindAll(p => p.FirstName.Contains(paginatedSearch.Search)
            || p.LastName.Contains(paginatedSearch.Search)
            || p.Email.Contains(paginatedSearch.Search)
            || p.PhoneNumber!.Contains(paginatedSearch.Search)
            , paginatedSearch.PageNumber
            , paginatedSearch.PageSize);
            var patientList = await patientQuery.Select(p => _mapper.Map<PatientModelDto>(p)).ToListAsync();
            return patientList;
        }
        public async Task<PatientModelDto> GetPatientById(string id)
        {
            var patient = await _unitOfWork.Patients.FindAll(p => p.Id == id)
                .Include(e => e.Bookings)!
                .ThenInclude(e => e.Doctor.Specialization).ThenInclude(e=>e.Doctors)
                .ThenInclude(e => e.Appointments)
                .ThenInclude(e => e.DaySchedules)
                .ThenInclude(e => e.TimeSlots)
                .FirstOrDefaultAsync();
            PatientModelDto patientModel = new PatientModelDto()
            {
                Image = patient?.Photo,
                FullName = $"{patient!.FirstName + patient.LastName}",
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber!,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
                Bookings = patient.Bookings?.Select(e => new BookingsInfoDto(new DoctorInfoDto(e.Doctor), e))
                .ToList(),
            };

            return patientModel;
        }


        //public async Task<List<DoctorInfoDto>> SearchForDoctors(PaginatedSearchModel paginatedSearch)
        //{
        //    paginatedSearch.PageNumber = Math.Max(paginatedSearch.PageNumber, 1);

        //    var doctorsQuery = _unitOfWork.Doctors.FindAll(d => d.FirstName.Contains(paginatedSearch.Search)
        //    || d.LastName.Contains(paginatedSearch.Search)
        //    || d.Email.Contains(paginatedSearch.Search)
        //    || d.PhoneNumber!.Contains(paginatedSearch.Search)
        //    , paginatedSearch.PageNumber
        //    , paginatedSearch.PageSize)
        //        .Include(d => d.Appointments)
        //        .ThenInclude(a => a.DaySchedules)
        //    .ThenInclude(ds => ds.TimeSlots);

        //    var doctorList = await doctorsQuery.Select(d => new DoctorInfoDto(d)).ToListAsync();

        //    return doctorList;
        //}
        //public async Task<ApiResponse<string>> BookAppointment(int timeId, string userId, string? discountCode = "")
        //{
        //    var timeSlot = await GetTimeSlotAsync(timeId);
        //    var appointment = await GetAppointmentAsync(timeId);

        //    DiscountCode? discount = null;
        //    if (!string.IsNullOrEmpty(discountCode))
        //    {
        //        discount = await GetDiscountCodeAsync(discountCode);  
        //        if (discount == null) return new ApiResponse<string> { IsSuccess = false, Message = "Discount Code is not valid..!" };
        //    }
        //    if (!ValidateBooking(timeSlot, appointment, out var response))
        //    {
        //        return response;
        //    }
        //    var newBooking = await CreateBooking(userId, timeSlot, appointment, discount);
        //    await _unitOfWork.Bookings.AddAsync(newBooking);
        //    await _unitOfWork.Complete();
        //    return new ApiResponse<string> { IsSuccess = true, Message = "Booked Successfully..!", Response = newBooking.BookingId.ToString() };
        //}

        //public async Task<List<BookingsInfoDto>> GetAllBookings(string userId)
        //{
        //    var patient = await _unitOfWork.Patients.Find(e => e.Email == userId);
        //    var bookings = _unitOfWork.Bookings.FindAll(e => e.PatientId == patient.Id).Include(d => d.Doctor).ThenInclude(s => s.Specialization)
        //        .Include(a => a.Doctor.Appointments)
        //        .ThenInclude(a => a.DaySchedules)
        //        .ThenInclude(ds => ds.TimeSlots);
        //    ;
        //    var doctor = bookings.Select(e => new DoctorInfoDto(e.Doctor)).FirstOrDefault();
        //    var bookingsList = await bookings.Select(b => new BookingsInfoDto(doctor, b)).ToListAsync();
        //    return bookingsList;
        //}

        //public async Task<ApiResponse<string>> CancelAppointment(int bookingId)
        //{
        //    var booking = await _unitOfWork.Bookings.Find(e => e.BookingId == bookingId);
        //    if (booking.Status == Status.Canceled)
        //    {
        //        return new ApiResponse<string> { IsSuccess = false, Message = "Already Canceled..!" };
        //    }
        //    booking.Status = Status.Canceled;
        //    var timeSlot = await _unitOfWork.TimeSlots.Find(t => t.TiemSlotId == booking.TimeSlotId);
        //    timeSlot.IsBooked = false;
        //    await _unitOfWork.Bookings.UpdateAsync(booking);
        //    var affectedRows = await _unitOfWork.Complete();
        //    return new ApiResponse<string> { IsSuccess = true, Message = "Cancelled Successfully..!", Response = bookingId.ToString() };
        //}

        //Helpers
        //private async Task<TimeSlot> GetTimeSlotAsync(int timeId)
        //{
        //    return await _unitOfWork.TimeSlots.Find(e => e.TiemSlotId == timeId);
        //}

        //private async Task<Appointment> GetAppointmentAsync(int timeId)
        //{
        //    return await _unitOfWork.Appointments.Find(e => e.AppointmentId == e.DaySchedules
        //    .FirstOrDefault(d => d.TimeSlots.Any(d => d.TiemSlotId == timeId))!
        //    .AppointmentId);
        //}

        //private async Task<DiscountCode> GetDiscountCodeAsync(string discountCode)
        //{
        //    return await _unitOfWork.DiscountCodes.Find(e => e.Code == discountCode);
        //}

        //private bool ValidateBooking(TimeSlot timeSlot, Appointment appointment, out ApiResponse<string> response)
        //{
        //    if(timeSlot == null || timeSlot.IsBooked)
        //    {
        //        response = new ApiResponse<string> { IsSuccess = false, Message = "TimeSlot is not valid or available..!" };
        //        return false;
        //    }
        //    if (appointment == null)
        //    {
        //        response = new ApiResponse<string> { IsSuccess = false, Message = "Appointment is not valid..!" };
        //        return false;
        //    }
        //    response = new ApiResponse<string> { IsSuccess = true, Message = "Validated Successfully..!" };
        //    return true;
        //}

        //private async Task<Booking> CreateBooking(string userId, TimeSlot timeSlot, Appointment appointment, DiscountCode discountCode)
        //{
        //    var patient = await _unitOfWork.Patients.Find(e => e.Email == userId);
        //    var finalPrice = appointment.Price;
        //    if (discountCode != null)
        //    {
        //        finalPrice = discountCode.DiscountType == DiscountType.Value ?
        //            finalPrice - discountCode.DiscountValue : finalPrice * ((100 - discountCode.DiscountValue) / 100);
        //    }

        //    timeSlot.IsBooked = true;

        //    return new Booking
        //    {
        //        Status = Status.Pending,
        //        PatientId = patient.Id,
        //        DiscountCodeId = discountCode?.DiscountCodeId,
        //        DoctorId = appointment.DoctorId,
        //        TimeSlotId = timeSlot.TiemSlotId,
        //        Price = appointment.Price,
        //        FinalPrice = finalPrice
        //    };
        //}

    }
}
