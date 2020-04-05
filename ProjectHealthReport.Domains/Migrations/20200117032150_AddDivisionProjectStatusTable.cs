using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddDivisionProjectStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DivisionProjectStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StatusColor = table.Column<string>(nullable: false),
                    ProjectStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionProjectStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivisionProjectStatuses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DivisionProjectStatuses_ProjectId",
                table: "DivisionProjectStatuses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionProjectStatuses_YearWeek_ProjectId",
                table: "DivisionProjectStatuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DivisionProjectStatuses");
        }
    }
}
