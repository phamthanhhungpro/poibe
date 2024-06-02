using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHSNSTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhongBanBoPhanId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu",
                column: "ChiNhanhVanPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_PhongBanBoPhanId",
                table: "HrmHoSoNhanSu",
                column: "PhongBanBoPhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_ChiNhanhVanPhongs_ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu",
                column: "ChiNhanhVanPhongId",
                principalTable: "ChiNhanhVanPhongs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_PhongBanBoPhans_PhongBanBoPhanId",
                table: "HrmHoSoNhanSu",
                column: "PhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_ChiNhanhVanPhongs_ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_PhongBanBoPhans_PhongBanBoPhanId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_PhongBanBoPhanId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "ChiNhanhVanPhongId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "PhongBanBoPhanId",
                table: "HrmHoSoNhanSu");
        }
    }
}
