using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRLPrj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjDuAnNvChuyenMon_AspNetUsers_QuanLyDuAnId",
                table: "PrjDuAnNvChuyenMon");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjDuAnNvChuyenMon_AspNetUsers_QuanLyDuAnId",
                table: "PrjDuAnNvChuyenMon",
                column: "QuanLyDuAnId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom",
                column: "TruongNhomId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjDuAnNvChuyenMon_AspNetUsers_QuanLyDuAnId",
                table: "PrjDuAnNvChuyenMon");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjDuAnNvChuyenMon_AspNetUsers_QuanLyDuAnId",
                table: "PrjDuAnNvChuyenMon",
                column: "QuanLyDuAnId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom",
                column: "TruongNhomId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
