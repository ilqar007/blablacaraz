using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class RideAddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassengerCount",
                table: "Rides",
                newName: "LoadType");

            migrationBuilder.AddColumn<bool>(
                name: "CanBookInstantly",
                table: "Rides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanSeeProfilePicture",
                table: "Rides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBookInstantly",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "CanSeeProfilePicture",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "LoadType",
                table: "Rides",
                newName: "PassengerCount");
        }
    }
}
