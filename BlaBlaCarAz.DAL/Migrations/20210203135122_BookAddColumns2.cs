using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class BookAddColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadType",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "LoadType",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
