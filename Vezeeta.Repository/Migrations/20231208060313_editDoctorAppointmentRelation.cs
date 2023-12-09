using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editDoctorAppointmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed9bb3a7-b25f-4e78-8ea3-cf3d23bef6a7", "AQAAAAIAAYagAAAAEFPJCVmgQR89RjFoCh5UgOSiXoLApZV0j/lazAXu6pU+aiLJk2qGQzdarFiH2Tk4kA==", "35efe42f-797c-49c8-94b6-41adb444be0d" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e52e959-11a7-43b0-893c-e01dd3d2ae81", "AQAAAAIAAYagAAAAELD+TeCS9AkGNCmqWtKEClnFhwqsqH2+JkMm9F9bGqvQFbw4F92d6WCpUwNvLl7ayQ==", "90e83f18-302f-45db-bdec-4fb27356dc4b" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");
        }
    }
}
