﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecaq.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeVideoDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoDuration",
                table: "HomeBanners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoDuration",
                table: "HomeBanners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
