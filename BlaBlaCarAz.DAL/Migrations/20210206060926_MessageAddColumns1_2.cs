using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class MessageAddColumns1_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_CreatedUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_CreatedUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedUserId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatedUserId",
                table: "Message",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_CreatedUserId",
                table: "Message",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
