using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRepairShop.Migrations
{
    public partial class OwnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Vehicle",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_OwnerId",
                table: "Vehicle",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_OwnerId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_OwnerId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Vehicle");
        }
    }
}
