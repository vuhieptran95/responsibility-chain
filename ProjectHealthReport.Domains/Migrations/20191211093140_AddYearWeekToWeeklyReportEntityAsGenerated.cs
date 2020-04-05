using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddYearWeekToWeeklyReportEntityAsGenerated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearWeek",
                table: "Statuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearWeek",
                table: "QualityReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearWeek",
                table: "BacklogItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearWeek",
                table: "AdditionalInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearWeek",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "YearWeek",
                table: "QualityReports");

            migrationBuilder.DropColumn(
                name: "YearWeek",
                table: "BacklogItems");

            migrationBuilder.DropColumn(
                name: "YearWeek",
                table: "AdditionalInfos");
        }
    }
}
