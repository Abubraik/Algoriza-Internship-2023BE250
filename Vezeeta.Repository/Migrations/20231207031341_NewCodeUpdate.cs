using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class NewCodeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "DiscountCode",
                newName: "Code");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e52e959-11a7-43b0-893c-e01dd3d2ae81", "AQAAAAIAAYagAAAAELD+TeCS9AkGNCmqWtKEClnFhwqsqH2+JkMm9F9bGqvQFbw4F92d6WCpUwNvLl7ayQ==", "90e83f18-302f-45db-bdec-4fb27356dc4b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "DiscountCode",
                newName: "Discount");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f4e0296-3ca2-4242-9546-f8a2a83e8f5f", "AQAAAAIAAYagAAAAELTgl6Toy7BgDOaL5OSLRRiVB/dokz/8byC3eUFDASwnCiAu1Lvm9gqMm78Q4kbnAw==", "2250efb1-be13-4c5c-918b-bbcbf83f6fb0" });
        }
    }
}
