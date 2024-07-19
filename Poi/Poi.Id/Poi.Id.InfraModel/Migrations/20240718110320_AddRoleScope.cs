using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerFunctionPerRole");

            migrationBuilder.CreateTable(
                name: "PerRoleFunctionScope",
                columns: table => new
                {
                    PerRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerFunctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerScopeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerRoleFunctionScope", x => new { x.PerRoleId, x.PerFunctionId });
                    table.ForeignKey(
                        name: "FK_PerRoleFunctionScope_PerFunction_PerFunctionId",
                        column: x => x.PerFunctionId,
                        principalTable: "PerFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerRoleFunctionScope_PerRole_PerRoleId",
                        column: x => x.PerRoleId,
                        principalTable: "PerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerRoleFunctionScope_PerScope_PerScopeId",
                        column: x => x.PerScopeId,
                        principalTable: "PerScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerRoleFunctionScope_PerFunctionId",
                table: "PerRoleFunctionScope",
                column: "PerFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_PerRoleFunctionScope_PerScopeId",
                table: "PerRoleFunctionScope",
                column: "PerScopeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerRoleFunctionScope");

            migrationBuilder.CreateTable(
                name: "PerFunctionPerRole",
                columns: table => new
                {
                    FunctionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerFunctionPerRole", x => new { x.FunctionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PerFunctionPerRole_PerFunction_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "PerFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerFunctionPerRole_PerRole_RolesId",
                        column: x => x.RolesId,
                        principalTable: "PerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerFunctionPerRole_RolesId",
                table: "PerFunctionPerRole",
                column: "RolesId");
        }
    }
}
