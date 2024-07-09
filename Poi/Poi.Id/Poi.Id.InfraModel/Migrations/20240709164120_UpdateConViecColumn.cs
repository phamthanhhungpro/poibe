using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConViecColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MucDoUuTien",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThoiGianDuKien",
                table: "PrjCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiChiTiet",
                table: "PrjCongViec",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "MucDoUuTien",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "ThoiGianDuKien",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "TrangThaiChiTiet",
                table: "PrjCongViec");
        }
    }
}
