using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddOpenedYearWeekToIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpenedYearWeek",
                table: "Issues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenedYearWeek",
                table: "Issues");
        }
    }
}
