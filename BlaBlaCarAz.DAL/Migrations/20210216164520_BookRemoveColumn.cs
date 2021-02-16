using Microsoft.EntityFrameworkCore.Migrations;

namespace BlaBlaCarAz.DAL.Migrations
{
    public partial class BookRemoveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Rides_RideId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_RideId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RideId1",
                table: "Books");

            migrationBuilder.AlterColumn<long>(
                name: "RideId",
                table: "Books",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Books_RideId",
                table: "Books",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Rides_RideId",
                table: "Books",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Rides_RideId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_RideId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "RideId",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "RideId1",
                table: "Books",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_RideId1",
                table: "Books",
                column: "RideId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Rides_RideId1",
                table: "Books",
                column: "RideId1",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
