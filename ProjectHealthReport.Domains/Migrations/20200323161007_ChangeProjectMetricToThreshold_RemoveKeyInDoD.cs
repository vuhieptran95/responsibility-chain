using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class ChangeProjectMetricToThreshold_RemoveKeyInDoD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoDReports_ProjectMetrics_ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.DropTable(
                name: "ProjectMetrics");

            migrationBuilder.DropIndex(
                name: "IX_DoDReports_ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.DropIndex(
                name: "IX_DoDReports_YearWeek_ProjectId_ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.DropColumn(
                name: "ProjectMetricId",
                table: "DoDReports");

            migrationBuilder.CreateTable(
                name: "Thresholds",
                columns: table => new
                {
                    MetricStatusId = table.Column<int>(nullable: false),
                    MetricId = table.Column<int>(nullable: false),
                    UpperBound = table.Column<decimal>(nullable: true),
                    LowerBound = table.Column<decimal>(nullable: true),
                    IsRange = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thresholds", x => new { x.MetricId, x.MetricStatusId });
                    table.ForeignKey(
                        name: "FK_Thresholds_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thresholds_MetricStatuses_MetricStatusId",
                        column: x => x.MetricStatusId,
                        principalTable: "MetricStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thresholds_MetricStatusId",
                table: "Thresholds",
                column: "MetricStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thresholds");

            migrationBuilder.AddColumn<int>(
                name: "ProjectMetricId",
                table: "DoDReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectMetrics",
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
                name: "IX_DoDReports_YearWeek_ProjectId_ProjectMetricId",
                table: "DoDReports",
                columns: new[] { "YearWeek", "ProjectId", "ProjectMetricId" },
                unique: true);

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
    }
}
