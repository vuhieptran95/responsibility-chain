using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RenameDivisionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionResourceAvailability");

            migrationBuilder.DropTable(
                name: "DivisionResourceNeed");

            migrationBuilder.DropTable(
                name: "DivisionResourceSoonAvailable");

            migrationBuilder.DropTable(
                name: "DivisionResourceUpdate");

            migrationBuilder.CreateTable(
                name: "DivisionAvailableResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceName = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    Billable = table.Column<int>(nullable: false),
                    Nonbillable = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionAvailableResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionNeededResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionNeededResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionSoonAvailableResources",
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
                    table.PrimaryKey("PK_DivisionSoonAvailableResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionUpdatedResources",
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
                    table.PrimaryKey("PK_DivisionUpdatedResources", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionAvailableResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionNeededResources_YearWeek_Role_Level_Project",
                table: "DivisionNeededResources",
                columns: new[] { "YearWeek", "Role", "Level", "Project" },
                unique: true,
                filter: "[Level] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionSoonAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionSoonAvailableResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionUpdatedResources_YearWeek_ResourceEmail",
                table: "DivisionUpdatedResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionAvailableResources");

            migrationBuilder.DropTable(
                name: "DivisionNeededResources");

            migrationBuilder.DropTable(
                name: "DivisionSoonAvailableResources");

            migrationBuilder.DropTable(
                name: "DivisionUpdatedResources");

            migrationBuilder.CreateTable(
                name: "DivisionResourceAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Billable = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nonbillable = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceAvailability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionResourceNeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceNeed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionResourceSoonAvailable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Availability = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartingAvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceSoonAvailable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionResourceUpdate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnBoardDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceUpdate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceAvailability_YearWeek_ResourceEmail",
                table: "DivisionResourceAvailability",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceNeed_YearWeek_Role_Level_Project",
                table: "DivisionResourceNeed",
                columns: new[] { "YearWeek", "Role", "Level", "Project" },
                unique: true,
                filter: "[Level] IS NOT NULL");

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
    }
}
