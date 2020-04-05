using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveUnneccessaryIndexAndRenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_DivisionConcerns_YearWeek_Concerns",
                "DivisionConcerns");
            migrationBuilder.DropIndex("IX_DivisionNeededResources_YearWeek_Role_Level_Project",
                "DivisionNeededResources");
            migrationBuilder.RenameTable("DivisionNeededResources", newName: "DivisionFutureResources");
            migrationBuilder.RenameIndex("PK_DivisionNeededResources", "PK_DivisionFutureResources",
                "DivisionFutureResources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
