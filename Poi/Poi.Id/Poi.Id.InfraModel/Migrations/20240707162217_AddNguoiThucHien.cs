using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddNguoiThucHien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiThucHienId",
                table: "PrjCongViec");

            migrationBuilder.RenameColumn(
                name: "NguoiThucHienId",
                table: "PrjCongViec",
                newName: "NguoiDuocGiaoId");

            migrationBuilder.RenameIndex(
                name: "IX_PrjCongViec_NguoiThucHienId",
                table: "PrjCongViec",
                newName: "IX_PrjCongViec_NguoiDuocGiaoId");

            migrationBuilder.CreateTable(
                name: "PrjCongViecNguoiThucHien",
                columns: table => new
                {
                    NguoiThucHienId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrjCongViec1Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjCongViecNguoiThucHien", x => new { x.NguoiThucHienId, x.PrjCongViec1Id });
                    table.ForeignKey(
                        name: "FK_PrjCongViecNguoiThucHien_AspNetUsers_NguoiThucHienId",
                        column: x => x.NguoiThucHienId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjCongViecNguoiThucHien_PrjCongViec_PrjCongViec1Id",
                        column: x => x.PrjCongViec1Id,
                        principalTable: "PrjCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViecNguoiThucHien_PrjCongViec1Id",
                table: "PrjCongViecNguoiThucHien",
                column: "PrjCongViec1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiDuocGiaoId",
                table: "PrjCongViec",
                column: "NguoiDuocGiaoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiDuocGiaoId",
                table: "PrjCongViec");

            migrationBuilder.DropTable(
                name: "PrjCongViecNguoiThucHien");

            migrationBuilder.RenameColumn(
                name: "NguoiDuocGiaoId",
                table: "PrjCongViec",
                newName: "NguoiThucHienId");

            migrationBuilder.RenameIndex(
                name: "IX_PrjCongViec_NguoiDuocGiaoId",
                table: "PrjCongViec",
                newName: "IX_PrjCongViec_NguoiThucHienId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiThucHienId",
                table: "PrjCongViec",
                column: "NguoiThucHienId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
