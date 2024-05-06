using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecaq.Data.Migrations
{
    /// <inheritdoc />
    public partial class modeAllianceCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceModelId",
                table: "AllianceCollectionModels");

            migrationBuilder.DropIndex(
                name: "IX_AllianceCollectionModels_AllianceModelId",
                table: "AllianceCollectionModels");

            migrationBuilder.DropColumn(
                name: "AllianceModelId",
                table: "AllianceCollectionModels");

            migrationBuilder.AddColumn<int>(
                name: "AllianceId",
                table: "AllianceCollectionModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AllianceCollectionModels_AllianceId",
                table: "AllianceCollectionModels",
                column: "AllianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceId",
                table: "AllianceCollectionModels",
                column: "AllianceId",
                principalTable: "AllianceModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceId",
                table: "AllianceCollectionModels");

            migrationBuilder.DropIndex(
                name: "IX_AllianceCollectionModels_AllianceId",
                table: "AllianceCollectionModels");

            migrationBuilder.DropColumn(
                name: "AllianceId",
                table: "AllianceCollectionModels");

            migrationBuilder.AddColumn<int>(
                name: "AllianceModelId",
                table: "AllianceCollectionModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllianceCollectionModels_AllianceModelId",
                table: "AllianceCollectionModels",
                column: "AllianceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceModelId",
                table: "AllianceCollectionModels",
                column: "AllianceModelId",
                principalTable: "AllianceModels",
                principalColumn: "Id");
        }
    }
}
