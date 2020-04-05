using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class PopulateProjectsCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update dbo.Projects SET Code = newid()");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
