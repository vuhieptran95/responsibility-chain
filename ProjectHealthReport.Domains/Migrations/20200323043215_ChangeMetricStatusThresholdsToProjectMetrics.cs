using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class ChangeMetricStatusThresholdsToProjectMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.DropTable(
                name: "MetricStatusThresholds");

            migrationBuilder.DropIndex(
                name: "IX_DoDReports_MetricStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "MetricStatusThresholdId",
                table: "DoDReports");

            migrationBuilder.AddColumn<int>(
                name: "ProjectMetricId",
                table: "DoDReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetricStatusId = table.Column<int>(nullable: false),
                    MetricId = table.Column<int>(nullable: false),
                    UpperBound = table.Column<decimal>(nullable: true),
                    LowerBound = table.Column<decimal>(nullable: true),
                    IsRange = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMetrics_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMetrics_MetricStatuses_MetricStatusId",
                        column: x => x.MetricStatusId,
                        principalTable: "MetricStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoDReports_ProjectMetricId",
                table: "DoDReports",
                column: "ProjectMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMetrics_MetricId",
                table: "ProjectMetrics",
                column: "MetricId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMetrics_MetricStatusId",
                table: "ProjectMetrics",
                column: "MetricStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoDReports_ProjectMetrics_ProjectMetricId",
                table: "DoDReports",
                column: "ProjectMetricId",
                principalTable: "ProjectMetrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_ProjectMetrics_ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.DropTable(
                name: "ProjectMetrics");

            migrationBuilder.DropIndex(
                name: "IX_DoDReports_ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.AddColumn<int>(
                name: "MetricStatusThresholdId",
                table: "DoDReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MetricStatusThresholds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRange = table.Column<bool>(type: "bit", nullable: false),
                    LowerBound = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MetricId = table.Column<int>(type: "int", nullable: false),
                    MetricStatusId = table.Column<int>(type: "int", nullable: false),
                    UpperBound = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricStatusThresholds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetricStatusThresholds_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetricStatusThresholds_MetricStatuses_MetricStatusId",
                        column: x => x.MetricStatusId,
                        principalTable: "MetricStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoDReports_MetricStatusThresholdId",
                table: "DoDReports",
                column: "MetricStatusThresholdId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricStatusThresholds_MetricId",
                table: "MetricStatusThresholds",
                column: "MetricId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricStatusThresholds_MetricStatusId",
                table: "MetricStatusThresholds",
                column: "MetricStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                table: "DoDReports",
                column: "MetricStatusThresholdId",
                principalTable: "MetricStatusThresholds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
