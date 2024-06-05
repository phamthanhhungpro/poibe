using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class TrangThaiEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HrmChamCongDiemDanh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    HrmTrangThaiChamCongId = table.Column<Guid>(type: "uuid", nullable: true),
                    HrmCongKhaiBaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmChamCongDiemDanh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrmChamCongDiemDanh_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HrmChamCongDiemDanh_HrmCongKhaiBao_HrmCongKhaiBaoId",
                        column: x => x.HrmCongKhaiBaoId,
                        principalTable: "HrmCongKhaiBao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HrmChamCongDiemDanh_HrmTrangThaiChamCong_HrmTrangThaiChamCo~",
                        column: x => x.HrmTrangThaiChamCongId,
                        principalTable: "HrmTrangThaiChamCong",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrmChamCongDiemDanh_HrmCongKhaiBaoId",
                table: "HrmChamCongDiemDanh",
                column: "HrmCongKhaiBaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmChamCongDiemDanh_HrmTrangThaiChamCongId",
                table: "HrmChamCongDiemDanh",
                column: "HrmTrangThaiChamCongId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmChamCongDiemDanh_UserId",
                table: "HrmChamCongDiemDanh",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrmChamCongDiemDanh");

            migrationBuilder.DropTable(
                name: "HrmDiemDanhHistory");

            migrationBuilder.DropTable(
                name: "HrmCongKhaiBao");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "HrmTrangThaiChamCong");
        }
    }
}
