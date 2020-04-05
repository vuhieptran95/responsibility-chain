using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class BlankMigrationToPopulatePHRRequiredFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Projects set PhrRequiredFrom = '2020-01-01' where PhrRequired = 1 and PhrRequiredFrom = null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
