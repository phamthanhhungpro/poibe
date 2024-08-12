using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDanhGiaCv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DGChapHanhCheDoThongTinBaoCao",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DGChapHanhDieuDongLamThemGio",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DGChatLuongHieuQua",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DGTienDo",
                table: "PrjCongViec",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DGChapHanhCheDoThongTinBaoCao",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "DGChapHanhDieuDongLamThemGio",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "DGChatLuongHieuQua",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "DGTienDo",
                table: "PrjCongViec");
        }
    }
}
