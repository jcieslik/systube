using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixedVideoCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Videos_VideoId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_VideoId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Videos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_VideoId",
                table: "Videos",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Videos_VideoId",
                table: "Videos",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id");
        }
    }
}
