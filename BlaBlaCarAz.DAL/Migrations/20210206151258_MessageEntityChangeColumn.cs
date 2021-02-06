using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class MessageEntityChangeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_AppUserId1",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Rides_RideId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_AppUserId1",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Chat");

            migrationBuilder.AlterColumn<long>(
                name: "RideId",
                table: "Chat",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AppUserId",
                table: "Chat",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_AppUserId",
                table: "Chat",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_AppUserId",
                table: "Chat",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Rides_RideId",
                table: "Chat",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_AppUserId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Rides_RideId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_AppUserId",
                table: "Chat");

            migrationBuilder.AlterColumn<long>(
                name: "RideId",
                table: "Chat",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Chat",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AppUserId1",
                table: "Chat",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_AppUserId1",
                table: "Chat",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_AppUserId1",
                table: "Chat",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Rides_RideId",
                table: "Chat",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
