using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDoDRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ValueType = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    Tool = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetricStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetricStatusThresholds",
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
                name: "IX_MetricStatusThresholds_MetricId",
                table: "MetricStatusThresholds",
                column: "MetricId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricStatusThresholds_MetricStatusId",
                table: "MetricStatusThresholds",
                column: "MetricStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricStatusThresholds");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "MetricStatuses");
        }
    }
}
