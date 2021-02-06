using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class MessageAddColumns1_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserMessage");

            migrationBuilder.AddColumn<long>(
                name: "CreatedUserId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FromUserId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ToUserId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatedUserId",
                table: "Message",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromUserId",
                table: "Message",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToUserId",
                table: "Message",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_CreatedUserId",
                table: "Message",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_FromUserId",
                table: "Message",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_ToUserId",
                table: "Message",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_CreatedUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_FromUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_ToUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_CreatedUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_FromUserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ToUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Message");

            migrationBuilder.CreateTable(
                name: "AppUserMessage",
                columns: table => new
                {
                    AppUsersId = table.Column<long>(type: "bigint", nullable: false),
                    MessagesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserMessage", x => new { x.AppUsersId, x.MessagesId });
                    table.ForeignKey(
                        name: "FK_AppUserMessage_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserMessage_Message_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserMessage_MessagesId",
                table: "AppUserMessage",
                column: "MessagesId");
        }
    }
}
