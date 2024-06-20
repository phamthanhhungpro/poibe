using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRLPrj1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PrjDuAnNvChuyenMon_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PrjToNhom_PrjToNhomId",
                table: "AspNetUsers");

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

            migrationBuilder.CreateTable(
                name: "PrjDuAnChuyenMonThanhVienDuAn",
                columns: table => new
                {
                    PrjDuAnNvChuyenMonId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThanhVienDuAnId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjDuAnChuyenMonThanhVienDuAn", x => new { x.PrjDuAnNvChuyenMonId, x.ThanhVienDuAnId });
                    table.ForeignKey(
                        name: "FK_PrjDuAnChuyenMonThanhVienDuAn_AspNetUsers_ThanhVienDuAnId",
                        column: x => x.ThanhVienDuAnId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjDuAnChuyenMonThanhVienDuAn_PrjDuAnNvChuyenMon_PrjDuAnNvC~",
                        column: x => x.PrjDuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjToNhomThanhVien",
                columns: table => new
                {
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrjToNhomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjToNhomThanhVien", x => new { x.MembersId, x.PrjToNhomId });
                    table.ForeignKey(
                        name: "FK_PrjToNhomThanhVien_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjToNhomThanhVien_PrjToNhom_PrjToNhomId",
                        column: x => x.PrjToNhomId,
                        principalTable: "PrjToNhom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjDuAnChuyenMonThanhVienDuAn_ThanhVienDuAnId",
                table: "PrjDuAnChuyenMonThanhVienDuAn",
                column: "ThanhVienDuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjToNhomThanhVien_PrjToNhomId",
                table: "PrjToNhomThanhVien",
                column: "PrjToNhomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrjDuAnChuyenMonThanhVienDuAn");

            migrationBuilder.DropTable(
                name: "PrjToNhomThanhVien");

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrjDuAnNvChuyenMonId",
                table: "AspNetUsers",
                column: "PrjDuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrjToNhomId",
                table: "AspNetUsers",
                column: "PrjToNhomId");

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
    }
}
