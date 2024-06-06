using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTableGiaiTrinh1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HrmGiaiTrinhChamCong",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HrmChamCongDiemDanhId = table.Column<Guid>(type: "uuid", nullable: true),
                    LyDo = table.Column<string>(type: "text", nullable: true),
                    HrmCongKhaiBaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NguoiXacNhanId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmGiaiTrinhChamCong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrmGiaiTrinhChamCong_AspNetUsers_NguoiXacNhanId",
                        column: x => x.NguoiXacNhanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HrmGiaiTrinhChamCong_HrmChamCongDiemDanh_HrmChamCongDiemDan~",
                        column: x => x.HrmChamCongDiemDanhId,
                        principalTable: "HrmChamCongDiemDanh",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HrmGiaiTrinhChamCong_HrmCongKhaiBao_HrmCongKhaiBaoId",
                        column: x => x.HrmCongKhaiBaoId,
                        principalTable: "HrmCongKhaiBao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrmGiaiTrinhChamCong_HrmChamCongDiemDanhId",
                table: "HrmGiaiTrinhChamCong",
                column: "HrmChamCongDiemDanhId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmGiaiTrinhChamCong_HrmCongKhaiBaoId",
                table: "HrmGiaiTrinhChamCong",
                column: "HrmCongKhaiBaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmGiaiTrinhChamCong_NguoiXacNhanId",
                table: "HrmGiaiTrinhChamCong",
                column: "NguoiXacNhanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrmGiaiTrinhChamCong");
        }
    }
}
