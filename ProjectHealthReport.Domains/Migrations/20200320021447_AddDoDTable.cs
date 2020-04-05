using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDoDTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoDReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    MetricsStatusThresholdId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    MetricStatusThresholdId = table.Column<int>(nullable: true),
                    YearWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoDReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoDReports_MetricStatusThresholds_MetricStatusThresholdId",
                        column: x => x.MetricStatusThresholdId,
                        principalTable: "MetricStatusThresholds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoDReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoDReports_MetricStatusThresholdId",
                table: "DoDReports",
                column: "MetricStatusThresholdId");

            migrationBuilder.CreateIndex(
                name: "IX_DoDReports_ProjectId",
                table: "DoDReports",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoDReports");
        }
    }
}
