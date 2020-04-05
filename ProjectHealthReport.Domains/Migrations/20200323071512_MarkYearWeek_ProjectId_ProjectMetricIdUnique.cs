using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class MarkYearWeek_ProjectId_ProjectMetricIdUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DoDReports_YearWeek_ProjectId_ProjectMetricId",
                table: "DoDReports",
                columns: new[] { "YearWeek", "ProjectId", "ProjectMetricId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoDReports_YearWeek_ProjectId_ProjectMetricId",
                table: "DoDReports");
        }
    }
}
