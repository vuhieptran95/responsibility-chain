using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddIssueAndManyToManyRelationshipWithAdditionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialItems",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "InitialStoryPoints",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(nullable: true),
                    Impact = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    MyProperty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfoIssues",
                columns: table => new
                {
                    AdditionalInfoId = table.Column<int>(nullable: false),
                    IssueId = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfoIssues");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "InitialItems",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitialStoryPoints",
                table: "Projects",
                type: "int",
                nullable: true);
        }
    }
}
