using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHRMTableHSNS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VaiTroId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ViTriCongViecId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HrmVaiTro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenVaiTro = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmVaiTro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrmVaiTro_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HrmViTriCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenViTri = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmViTriCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrmViTriCongViec_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu",
                column: "KhuVucChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu",
                column: "PhanLoaiNhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_VaiTroId",
                table: "HrmHoSoNhanSu",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_ViTriCongViecId",
                table: "HrmHoSoNhanSu",
                column: "ViTriCongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmVaiTro_TenantId",
                table: "HrmVaiTro",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HrmViTriCongViec_TenantId",
                table: "HrmViTriCongViec",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmKhuVucChuyenMon_KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu",
                column: "KhuVucChuyenMonId",
                principalTable: "HrmKhuVucChuyenMon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmPhanLoaiNhanSu_PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu",
                column: "PhanLoaiNhanSuId",
                principalTable: "HrmPhanLoaiNhanSu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmVaiTro_VaiTroId",
                table: "HrmHoSoNhanSu",
                column: "VaiTroId",
                principalTable: "HrmVaiTro",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmViTriCongViec_ViTriCongViecId",
                table: "HrmHoSoNhanSu",
                column: "ViTriCongViecId",
                principalTable: "HrmViTriCongViec",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmKhuVucChuyenMon_KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmPhanLoaiNhanSu_PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmVaiTro_VaiTroId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_HrmViTriCongViec_ViTriCongViecId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropTable(
                name: "HrmVaiTro");

            migrationBuilder.DropTable(
                name: "HrmViTriCongViec");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_VaiTroId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_ViTriCongViecId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "KhuVucChuyenMonId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "PhanLoaiNhanSuId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "VaiTroId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecId",
                table: "HrmHoSoNhanSu");
        }
    }
}
