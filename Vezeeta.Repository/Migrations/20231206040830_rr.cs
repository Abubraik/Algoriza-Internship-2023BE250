using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class rr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Feedbacks_Booking_BookingId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_DaySchedule_DayScheduleId",
                table: "TimeSlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlot",
                table: "TimeSlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "TimeSlot",
                newName: "TimeSlots");

            migrationBuilder.RenameTable(
                name: "DaySchedule",
                newName: "DaySchedules");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlot_DayScheduleId",
                table: "TimeSlots",
                newName: "IX_TimeSlots_DayScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedule_AppointmentId",
                table: "DaySchedules",
                newName: "IX_DaySchedules_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_TimeSlotId",
                table: "Bookings",
                newName: "IX_Bookings_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PatientId",
                table: "Bookings",
                newName: "IX_Bookings_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DoctorId",
                table: "Bookings",
                newName: "IX_Bookings_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DiscountCodeId",
                table: "Bookings",
                newName: "IX_Bookings_DiscountCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots",
                column: "TiemSlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaySchedules",
                table: "DaySchedules",
                column: "DayScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f4e0296-3ca2-4242-9546-f8a2a83e8f5f", "AQAAAAIAAYagAAAAELTgl6Toy7BgDOaL5OSLRRiVB/dokz/8byC3eUFDASwnCiAu1Lvm9gqMm78Q4kbnAw==", "2250efb1-be13-4c5c-918b-bbcbf83f6fb0" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings",
                column: "DiscountCodeId",
                principalTable: "DiscountCode",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Doctors_DoctorId",
                table: "Bookings",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Patients_PatientId",
                table: "Bookings",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TimeSlots_TimeSlotId",
                table: "Bookings",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "TiemSlotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DaySchedules_Appointments_AppointmentId",
                table: "DaySchedules",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Bookings_BookingId",
                table: "Feedbacks",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_DaySchedules_DayScheduleId",
                table: "TimeSlots",
                column: "DayScheduleId",
                principalTable: "DaySchedules",
                principalColumn: "DayScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Doctors_DoctorId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Patients_PatientId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TimeSlots_TimeSlotId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_DaySchedules_Appointments_AppointmentId",
                table: "DaySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Bookings_BookingId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_DaySchedules_DayScheduleId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaySchedules",
                table: "DaySchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "TimeSlots",
                newName: "TimeSlot");

            migrationBuilder.RenameTable(
                name: "DaySchedules",
                newName: "DaySchedule");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_DayScheduleId",
                table: "TimeSlot",
                newName: "IX_TimeSlot_DayScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_DaySchedules_AppointmentId",
                table: "DaySchedule",
                newName: "IX_DaySchedule_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Booking",
                newName: "IX_Booking_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_PatientId",
                table: "Booking",
                newName: "IX_Booking_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_DoctorId",
                table: "Booking",
                newName: "IX_Booking_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_DiscountCodeId",
                table: "Booking",
                newName: "IX_Booking_DiscountCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointment",
                newName: "IX_Appointment_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlot",
                table: "TimeSlot",
                column: "TiemSlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule",
                column: "DayScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

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
                name: "FK_Feedbacks_Booking_BookingId",
                table: "Feedbacks",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_DaySchedule_DayScheduleId",
                table: "TimeSlot",
                column: "DayScheduleId",
                principalTable: "DaySchedule",
                principalColumn: "DayScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
