using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class AddChatEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Message");

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RideId = table.Column<long>(type: "bigint", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<long>(type: "bigint", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_AppUserId1",
                table: "Chat",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_RideId",
                table: "Chat",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Message");

            migrationBuilder.AddColumn<long>(
                name: "RideId",
                table: "Message",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Message",
                type: "nvarchar(max)",
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
    }
}
