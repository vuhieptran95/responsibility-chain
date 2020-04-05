using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveRemainingPropertiesInbacklogItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsRemaining",
                table: "BacklogItems");

            migrationBuilder.DropColumn(
                name: "StoryPointsRemaining",
                table: "BacklogItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsRemaining",
                table: "BacklogItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoryPointsRemaining",
                table: "BacklogItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
