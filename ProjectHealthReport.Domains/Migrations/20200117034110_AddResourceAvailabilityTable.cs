using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddResourceAvailabilityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionResourceAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceName = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    Billable = table.Column<int>(nullable: false),
                    Nonbillable = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionResourceAvailability", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionResourceAvailability_YearWeek_ResourceEmail",
                table: "DivisionResourceAvailability",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionResourceAvailability");
        }
    }
}
