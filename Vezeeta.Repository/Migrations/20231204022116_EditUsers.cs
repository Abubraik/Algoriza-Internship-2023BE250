using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EditUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoPath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "photoPath",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photoPath",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "105ede58-4581-4317-9abd-89731456e1d8", "AQAAAAIAAYagAAAAEOZOuzT3ORVgr/QGWhfvrXWYnADb59NhdBLSSW0rHmEQKH6ukY1XyVVpA+/F/iUZ4A==", "6b79d100-e728-4e06-92bd-b4fd35f6ae7d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoPath",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "photoPath",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "photoPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "photoPath" },
                values: new object[] { "09106589-2fff-4a39-977d-93dd324198c6", "AQAAAAIAAYagAAAAEDfN5mbqj43juybdG8GOgH9xp8APebujFexKRY5UG89sPneY1qRzV/9J3gdfUOr0Dw==", "48558978-49c9-43e4-ae10-960fd4984dfc", "Admin" });
        }
    }
}
