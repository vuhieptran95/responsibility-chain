using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDivisionResourceNeedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionResourceNeed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceNeed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed",
                columns: new[] { "YearWeek", "Role", "Level", "Project" },
                unique: true,
                filter: "[Level] IS NOT NULL AND [Project] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionResourceNeed");
        }
    }
}
