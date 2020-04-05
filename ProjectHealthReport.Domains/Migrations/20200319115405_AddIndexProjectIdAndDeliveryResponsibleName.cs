using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddIndexProjectIdAndDeliveryResponsibleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeliveryResponsibleName",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Id_DeliveryResponsibleName",
                table: "Projects",
                columns: new[] { "Id", "DeliveryResponsibleName" },
                unique: true,
                filter: "[DeliveryResponsibleName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_Id_DeliveryResponsibleName",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryResponsibleName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
