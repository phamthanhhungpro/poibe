using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PhongBanLanhDao",
                columns: table => new
                {
                    LanhDaoPhongBanId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuanLyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanLanhDao", x => new { x.LanhDaoPhongBanId, x.QuanLyId });
                    table.ForeignKey(
                        name: "FK_PhongBanLanhDao_AspNetUsers_QuanLyId",
                        column: x => x.QuanLyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongBanLanhDao_PhongBanBoPhans_LanhDaoPhongBanId",
                        column: x => x.LanhDaoPhongBanId,
                        principalTable: "PhongBanBoPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhongBanThanhVien",
                columns: table => new
                {
                    ThanhVienId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThanhVienPhongBanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanThanhVien", x => new { x.ThanhVienId, x.ThanhVienPhongBanId });
                    table.ForeignKey(
                        name: "FK_PhongBanThanhVien_AspNetUsers_ThanhVienId",
                        column: x => x.ThanhVienId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongBanThanhVien_PhongBanBoPhans_ThanhVienPhongBanId",
                        column: x => x.ThanhVienPhongBanId,
                        principalTable: "PhongBanBoPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanLanhDao_QuanLyId",
                table: "PhongBanLanhDao",
                column: "QuanLyId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanThanhVien_ThanhVienPhongBanId",
                table: "PhongBanThanhVien",
                column: "ThanhVienPhongBanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongBanLanhDao");

            migrationBuilder.DropTable(
                name: "PhongBanThanhVien");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                column: "ManagerOfPhongBanBoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                column: "ManagerOfPhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
