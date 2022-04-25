using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AdditionalVideoInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Videos",
                newName: "Title");

            migrationBuilder.AddColumn<long>(
                name: "SecondsLength",
                table: "Videos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFilepath",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WatchedCounter",
                table: "Videos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Videos_VideoId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_VideoId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "SecondsLength",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ThumbnailFilepath",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "WatchedCounter",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Videos",
                newName: "Name");
        }
    }
}
