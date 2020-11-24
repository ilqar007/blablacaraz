using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class AddRideNewfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Rides",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassengerCount",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_AppUserId",
                table: "Rides",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_AspNetUsers_AppUserId",
                table: "Rides",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_AspNetUsers_AppUserId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_AppUserId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PassengerCount",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Rides");
        }
    }
}
