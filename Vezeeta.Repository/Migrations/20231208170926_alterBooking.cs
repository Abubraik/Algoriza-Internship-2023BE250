using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Repository.Migrations
{
    /// <inheritdoc />
    public partial class alterBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountCodeId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a28e9d70-7100-42c4-be9f-af6b216ba451", "AQAAAAIAAYagAAAAEIjLiksecXCuw6+U+aWOuDsO10jhhqLM+UP/ZrRgU7B2eQ0ZpPJkTNznmxKWw60HTw==", "c0051bff-1b70-4a64-b8fa-13fc0b063c73" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings",
                column: "DiscountCodeId",
                principalTable: "DiscountCode",
                principalColumn: "DiscountCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountCodeId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed9bb3a7-b25f-4e78-8ea3-cf3d23bef6a7", "AQAAAAIAAYagAAAAEFPJCVmgQR89RjFoCh5UgOSiXoLApZV0j/lazAXu6pU+aiLJk2qGQzdarFiH2Tk4kA==", "35efe42f-797c-49c8-94b6-41adb444be0d" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DiscountCode_DiscountCodeId",
                table: "Bookings",
                column: "DiscountCodeId",
                principalTable: "DiscountCode",
                principalColumn: "DiscountCodeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
