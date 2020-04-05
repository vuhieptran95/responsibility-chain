using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddMileStonesDateAndProjectIdUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Milestones_Date_ProjectId",
                table: "Milestones",
                columns: new[] { "Date", "ProjectId" },
                unique: true,
                filter: "[Date] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Milestones_Date_ProjectId",
                table: "Milestones");
        }
    }
}
