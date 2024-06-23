using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Tenants",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PrjToNhom",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PrjNhomCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PrjLinhVuc",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PrjDuAnNvChuyenMon",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PhongBanBoPhans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmViTriCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmVaiTro",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmTrangThaiChamCong",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmThamSoLuong",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmPhanLoaiNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmNhomChucNang",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmKhuVucChuyenMon",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmGiaiTrinhChamCong",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmDiemDanhHistory",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmCongThucLuong",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmCongKhaiBao",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmChucNang",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "HrmChamCongDiemDanh",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Groups",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CoQuanDonVis",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ChiNhanhVanPhongs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Apps",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrjToNhom");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrjNhomCongViec");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrjLinhVuc");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrjDuAnNvChuyenMon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PhongBanBoPhans");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmViTriCongViec");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmVaiTro");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmTrangThaiChamCong");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmThamSoLuong");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmPhanLoaiNhanSu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmNhomChucNang");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmKhuVucChuyenMon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmGiaiTrinhChamCong");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmDiemDanhHistory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmCongThucLuong");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmCongKhaiBao");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmChucNang");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CoQuanDonVis");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChiNhanhVanPhongs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Apps");
        }
    }
}
