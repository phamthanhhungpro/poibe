using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCCDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HrmChamCongDiemDanh",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LyDo",
                table: "HrmChamCongDiemDanh",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropColumn(
                name: "LyDo",
                table: "HrmChamCongDiemDanh");
        }
    }
}
