using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDivisionConcernTableAndUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed");

            migrationBuilder.AlterColumn<string>(
                name: "Project",
                table: "DivisionResourceNeed",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "DivisionResourceNeed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DivisionConcerns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    Concerns = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionConcerns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed",
                columns: new[] { "YearWeek", "Role", "Level", "Project" },
                unique: true,
                filter: "[Level] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionConcerns_YearWeek_Concerns",
                table: "DivisionConcerns",
                columns: new[] { "YearWeek", "Concerns" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionConcerns");

            migrationBuilder.DropIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "DivisionResourceNeed");

            migrationBuilder.AlterColumn<string>(
                name: "Project",
                table: "DivisionResourceNeed",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed",
                columns: new[] { "YearWeek", "Role", "Level", "Project" },
                unique: true,
                filter: "[Level] IS NOT NULL AND [Project] IS NOT NULL");
        }
    }
}
