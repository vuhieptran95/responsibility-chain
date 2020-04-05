using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class BlankMigrationSeedingData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Fully loaded','Number','s','Dareboost', NULL, 0)");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('PWA','Number','%','Speed curve', NULL, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
