using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class WeeklyReportStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeeklyReportStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    YearWeek = table.Column<int>(nullable: false),
                    IsDeadlineMissed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyReportStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyReportStatuses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportStatuses_ProjectId",
                table: "WeeklyReportStatuses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportStatuses_YearWeek_ProjectId",
                table: "WeeklyReportStatuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeklyReportStatuses");
        }
    }
}
