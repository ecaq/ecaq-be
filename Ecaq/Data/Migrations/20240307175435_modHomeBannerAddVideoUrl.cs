using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecaq.Data.Migrations
{
    /// <inheritdoc />
    public partial class modHomeBannerAddVideoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVideo",
                table: "HomeBanners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VideoDuration",
                table: "HomeBanners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVideo",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "VideoDuration",
                table: "HomeBanners");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "HomeBanners");
        }
    }
}
