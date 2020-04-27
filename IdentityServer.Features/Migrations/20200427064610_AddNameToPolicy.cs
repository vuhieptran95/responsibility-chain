using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Features.Migrations
{
    public partial class AddNameToPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Policies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Policies");
        }
    }
}
