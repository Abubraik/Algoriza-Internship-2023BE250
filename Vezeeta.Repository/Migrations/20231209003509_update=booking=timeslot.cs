using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatebookingtimeslot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "637c0c48-db61-4e11-a73c-0e63d62e54aa", "AQAAAAIAAYagAAAAEI4Ol6jEWq/G9Y1+1HA9pCuf9TsRu6hVrBYgy0LEz6eYBc8HfBX52pTn10OR+bFl+Q==", "83fa9f81-1b07-4ec6-88dc-3e803eefce60" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings",
                column: "TimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a28e9d70-7100-42c4-be9f-af6b216ba451", "AQAAAAIAAYagAAAAEIjLiksecXCuw6+U+aWOuDsO10jhhqLM+UP/ZrRgU7B2eQ0ZpPJkTNznmxKWw60HTw==", "c0051bff-1b70-4a64-b8fa-13fc0b063c73" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings",
                column: "TimeSlotId",
                unique: true);
        }
    }
}
