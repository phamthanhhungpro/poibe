using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionHRM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HrmChucNang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenChucNang = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Method = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmChucNang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrmNhomChucNang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenNhomChucNang = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmNhomChucNang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrmChucNangHrmNhomChucNang",
                columns: table => new
                {
                    HrmChucNangId = table.Column<Guid>(type: "uuid", nullable: false),
                    HrmNhomChucNangId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmChucNangHrmNhomChucNang", x => new { x.HrmChucNangId, x.HrmNhomChucNangId });
                    table.ForeignKey(
                        name: "FK_HrmChucNangHrmNhomChucNang_HrmChucNang_HrmChucNangId",
                        column: x => x.HrmChucNangId,
                        principalTable: "HrmChucNang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HrmChucNangHrmNhomChucNang_HrmNhomChucNang_HrmNhomChucNangId",
                        column: x => x.HrmNhomChucNangId,
                        principalTable: "HrmNhomChucNang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HrmNhomChucNangHrmVaiTro",
                columns: table => new
                {
                    HrmNhomChucNangId = table.Column<Guid>(type: "uuid", nullable: false),
                    HrmVaiTroId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmNhomChucNangHrmVaiTro", x => new { x.HrmNhomChucNangId, x.HrmVaiTroId });
                    table.ForeignKey(
                        name: "FK_HrmNhomChucNangHrmVaiTro_HrmNhomChucNang_HrmNhomChucNangId",
                        column: x => x.HrmNhomChucNangId,
                        principalTable: "HrmNhomChucNang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HrmNhomChucNangHrmVaiTro_HrmVaiTro_HrmVaiTroId",
                        column: x => x.HrmVaiTroId,
                        principalTable: "HrmVaiTro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrmChucNangHrmNhomChucNang_HrmNhomChucNangId",
                table: "HrmChucNangHrmNhomChucNang",
                column: "HrmNhomChucNangId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmNhomChucNangHrmVaiTro_HrmVaiTroId",
                table: "HrmNhomChucNangHrmVaiTro",
                column: "HrmVaiTroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrmChucNangHrmNhomChucNang");

            migrationBuilder.DropTable(
                name: "HrmNhomChucNangHrmVaiTro");

            migrationBuilder.DropTable(
                name: "HrmChucNang");

            migrationBuilder.DropTable(
                name: "HrmNhomChucNang");
        }
    }
}
