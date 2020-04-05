using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddIndexForWeekYearIdInWeeklyReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Status_Week_Year_Id",
                table: "Status",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_QualityReport_Week_Year_Id",
                table: "QualityReport",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItem_Week_Year_Id",
                table: "BacklogItem",
                columns: new[] { "Week", "Year", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_Week_Year_Id",
                table: "AdditionalInfo",
                columns: new[] { "Week", "Year", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Status_Week_Year_Id",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_QualityReport_Week_Year_Id",
                table: "QualityReport");

            migrationBuilder.DropIndex(
                name: "IX_BacklogItem_Week_Year_Id",
                table: "BacklogItem");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalInfo_Week_Year_Id",
                table: "AdditionalInfo");
        }
    }
}
