using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctors_doctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DiscountCode_discountCodeId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Doctors_doctorId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Patients_patientId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_TimeSlot_timeSlotId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedule_Appointment_appointmentId",
                table: "DaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_specializationId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Booking_bookingId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Doctors_doctorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Patients_patientId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_DaySchedule_dayScheduleId",
                table: "TimeSlot");

            migrationBuilder.RenameColumn(
                name: "startTime",
                table: "TimeSlot",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "isBooked",
                table: "TimeSlot",
                newName: "IsBooked");

            migrationBuilder.RenameColumn(
                name: "endTime",
                table: "TimeSlot",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "dayScheduleId",
                table: "TimeSlot",
                newName: "DayScheduleId");

            migrationBuilder.RenameColumn(
                name: "tiemSlotId",
                table: "TimeSlot",
                newName: "TiemSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlot_dayScheduleId",
                table: "TimeSlot",
                newName: "IX_TimeSlot_DayScheduleId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Specializations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "specializationId",
                table: "Specializations",
                newName: "SpecializationId");

            migrationBuilder.RenameColumn(
                name: "photoPath",
                table: "Patients",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Feedbacks",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "patientId",
                table: "Feedbacks",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "Feedbacks",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "Feedbacks",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "feedbackId",
                table: "Feedbacks",
                newName: "FeedbackId");

            migrationBuilder.RenameColumn(
                name: "feedback",
                table: "Feedbacks",
                newName: "PatientFeedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_patientId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_doctorId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_bookingId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_BookingId");

            migrationBuilder.RenameColumn(
                name: "specializationId",
                table: "Doctors",
                newName: "SpecializationId");

            migrationBuilder.RenameColumn(
                name: "photoPath",
                table: "Doctors",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_specializationId",
                table: "Doctors",
                newName: "IX_Doctors_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "numberOfRequiredBookings",
                table: "DiscountCode",
                newName: "NumberOfRequiredBookings");

            migrationBuilder.RenameColumn(
                name: "isValid",
                table: "DiscountCode",
                newName: "IsValid");

            migrationBuilder.RenameColumn(
                name: "discountValue",
                table: "DiscountCode",
                newName: "DiscountValue");

            migrationBuilder.RenameColumn(
                name: "discountType",
                table: "DiscountCode",
                newName: "DiscountType");

            migrationBuilder.RenameColumn(
                name: "discountCodeId",
                table: "DiscountCode",
                newName: "DiscountCodeId");

            migrationBuilder.RenameColumn(
                name: "discountCode",
                table: "DiscountCode",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "dayOfWeek",
                table: "DaySchedule",
                newName: "DayOfWeek");

            migrationBuilder.RenameColumn(
                name: "appointmentId",
                table: "DaySchedule",
                newName: "AppointmentId");

            migrationBuilder.RenameColumn(
                name: "dayScheduleId",
                table: "DaySchedule",
                newName: "DayScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedule_appointmentId",
                table: "DaySchedule",
                newName: "IX_DaySchedule_AppointmentId");

            migrationBuilder.RenameColumn(
                name: "timeSlotId",
                table: "Booking",
                newName: "TimeSlotId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Booking",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Booking",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "patientId",
                table: "Booking",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "finalPrice",
                table: "Booking",
                newName: "FinalPrice");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "Booking",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "discountCodeId",
                table: "Booking",
                newName: "DiscountCodeId");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "Booking",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_timeSlotId",
                table: "Booking",
                newName: "IX_Booking_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_patientId",
                table: "Booking",
                newName: "IX_Booking_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_doctorId",
                table: "Booking",
                newName: "IX_Booking_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_discountCodeId",
                table: "Booking",
                newName: "IX_Booking_DiscountCodeId");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                table: "AspNetUsers",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Appointment",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "doctorId",
                table: "Appointment",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "appointmentId",
                table: "Appointment",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_doctorId",
                table: "Appointment",
                newName: "IX_Appointment_DoctorId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66b78678-912f-43b6-9e44-9b7a6d167fda", "AQAAAAIAAYagAAAAEEAA9YBWK9qEwnWEP2g21A2ZMp9HWU6hZraDsPV1uHM9kdjXAFLNf4p6hzjKgsROzw==", "1a23e7b1-450a-4f20-b200-26ade2e38313" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctors_DoctorId",
                table: "Appointment",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DiscountCode_DiscountCodeId",
                table: "Booking",
                column: "DiscountCodeId",
                principalTable: "DiscountCode",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Doctors_DoctorId",
                table: "Booking",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Patients_PatientId",
                table: "Booking",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "TiemSlotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedule_Appointment_AppointmentId",
                table: "DaySchedule",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Booking_BookingId",
                table: "Feedbacks",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Doctors_DoctorId",
                table: "Feedbacks",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Patients_PatientId",
                table: "Feedbacks",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_DaySchedule_DayScheduleId",
                table: "TimeSlot",
                column: "DayScheduleId",
                principalTable: "DaySchedule",
                principalColumn: "DayScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctors_DoctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DiscountCode_DiscountCodeId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Doctors_DoctorId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Patients_PatientId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_TimeSlot_TimeSlotId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedule_Appointment_AppointmentId",
                table: "DaySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Booking_BookingId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Doctors_DoctorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Patients_PatientId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_DaySchedule_DayScheduleId",
                table: "TimeSlot");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "TimeSlot",
                newName: "startTime");

            migrationBuilder.RenameColumn(
                name: "IsBooked",
                table: "TimeSlot",
                newName: "isBooked");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "TimeSlot",
                newName: "endTime");

            migrationBuilder.RenameColumn(
                name: "DayScheduleId",
                table: "TimeSlot",
                newName: "dayScheduleId");

            migrationBuilder.RenameColumn(
                name: "TiemSlotId",
                table: "TimeSlot",
                newName: "tiemSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlot_DayScheduleId",
                table: "TimeSlot",
                newName: "IX_TimeSlot_dayScheduleId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Specializations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Specializations",
                newName: "specializationId");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Patients",
                newName: "photoPath");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Feedbacks",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Feedbacks",
                newName: "patientId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Feedbacks",
                newName: "doctorId");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Feedbacks",
                newName: "bookingId");

            migrationBuilder.RenameColumn(
                name: "FeedbackId",
                table: "Feedbacks",
                newName: "feedbackId");

            migrationBuilder.RenameColumn(
                name: "PatientFeedback",
                table: "Feedbacks",
                newName: "feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_patientId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_DoctorId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_BookingId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_bookingId");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Doctors",
                newName: "specializationId");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Doctors",
                newName: "photoPath");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                newName: "IX_Doctors_specializationId");

            migrationBuilder.RenameColumn(
                name: "NumberOfRequiredBookings",
                table: "DiscountCode",
                newName: "numberOfRequiredBookings");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "DiscountCode",
                newName: "isValid");

            migrationBuilder.RenameColumn(
                name: "DiscountValue",
                table: "DiscountCode",
                newName: "discountValue");

            migrationBuilder.RenameColumn(
                name: "DiscountType",
                table: "DiscountCode",
                newName: "discountType");

            migrationBuilder.RenameColumn(
                name: "DiscountCodeId",
                table: "DiscountCode",
                newName: "discountCodeId");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "DiscountCode",
                newName: "discountCode");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "DaySchedule",
                newName: "dayOfWeek");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "DaySchedule",
                newName: "appointmentId");

            migrationBuilder.RenameColumn(
                name: "DayScheduleId",
                table: "DaySchedule",
                newName: "dayScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedule_AppointmentId",
                table: "DaySchedule",
                newName: "IX_DaySchedule_appointmentId");

            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "Booking",
                newName: "timeSlotId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Booking",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Booking",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Booking",
                newName: "patientId");

            migrationBuilder.RenameColumn(
                name: "FinalPrice",
                table: "Booking",
                newName: "finalPrice");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Booking",
                newName: "doctorId");

            migrationBuilder.RenameColumn(
                name: "DiscountCodeId",
                table: "Booking",
                newName: "discountCodeId");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Booking",
                newName: "bookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_TimeSlotId",
                table: "Booking",
                newName: "IX_Booking_timeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PatientId",
                table: "Booking",
                newName: "IX_Booking_patientId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DoctorId",
                table: "Booking",
                newName: "IX_Booking_doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DiscountCodeId",
                table: "Booking",
                newName: "IX_Booking_discountCodeId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "dateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Appointment",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointment",
                newName: "doctorId");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Appointment",
                newName: "appointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                newName: "IX_Appointment_doctorId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "105ede58-4581-4317-9abd-89731456e1d8", "AQAAAAIAAYagAAAAEOZOuzT3ORVgr/QGWhfvrXWYnADb59NhdBLSSW0rHmEQKH6ukY1XyVVpA+/F/iUZ4A==", "6b79d100-e728-4e06-92bd-b4fd35f6ae7d" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctors_doctorId",
                table: "Appointment",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DiscountCode_discountCodeId",
                table: "Booking",
                column: "discountCodeId",
                principalTable: "DiscountCode",
                principalColumn: "discountCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Doctors_doctorId",
                table: "Booking",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Patients_patientId",
                table: "Booking",
                column: "patientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_TimeSlot_timeSlotId",
                table: "Booking",
                column: "timeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "tiemSlotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedule_Appointment_appointmentId",
                table: "DaySchedule",
                column: "appointmentId",
                principalTable: "Appointment",
                principalColumn: "appointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_specializationId",
                table: "Doctors",
                column: "specializationId",
                principalTable: "Specializations",
                principalColumn: "specializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Booking_bookingId",
                table: "Feedbacks",
                column: "bookingId",
                principalTable: "Booking",
                principalColumn: "bookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Doctors_doctorId",
                table: "Feedbacks",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Patients_patientId",
                table: "Feedbacks",
                column: "patientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_DaySchedule_dayScheduleId",
                table: "TimeSlot",
                column: "dayScheduleId",
                principalTable: "DaySchedule",
                principalColumn: "dayScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
