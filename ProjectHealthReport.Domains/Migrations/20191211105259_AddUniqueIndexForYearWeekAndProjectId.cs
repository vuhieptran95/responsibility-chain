using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddUniqueIndexForYearWeekAndProjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Statuses_YearWeek_ProjectId",
                table: "Statuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualityReports_YearWeek_ProjectId",
                table: "QualityReports",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_YearWeek_ProjectId",
                table: "BacklogItems",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_YearWeek_ProjectId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_QualityReports_YearWeek_ProjectId",
                table: "QualityReports");

            migrationBuilder.DropIndex(
                name: "IX_BacklogItems_YearWeek_ProjectId",
                table: "BacklogItems");
        }
    }
}
