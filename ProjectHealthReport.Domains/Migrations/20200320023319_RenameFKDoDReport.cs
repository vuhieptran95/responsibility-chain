using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RenameFKDoDReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "MetricsStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.AlterColumn<int>(
                name: "MetricStatusThresholdId",
                table: "DoDReports",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports",
                column: "MetricStatusThresholdId",
                principalTable: "MetricStatusThresholds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.AlterColumn<int>(
                name: "MetricStatusThresholdId",
                table: "DoDReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MetricsStatusThresholdId",
                table: "DoDReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports",
                column: "MetricStatusThresholdId",
                principalTable: "MetricStatusThresholds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
