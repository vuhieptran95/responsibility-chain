using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Features.Migrations
{
    public partial class ChangeDBSchemaForPolicy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPolicies_PolicyScopes_PolicyScopePolicyId_PolicyScopeScopeId",
                table: "UserPolicies");

            migrationBuilder.DropIndex(
                name: "IX_UserPolicies_PolicyScopePolicyId_PolicyScopeScopeId",
                table: "UserPolicies");

            migrationBuilder.DropColumn(
                name: "PolicyScopePolicyId",
                table: "UserPolicies");

            migrationBuilder.DropColumn(
                name: "PolicyScopeScopeId",
                table: "UserPolicies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PolicyScopePolicyId",
                table: "UserPolicies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyScopeScopeId",
                table: "UserPolicies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_PolicyScopePolicyId_PolicyScopeScopeId",
                table: "UserPolicies",
                columns: new[] { "PolicyScopePolicyId", "PolicyScopeScopeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPolicies_PolicyScopes_PolicyScopePolicyId_PolicyScopeScopeId",
                table: "UserPolicies",
                columns: new[] { "PolicyScopePolicyId", "PolicyScopeScopeId" },
                principalTable: "PolicyScopes",
                principalColumns: new[] { "PolicyId", "ScopeId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
