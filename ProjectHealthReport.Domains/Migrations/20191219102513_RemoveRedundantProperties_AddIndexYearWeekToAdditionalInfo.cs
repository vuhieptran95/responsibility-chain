using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveRedundantProperties_AddIndexYearWeekToAdditionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "AdditionalInfos");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "AdditionalInfos");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "AdditionalInfos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AdditionalInfos");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_YearWeek_ProjectId",
                table: "AdditionalInfos",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdditionalInfos_YearWeek_ProjectId",
                table: "AdditionalInfos");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "AdditionalInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "AdditionalInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "AdditionalInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AdditionalInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
