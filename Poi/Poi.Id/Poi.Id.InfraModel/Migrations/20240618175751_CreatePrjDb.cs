using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrjDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrjDuAnNvChuyenMonId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrjToNhomId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrjLinhVuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenLinhVuc = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjLinhVuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjLinhVuc_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjToNhom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenToNhom = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    TruongNhomId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjToNhom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                        column: x => x.TruongNhomId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjToNhom_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjDuAnNvChuyenMon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenDuAn = table.Column<string>(type: "text", nullable: true),
                    MoTaDuAn = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToNhomId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhongBanBoPhanId = table.Column<Guid>(type: "uuid", nullable: true),
                    QuanLyDuAnId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThoiGianBatDau = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LinhVucId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsNhiemVuChuyenMon = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjDuAnNvChuyenMon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjDuAnNvChuyenMon_AspNetUsers_QuanLyDuAnId",
                        column: x => x.QuanLyDuAnId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjDuAnNvChuyenMon_PhongBanBoPhans_PhongBanBoPhanId",
                        column: x => x.PhongBanBoPhanId,
                        principalTable: "PhongBanBoPhans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjDuAnNvChuyenMon_PrjLinhVuc_LinhVucId",
                        column: x => x.LinhVucId,
                        principalTable: "PrjLinhVuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjDuAnNvChuyenMon_PrjToNhom_ToNhomId",
                        column: x => x.ToNhomId,
                        principalTable: "PrjToNhom",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjDuAnNvChuyenMon_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjNhomCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenNhomCongViec = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    DuAnNvChuyenMonId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjNhomCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjNhomCongViec_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                        column: x => x.DuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjNhomCongViec_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenCongViec = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    NhomCongViecId = table.Column<Guid>(type: "uuid", nullable: false),
                    CongViecChaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjCongViec_PrjCongViec_CongViecChaId",
                        column: x => x.CongViecChaId,
                        principalTable: "PrjCongViec",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrjCongViec_PrjNhomCongViec_NhomCongViecId",
                        column: x => x.NhomCongViecId,
                        principalTable: "PrjNhomCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjCongViec_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers",
                column: "PrjDuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrjToNhomId",
                table: "AspNetUsers",
                column: "PrjToNhomId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_CongViecChaId",
                table: "PrjCongViec",
                column: "CongViecChaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_NhomCongViecId",
                table: "PrjCongViec",
                column: "NhomCongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_TenantId",
                table: "PrjCongViec",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnNvChuyenMon_LinhVucId",
                table: "PrjDuAnNvChuyenMon",
                column: "LinhVucId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnNvChuyenMon_PhongBanBoPhanId",
                table: "PrjDuAnNvChuyenMon",
                column: "PhongBanBoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnNvChuyenMon_QuanLyDuAnId",
                table: "PrjDuAnNvChuyenMon",
                column: "QuanLyDuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnNvChuyenMon_TenantId",
                table: "PrjDuAnNvChuyenMon",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnNvChuyenMon_ToNhomId",
                table: "PrjDuAnNvChuyenMon",
                column: "ToNhomId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjLinhVuc_TenantId",
                table: "PrjLinhVuc",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjNhomCongViec_DuAnNvChuyenMonId",
                table: "PrjNhomCongViec",
                column: "DuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjNhomCongViec_TenantId",
                table: "PrjNhomCongViec",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjToNhom_TenantId",
                table: "PrjToNhom",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjToNhom_TruongNhomId",
                table: "PrjToNhom",
                column: "TruongNhomId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PrjDuAnNvChuyenMon_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers",
                column: "PrjDuAnNvChuyenMonId",
                principalTable: "PrjDuAnNvChuyenMon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PrjToNhom_PrjToNhomId",
                table: "AspNetUsers",
                column: "PrjToNhomId",
                principalTable: "PrjToNhom",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PrjDuAnNvChuyenMon_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PrjToNhom_PrjToNhomId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PrjCongViec");

            migrationBuilder.DropTable(
                name: "PrjNhomCongViec");

            migrationBuilder.DropTable(
                name: "PrjDuAnNvChuyenMon");

            migrationBuilder.DropTable(
                name: "PrjLinhVuc");

            migrationBuilder.DropTable(
                name: "PrjToNhom");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrjToNhomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrjDuAnNvChuyenMonId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrjToNhomId",
                table: "AspNetUsers");
        }
    }
}
