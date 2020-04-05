using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddInitialItemsAndStoryPointsAndPlatformVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitialItems",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitialStoryPoints",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlatformVersion",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialItems",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "InitialStoryPoints",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PlatformVersion",
                table: "Projects");
        }
    }
}
