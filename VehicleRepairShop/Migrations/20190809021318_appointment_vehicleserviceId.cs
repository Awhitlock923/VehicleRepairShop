using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRepairShop.Migrations
{
    public partial class appointment_vehicleserviceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleSeviceId",
                table: "AppointmentVehicleServiceLink",
                newName: "VehicleServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentVehicleServiceLink_VehicleServiceId",
                table: "AppointmentVehicleServiceLink",
                column: "VehicleServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentVehicleServiceLink_VehicleService_VehicleServiceId",
                table: "AppointmentVehicleServiceLink",
                column: "VehicleServiceId",
                principalTable: "VehicleService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentVehicleServiceLink_VehicleService_VehicleServiceId",
                table: "AppointmentVehicleServiceLink");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentVehicleServiceLink_VehicleServiceId",
                table: "AppointmentVehicleServiceLink");

            migrationBuilder.RenameColumn(
                name: "VehicleServiceId",
                table: "AppointmentVehicleServiceLink",
                newName: "VehicleSeviceId");
        }
    }
}
