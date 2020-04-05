using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveUnusedIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_Week_Year_ProjectId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_QualityReports_Week_Year_ProjectId",
                table: "QualityReports");

            migrationBuilder.DropIndex(
                name: "IX_BacklogItems_Week_Year_ProjectId",
                table: "BacklogItems");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalInfos_Week_Year_ProjectId",
                table: "AdditionalInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Week_Year_ProjectId",
                table: "Statuses",
                columns: new[] { "Week", "Year", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_QualityReports_Week_Year_ProjectId",
                table: "QualityReports",
                columns: new[] { "Week", "Year", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_Week_Year_ProjectId",
                table: "BacklogItems",
                columns: new[] { "Week", "Year", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_Week_Year_ProjectId",
                table: "AdditionalInfos",
                columns: new[] { "Week", "Year", "ProjectId" });
        }
    }
}
