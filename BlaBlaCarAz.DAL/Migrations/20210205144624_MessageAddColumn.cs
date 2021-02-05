using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class MessageAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RideId",
                table: "Message",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_RideId",
                table: "Message",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Rides_RideId",
                table: "Message",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Rides_RideId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_RideId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Message");
        }
    }
}
