using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddProjectStateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectState",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectStateTypeId",
                table: "Projects",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateTable(
                name: "ProjectStateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStateTypes", x => x.Id);
                });

            migrationBuilder.Sql(@"
                Insert into ProjectStateTypes Values('Preparing') 
                Insert into ProjectStateTypes Values('Active')
                Insert into ProjectStateTypes Values('Closed')");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectStateTypeId",
                table: "Projects",
                column: "ProjectStateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectStateTypes_ProjectStateTypeId",
                table: "Projects",
                column: "ProjectStateTypeId",
                principalTable: "ProjectStateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectStateTypes_ProjectStateTypeId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectStateTypes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectStateTypeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectStateTypeId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectState",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
