using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTrangThaiChoDuyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrangThaiChoXacNhan",
                table: "PrjCongViec",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiChoXacNhan",
                table: "PrjCongViec");
        }
    }
}
