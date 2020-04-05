using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class OrderToMetric_OperatorToThresholds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LowerBoundOperator",
                table: "Thresholds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpperBoundOperator",
                table: "Thresholds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Metrics",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowerBoundOperator",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "UpperBoundOperator",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Metrics");
        }
    }
}
