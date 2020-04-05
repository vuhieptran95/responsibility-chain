using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddLinkToReport_DoDReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkToReport",
                table: "DoDReports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkToReport",
                table: "DoDReports");
        }
    }
}
