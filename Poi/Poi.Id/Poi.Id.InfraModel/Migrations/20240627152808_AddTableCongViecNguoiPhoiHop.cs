using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCongViecNguoiPhoiHop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NguoiGiaoViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NguoiThucHienId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrjCongViecNguoiPhoiHop",
                columns: table => new
                {
                    NguoiPhoiHopId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrjCongViecId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjCongViecNguoiPhoiHop", x => new { x.NguoiPhoiHopId, x.PrjCongViecId });
                    table.ForeignKey(
                        name: "FK_PrjCongViecNguoiPhoiHop_AspNetUsers_NguoiPhoiHopId",
                        column: x => x.NguoiPhoiHopId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjCongViecNguoiPhoiHop_PrjCongViec_PrjCongViecId",
                        column: x => x.PrjCongViecId,
                        principalTable: "PrjCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_NguoiGiaoViecId",
                table: "PrjCongViec",
                column: "NguoiGiaoViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_NguoiThucHienId",
                table: "PrjCongViec",
                column: "NguoiThucHienId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViecNguoiPhoiHop_PrjCongViecId",
                table: "PrjCongViecNguoiPhoiHop",
                column: "PrjCongViecId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiGiaoViecId",
                table: "PrjCongViec",
                column: "NguoiGiaoViecId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiThucHienId",
                table: "PrjCongViec",
                column: "NguoiThucHienId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiGiaoViecId",
                table: "PrjCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_AspNetUsers_NguoiThucHienId",
                table: "PrjCongViec");

            migrationBuilder.DropTable(
                name: "PrjCongViecNguoiPhoiHop");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_NguoiGiaoViecId",
                table: "PrjCongViec");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_NguoiThucHienId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "NguoiGiaoViecId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "NguoiThucHienId",
                table: "PrjCongViec");
        }
    }
}
