using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class RideAddColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Rides",
                newName: "Comment");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommendedPriceOk",
                table: "Rides",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LoadLimits",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecommendedPriceOk",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "LoadLimits",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Rides",
                newName: "Note");
        }
    }
}
