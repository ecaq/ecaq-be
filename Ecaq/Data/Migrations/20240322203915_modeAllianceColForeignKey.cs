using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecaq.Data.Migrations
{
    /// <inheritdoc />
    public partial class modeAllianceColForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceId",
                table: "AllianceCollectionModels");

            migrationBuilder.RenameColumn(
                name: "AllianceId",
                table: "AllianceCollectionModels",
                newName: "AllianceModelId");

            migrationBuilder.RenameIndex(
                name: "IX_AllianceCollectionModels_AllianceId",
                table: "AllianceCollectionModels",
                newName: "IX_AllianceCollectionModels_AllianceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceModelId",
                table: "AllianceCollectionModels",
                column: "AllianceModelId",
                principalTable: "AllianceModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceModelId",
                table: "AllianceCollectionModels");

            migrationBuilder.RenameColumn(
                name: "AllianceModelId",
                table: "AllianceCollectionModels",
                newName: "AllianceId");

            migrationBuilder.RenameIndex(
                name: "IX_AllianceCollectionModels_AllianceModelId",
                table: "AllianceCollectionModels",
                newName: "IX_AllianceCollectionModels_AllianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllianceCollectionModels_AllianceModels_AllianceId",
                table: "AllianceCollectionModels",
                column: "AllianceId",
                principalTable: "AllianceModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
