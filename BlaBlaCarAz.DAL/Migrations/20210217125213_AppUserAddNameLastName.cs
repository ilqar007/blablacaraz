using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class AppUserAddNameLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameLastName",
                table: "AspNetUsers");
        }
    }
}
