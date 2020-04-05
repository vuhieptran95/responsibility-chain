using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AlterDoDReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoDReports",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DoDReports");

            migrationBuilder.AddColumn<int>(
                name: "MetricId",
                table: "DoDReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoDReports",
                table: "DoDReports",
                columns: new[] { "MetricId", "ProjectId", "YearWeek" });

            migrationBuilder.AddForeignKey(
                name: "FK_DoDReports_Metrics_MetricId",
                table: "DoDReports",
                column: "MetricId",
                principalTable: "Metrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_Metrics_MetricId",
                table: "DoDReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoDReports",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "MetricId",
                table: "DoDReports");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DoDReports",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoDReports",
                table: "DoDReports",
                column: "Id");
        }
    }
}
