using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<string>(maxLength: 63, nullable: true),
                    CommandType = table.Column<string>(maxLength: 255, nullable: true),
                    CommandText = table.Column<string>(nullable: false),
                    Recorded = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionAvailableResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    Billable = table.Column<int>(nullable: false),
                    Nonbillable = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionAvailableResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionConcerns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    Concerns = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionConcerns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionFutureResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionFutureResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionSoonAvailableResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    Availability = table.Column<int>(nullable: false),
                    StartingAvailableDate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionSoonAvailableResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DivisionUpdatedResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    ResourceEmail = table.Column<string>(nullable: false),
                    OnBoardDate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionUpdatedResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(nullable: true),
                    Impact = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    OpenedYearWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    KeyAccountManager = table.Column<string>(nullable: false),
                    DeliveryResponsibleName = table.Column<string>(nullable: false),
                    PlatformVersion = table.Column<string>(nullable: true),
                    JIRALink = table.Column<string>(nullable: false),
                    SourceCodeLink = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ProjectStartDate = table.Column<DateTime>(nullable: false),
                    ProjectEndDate = table.Column<DateTime>(nullable: true),
                    SprintStartDate = table.Column<DateTime>(nullable: true),
                    SprintEndDate = table.Column<DateTime>(nullable: true),
                    NextMileStoneDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    YearWeek = table.Column<int>(nullable: false)
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
                name: "BacklogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Sprint = table.Column<int>(nullable: true),
                    ItemsAdded = table.Column<int>(nullable: false),
                    StoryPointsAdded = table.Column<int>(nullable: true),
                    ItemsDone = table.Column<int>(nullable: false),
                    StoryPointsDone = table.Column<int>(nullable: true),
                    YearWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BacklogItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionProjectStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearWeek = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StatusColor = table.Column<string>(nullable: false),
                    ProjectStatus = table.Column<string>(nullable: true),
                    Actions = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualityReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    CriticalBugs = table.Column<int>(nullable: false),
                    MajorBugs = table.Column<int>(nullable: false),
                    MinorBugs = table.Column<int>(nullable: false),
                    DoneBugs = table.Column<int>(nullable: false),
                    ReOpenBugs = table.Column<int>(nullable: false),
                    YearWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    StatusColor = table.Column<string>(nullable: false),
                    ProjectStatus = table.Column<string>(nullable: true),
                    RetrospectiveFeedBack = table.Column<string>(nullable: true),
                    YearWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statuses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyReportStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    YearWeek = table.Column<int>(nullable: false),
                    IsDeadlineMissed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyReportStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyReportStatuses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfoIssues",
                columns: table => new
                {
                    AdditionalInfoId = table.Column<int>(nullable: false),
                    IssueId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_ProjectId",
                table: "BacklogItems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_YearWeek_ProjectId",
                table: "BacklogItems",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionProjectStatuses_ProjectId",
                table: "DivisionProjectStatuses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionProjectStatuses_YearWeek_ProjectId",
                table: "DivisionProjectStatuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProjectId",
                table: "Milestones",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_Date_ProjectId",
                table: "Milestones",
                columns: new[] { "Date", "ProjectId" },
                unique: true,
                filter: "[Date] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Code",
                table: "Projects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualityReports_ProjectId",
                table: "QualityReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityReports_YearWeek_ProjectId",
                table: "QualityReports",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_ProjectId",
                table: "Statuses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_YearWeek_ProjectId",
                table: "Statuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportStatuses_ProjectId",
                table: "WeeklyReportStatuses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyReportStatuses_YearWeek_ProjectId",
                table: "WeeklyReportStatuses",
                columns: new[] { "YearWeek", "ProjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfoIssues");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BacklogItems");

            migrationBuilder.DropTable(
                name: "DivisionAvailableResources");

            migrationBuilder.DropTable(
                name: "DivisionConcerns");

            migrationBuilder.DropTable(
                name: "DivisionFutureResources");

            migrationBuilder.DropTable(
                name: "DivisionProjectStatuses");

            migrationBuilder.DropTable(
                name: "DivisionSoonAvailableResources");

            migrationBuilder.DropTable(
                name: "DivisionUpdatedResources");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "QualityReports");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "WeeklyReportStatuses");

            migrationBuilder.DropTable(
                name: "AdditionalInfos");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
