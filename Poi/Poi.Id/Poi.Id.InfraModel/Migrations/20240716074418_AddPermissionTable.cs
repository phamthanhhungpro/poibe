using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerEndpoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Method = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    AppCode = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerEndpoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerGroupFunction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AppCode = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerGroupFunction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppCode = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerRole_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerScope",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AppCode = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerScope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerFunction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    GroupFunctionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppCode = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerFunction_PerGroupFunction_GroupFunctionId",
                        column: x => x.GroupFunctionId,
                        principalTable: "PerGroupFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerEndpointPerFunction",
                columns: table => new
                {
                    EndpointsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FunctionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerEndpointPerFunction", x => new { x.EndpointsId, x.FunctionsId });
                    table.ForeignKey(
                        name: "FK_PerEndpointPerFunction_PerEndpoint_EndpointsId",
                        column: x => x.EndpointsId,
                        principalTable: "PerEndpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerEndpointPerFunction_PerFunction_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "PerFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PerFunctionPerScope",
                columns: table => new
                {
                    FunctionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScopesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerFunctionPerScope", x => new { x.FunctionsId, x.ScopesId });
                    table.ForeignKey(
                        name: "FK_PerFunctionPerScope_PerFunction_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "PerFunction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerFunctionPerScope_PerScope_ScopesId",
                        column: x => x.ScopesId,
                        principalTable: "PerScope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerEndpointPerFunction_FunctionsId",
                table: "PerEndpointPerFunction",
                column: "FunctionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PerFunction_GroupFunctionId",
                table: "PerFunction",
                column: "GroupFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_PerFunctionPerRole_RolesId",
                table: "PerFunctionPerRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_PerFunctionPerScope_ScopesId",
                table: "PerFunctionPerScope",
                column: "ScopesId");

            migrationBuilder.CreateIndex(
                name: "IX_PerRole_TenantId",
                table: "PerRole",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PerRole_UserId",
                table: "PerRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerEndpointPerFunction");

            migrationBuilder.DropTable(
                name: "PerFunctionPerRole");

            migrationBuilder.DropTable(
                name: "PerFunctionPerScope");

            migrationBuilder.DropTable(
                name: "PerEndpoint");

            migrationBuilder.DropTable(
                name: "PerRole");

            migrationBuilder.DropTable(
                name: "PerFunction");

            migrationBuilder.DropTable(
                name: "PerScope");

            migrationBuilder.DropTable(
                name: "PerGroupFunction");
        }
    }
}
