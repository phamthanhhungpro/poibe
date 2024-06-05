using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class TrangThaiEnumConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "HrmChamCongDiemDanh",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "HrmChamCongDiemDanh",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
