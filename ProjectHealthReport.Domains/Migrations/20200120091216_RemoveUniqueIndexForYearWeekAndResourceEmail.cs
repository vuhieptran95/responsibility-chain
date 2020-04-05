using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveUniqueIndexForYearWeekAndResourceEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DivisionUpdatedResources_YearWeek_ResourceEmail",
                table: "DivisionUpdatedResources");

            migrationBuilder.DropIndex(
                name: "IX_DivisionSoonAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionSoonAvailableResources");

            migrationBuilder.DropIndex(
                name: "IX_DivisionAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionAvailableResources");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionUpdatedResources",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionSoonAvailableResources",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionAvailableResources",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionUpdatedResources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionSoonAvailableResources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ResourceEmail",
                table: "DivisionAvailableResources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_DivisionUpdatedResources_YearWeek_ResourceEmail",
                table: "DivisionUpdatedResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionSoonAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionSoonAvailableResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionAvailableResources_YearWeek_ResourceEmail",
                table: "DivisionAvailableResources",
                columns: new[] { "YearWeek", "ResourceEmail" },
                unique: true);
        }
    }
}
