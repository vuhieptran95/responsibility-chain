using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class BlankMigrationForSeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MetricStatuses VALUES ('GREEN')");
            migrationBuilder.Sql("INSERT INTO MetricStatuses VALUES ('YELLOW')");
            migrationBuilder.Sql("INSERT INTO MetricStatuses VALUES ('RED')");

            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Score','Number',NULL,'Dareboost')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Speed Index','Number','ms','Dareboost')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Improvement','Number',NULL,'Dareboost')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Start render','Number',NULL,'Dareboost')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('First byte','Number','ms','Dareboost')");
            
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Performance','Number','%','Speed curve')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Best Practice','Number','%','Speed curve')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('SEO','Number','%','Speed curve')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Accessibility WCAG 2.1 AA','Number','%','Speed curve')");
            
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('40x, 50x','Text',NULL,'Screaming Frog')");
            migrationBuilder.Sql("INSERT INTO Metrics VALUES ('Image with no alt text','Text',NULL,'Screaming Frog')");

            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 1, 100, 90, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 1, 89, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 1, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 2, 999, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 2, 4999, 1000, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 2, 1000000000, 5000, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 3, 10, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 3, 15, 11, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 3, 1000000000, 16, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 4, 1.0, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 4, 1.2, 1.1, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 4, 1000000000, 1.3, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 5, 100, 0, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 5, 200, 101, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 5, 1000000000, 201, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 6, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 6, 79, 70, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 6, 69, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 7, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 7, 79, 70, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 7, 69, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 8, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 8, 79, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 8, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 9, 100, 80, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (2, 9, 79, 60, 1, NULL)");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 9, 59, 0, 1, NULL)");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 10, NULL, NULL, 0, 'No')");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 10, NULL, NULL, 0, 'Yes')");
            
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (1, 11, NULL, NULL, 0, 'No')");
            migrationBuilder.Sql("INSERT INTO MetricStatusThresholds VALUES (3, 11, NULL, NULL, 0, 'Yes')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
