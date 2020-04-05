using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddStatus_BacklogItems_QualityReports_AdditionalInfoEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Item = table.Column<string>(nullable: true),
                    Impact = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalInfo_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BacklogItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sprint = table.Column<int>(nullable: false),
                    ItemsAdded = table.Column<int>(nullable: false),
                    StoryPointsAdded = table.Column<int>(nullable: false),
                    ItemsDone = table.Column<int>(nullable: false),
                    StoryPointsDone = table.Column<int>(nullable: false),
                    ItemsRemaining = table.Column<int>(nullable: false),
                    StoryPointsRemaining = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklogItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BacklogItem_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QualityReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriticalBugs = table.Column<int>(nullable: false),
                    MajorBugs = table.Column<int>(nullable: false),
                    MinorBugs = table.Column<int>(nullable: false),
                    DoneBugs = table.Column<int>(nullable: false),
                    RemainingBugs = table.Column<int>(nullable: false),
                    ReOpenBugs = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityReport_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusColor = table.Column<string>(nullable: false),
                    ProjectStatus = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_ProjectId",
                table: "AdditionalInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItem_ProjectId",
                table: "BacklogItem",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityReport_ProjectId",
                table: "QualityReport",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_ProjectId",
                table: "Status",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "BacklogItem");

            migrationBuilder.DropTable(
                name: "QualityReport");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
