using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveResourceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "DivisionUpdatedResources");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "DivisionSoonAvailableResources");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "DivisionAvailableResources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "DivisionUpdatedResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "DivisionSoonAvailableResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "DivisionAvailableResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
