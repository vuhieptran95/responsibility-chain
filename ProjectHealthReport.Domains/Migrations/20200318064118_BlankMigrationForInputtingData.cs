using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class BlankMigrationForInputtingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Projects set DmrRequiredFrom = ProjectStartDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
