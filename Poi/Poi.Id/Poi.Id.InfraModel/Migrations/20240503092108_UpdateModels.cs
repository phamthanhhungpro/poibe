using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Tenants_TenantId",
                table: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Apps_TenantId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Apps");

            migrationBuilder.CreateTable(
                name: "AppTenant",
                columns: table => new
                {
                    AppsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTenant", x => new { x.AppsId, x.TenantsId });
                    table.ForeignKey(
                        name: "FK_AppTenant_Apps_AppsId",
                        column: x => x.AppsId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTenant_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTenant_TenantsId",
                table: "AppTenant",
                column: "TenantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTenant");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Apps",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_TenantId",
                table: "Apps",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Tenants_TenantId",
                table: "Apps",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
