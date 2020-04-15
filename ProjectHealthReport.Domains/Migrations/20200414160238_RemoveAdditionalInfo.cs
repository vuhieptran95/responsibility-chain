using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveAdditionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfoIssues");

            migrationBuilder.DropTable(
                name: "AdditionalInfos");

            migrationBuilder.DropTable(
                name: "Issues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    YearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalInfos_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Impact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenedYearWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfoIssues",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfoIssues", x => new { x.IssueId, x.AdditionalInfoId });
                    table.ForeignKey(
                        name: "FK_AdditionalInfoIssues_AdditionalInfos_AdditionalInfoId",
                        column: x => x.AdditionalInfoId,
                        principalTable: "AdditionalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalInfoIssues_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfoIssues_AdditionalInfoId",
                table: "AdditionalInfoIssues",
                column: "AdditionalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_ProjectId",
                table: "AdditionalInfos",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_YearWeek_ProjectId",
                table: "AdditionalInfos",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }
    }
}
