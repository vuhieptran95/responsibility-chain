using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class ChangeIndexFromIdToProjectIdInWeeklyReportTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_Week_Year_Id",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_QualityReports_Week_Year_Id",
                table: "QualityReports");

            migrationBuilder.DropIndex(
                name: "IX_BacklogItems_Week_Year_Id",
                table: "BacklogItems");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalInfos_Week_Year_Id",
                table: "AdditionalInfos");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Week_Year_Id",
                table: "Statuses",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_QualityReports_Week_Year_Id",
                table: "QualityReports",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_Week_Year_Id",
                table: "BacklogItems",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_Week_Year_Id",
                table: "AdditionalInfos",
                columns: new[] { "Week", "Year", "Id" });
        }
    }
}
