using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddSelectValuesToMetric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectValues",
                table: "Metrics",
                nullable: true);

            migrationBuilder.Sql("Update Metrics set SelectValues='Yes;No' where Tool='Screaming Frog'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectValues",
                table: "Metrics");
        }
    }
}
