using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddProjectFKToBacklogItemQualityReportStatusAndAdditionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_Projects_ProjectId",
                table: "AdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BacklogItem_Projects_ProjectId",
                table: "BacklogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QualityReport_Projects_ProjectId",
                table: "QualityReport");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Projects_ProjectId",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QualityReport",
                table: "QualityReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BacklogItem",
                table: "BacklogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalInfo",
                table: "AdditionalInfo");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "QualityReport",
                newName: "QualityReports");

            migrationBuilder.RenameTable(
                name: "BacklogItem",
                newName: "BacklogItems");

            migrationBuilder.RenameTable(
                name: "AdditionalInfo",
                newName: "AdditionalInfos");

            migrationBuilder.RenameIndex(
                name: "IX_Status_Week_Year_Id",
                table: "Statuses",
                newName: "IX_Statuses_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Status_ProjectId",
                table: "Statuses",
                newName: "IX_Statuses_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_QualityReport_Week_Year_Id",
                table: "QualityReports",
                newName: "IX_QualityReports_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_QualityReport_ProjectId",
                table: "QualityReports",
                newName: "IX_QualityReports_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_BacklogItem_Week_Year_Id",
                table: "BacklogItems",
                newName: "IX_BacklogItems_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BacklogItem_ProjectId",
                table: "BacklogItems",
                newName: "IX_BacklogItems_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalInfo_Week_Year_Id",
                table: "AdditionalInfos",
                newName: "IX_AdditionalInfos_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalInfo_ProjectId",
                table: "AdditionalInfos",
                newName: "IX_AdditionalInfos_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Statuses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "QualityReports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "BacklogItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "AdditionalInfos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QualityReports",
                table: "QualityReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BacklogItems",
                table: "BacklogItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalInfos",
                table: "AdditionalInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfos_Projects_ProjectId",
                table: "AdditionalInfos",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BacklogItems_Projects_ProjectId",
                table: "BacklogItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QualityReports_Projects_ProjectId",
                table: "QualityReports",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Projects_ProjectId",
                table: "Statuses",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfos_Projects_ProjectId",
                table: "AdditionalInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BacklogItems_Projects_ProjectId",
                table: "BacklogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QualityReports_Projects_ProjectId",
                table: "QualityReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Projects_ProjectId",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QualityReports",
                table: "QualityReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BacklogItems",
                table: "BacklogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalInfos",
                table: "AdditionalInfos");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "QualityReports",
                newName: "QualityReport");

            migrationBuilder.RenameTable(
                name: "BacklogItems",
                newName: "BacklogItem");

            migrationBuilder.RenameTable(
                name: "AdditionalInfos",
                newName: "AdditionalInfo");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_Week_Year_Id",
                table: "Status",
                newName: "IX_Status_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_ProjectId",
                table: "Status",
                newName: "IX_Status_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_QualityReports_Week_Year_Id",
                table: "QualityReport",
                newName: "IX_QualityReport_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_QualityReports_ProjectId",
                table: "QualityReport",
                newName: "IX_QualityReport_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_BacklogItems_Week_Year_Id",
                table: "BacklogItem",
                newName: "IX_BacklogItem_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BacklogItems_ProjectId",
                table: "BacklogItem",
                newName: "IX_BacklogItem_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalInfos_Week_Year_Id",
                table: "AdditionalInfo",
                newName: "IX_AdditionalInfo_Week_Year_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalInfos_ProjectId",
                table: "AdditionalInfo",
                newName: "IX_AdditionalInfo_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Status",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "QualityReport",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "BacklogItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "AdditionalInfo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QualityReport",
                table: "QualityReport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BacklogItem",
                table: "BacklogItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalInfo",
                table: "AdditionalInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_Projects_ProjectId",
                table: "AdditionalInfo",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BacklogItem_Projects_ProjectId",
                table: "BacklogItem",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QualityReport_Projects_ProjectId",
                table: "QualityReport",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Projects_ProjectId",
                table: "Status",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
