using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Features.Migrations
{
    public partial class ChangeDBSchemaForPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserScopes");

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyScopes",
                columns: table => new
                {
                    PolicyId = table.Column<string>(nullable: false),
                    ScopeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyScopes", x => new { x.PolicyId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_PolicyScopes_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyScopes_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPolicies",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    PolicyId = table.Column<string>(nullable: false),
                    PolicyScopePolicyId = table.Column<string>(nullable: true),
                    PolicyScopeScopeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicies", x => new { x.Username, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_UserPolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPolicies_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPolicies_PolicyScopes_PolicyScopePolicyId_PolicyScopeScopeId",
                        columns: x => new { x.PolicyScopePolicyId, x.PolicyScopeScopeId },
                        principalTable: "PolicyScopes",
                        principalColumns: new[] { "PolicyId", "ScopeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyScopes_ScopeId",
                table: "PolicyScopes",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_PolicyId",
                table: "UserPolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_PolicyScopePolicyId_PolicyScopeScopeId",
                table: "UserPolicies",
                columns: new[] { "PolicyScopePolicyId", "PolicyScopeScopeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPolicies");

            migrationBuilder.DropTable(
                name: "PolicyScopes");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.CreateTable(
                name: "UserScopes",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScopeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScopes", x => new { x.Username, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_UserScopes_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserScopes_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScopes_ScopeId",
                table: "UserScopes",
                column: "ScopeId");
        }
    }
}
