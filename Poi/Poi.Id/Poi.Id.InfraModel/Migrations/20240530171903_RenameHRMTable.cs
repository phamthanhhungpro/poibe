using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class RenameHRMTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoNhanSu");

            migrationBuilder.DropTable(
                name: "KhuVucChuyenMon");

            migrationBuilder.DropTable(
                name: "PhanLoaiNhanSu");

            migrationBuilder.CreateTable(
                name: "HrmHoSoNhanSu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaHoSo = table.Column<string>(type: "text", nullable: true),
                    NgaySinh = table.Column<string>(type: "text", nullable: true),
                    TenKhac = table.Column<string>(type: "text", nullable: true),
                    NoiSinh = table.Column<string>(type: "text", nullable: true),
                    QueQuan = table.Column<string>(type: "text", nullable: true),
                    DanToc = table.Column<string>(type: "text", nullable: true),
                    TonGiao = table.Column<string>(type: "text", nullable: true),
                    ThuongTru = table.Column<string>(type: "text", nullable: true),
                    NoiOHienNay = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThongTinThem = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmHoSoNhanSu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrmHoSoNhanSu_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HrmKhuVucChuyenMon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "text", nullable: true),
                    MaKhuVuc = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmKhuVucChuyenMon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrmPhanLoaiNhanSu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ten = table.Column<string>(type: "text", nullable: true),
                    MaPhanLoai = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmPhanLoaiNhanSu", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_UserId",
                table: "HrmHoSoNhanSu",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HrmHoSoNhanSu");

            migrationBuilder.DropTable(
                name: "HrmKhuVucChuyenMon");

            migrationBuilder.DropTable(
                name: "HrmPhanLoaiNhanSu");

            migrationBuilder.CreateTable(
                name: "HoSoNhanSu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DanToc = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MaHoSo = table.Column<string>(type: "text", nullable: true),
                    NgaySinh = table.Column<string>(type: "text", nullable: true),
                    NoiOHienNay = table.Column<string>(type: "text", nullable: true),
                    NoiSinh = table.Column<string>(type: "text", nullable: true),
                    QueQuan = table.Column<string>(type: "text", nullable: true),
                    TenKhac = table.Column<string>(type: "text", nullable: true),
                    ThongTinThem = table.Column<string>(type: "jsonb", nullable: true),
                    ThuongTru = table.Column<string>(type: "text", nullable: true),
                    TonGiao = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoNhanSu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoSoNhanSu_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KhuVucChuyenMon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MaKhuVuc = table.Column<string>(type: "text", nullable: true),
                    Ten = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuVucChuyenMon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoaiNhanSu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MaPhanLoai = table.Column<string>(type: "text", nullable: true),
                    Ten = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoaiNhanSu", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoSoNhanSu_UserId",
                table: "HoSoNhanSu",
                column: "UserId");
        }
    }
}
