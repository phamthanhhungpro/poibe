using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class DiemDanhThuCong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HrmGiaiTrinhChamCong",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmChamCongDiemDanh_HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh",
                column: "HrmCongXacNhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmChamCongDiemDanh_HrmCongKhaiBao_HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh",
                column: "HrmCongXacNhanId",
                principalTable: "HrmCongKhaiBao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmChamCongDiemDanh_HrmCongKhaiBao_HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropIndex(
                name: "IX_HrmChamCongDiemDanh_HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HrmGiaiTrinhChamCong");

            migrationBuilder.DropColumn(
                name: "HrmCongXacNhanId",
                table: "HrmChamCongDiemDanh");
        }
    }
}
