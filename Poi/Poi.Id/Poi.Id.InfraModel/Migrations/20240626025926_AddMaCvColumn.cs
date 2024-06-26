using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddMaCvColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaNhomCongViec",
                table: "PrjNhomCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaCongViec",
                table: "PrjCongViec",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNhomCongViec",
                table: "PrjNhomCongViec");

            migrationBuilder.DropColumn(
                name: "MaCongViec",
                table: "PrjCongViec");
        }
    }
}
