using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDivisionResourceUpdateTableAndRenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionResourceSoonAvailable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceName = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    Availability = table.Column<int>(nullable: false),
                    StartingAvailableDate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceSoonAvailable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionResourceUpdate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceName = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    OnBoardDate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceUpdate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceSoonAvailable_YearWeek_ResourceEmail",
                table: "DivisionResourceSoonAvailable",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceUpdate_YearWeek_ResourceEmail",
                table: "DivisionResourceUpdate",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionResourceSoonAvailable");

            migrationBuilder.DropTable(
                name: "DivisionResourceUpdate");
        }
    }
}
