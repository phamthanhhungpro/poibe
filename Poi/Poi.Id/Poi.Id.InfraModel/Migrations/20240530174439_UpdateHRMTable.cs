using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHRMTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "HrmPhanLoaiNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "HrmKhuVucChuyenMon",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmPhanLoaiNhanSu_TenantId",
                table: "HrmPhanLoaiNhanSu",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmKhuVucChuyenMon_TenantId",
                table: "HrmKhuVucChuyenMon",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmKhuVucChuyenMon_Tenants_TenantId",
                table: "HrmKhuVucChuyenMon",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmPhanLoaiNhanSu_Tenants_TenantId",
                table: "HrmPhanLoaiNhanSu",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmKhuVucChuyenMon_Tenants_TenantId",
                table: "HrmKhuVucChuyenMon");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmPhanLoaiNhanSu_Tenants_TenantId",
                table: "HrmPhanLoaiNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmPhanLoaiNhanSu_TenantId",
                table: "HrmPhanLoaiNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmKhuVucChuyenMon_TenantId",
                table: "HrmKhuVucChuyenMon");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "HrmPhanLoaiNhanSu");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "HrmKhuVucChuyenMon");
        }
    }
}
