using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRepairShop.Migrations
{
    public partial class appointment_user_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Appointment",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
