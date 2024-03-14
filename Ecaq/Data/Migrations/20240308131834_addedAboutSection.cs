using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecaq.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAboutSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatNationalityText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatNationalityValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatMembersText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatMembersyValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatChurchesText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatChurchesValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatAllianceText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatAlianceValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutModels");
        }
    }
}
