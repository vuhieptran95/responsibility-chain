using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class BlankMigrationsForSeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 1, 100, 90, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 1, 89, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 1, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 2, 999, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 2, 4999, 1000, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 2, 1000000000, 5000, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 3, 10, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 3, 15, 11, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 3, 1000000000, 16, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 4, 1.0, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 4, 1.2, 1.1, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 4, 1000000000, 1.3, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 5, 100, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 5, 200, 101, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 5, 1000000000, 201, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 6, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 6, 79, 70, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 6, 69, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 7, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 7, 79, 70, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 7, 69, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 8, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 8, 79, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 8, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 9, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (2, 9, 79, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 9, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 10, NULL, NULL, 0, 'NO')");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 10, NULL, NULL, 0, 'YES')");
            
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (1, 11, NULL, NULL, 0, 'NO')");
            migrationBuilder.Sql("INSERT INTO ProjectMetrics VALUES (3, 11, NULL, NULL, 0, 'YES')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
