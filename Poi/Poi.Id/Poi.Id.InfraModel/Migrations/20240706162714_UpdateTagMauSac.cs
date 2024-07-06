using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagMauSac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MauSac",
                table: "PrjTagCongViec",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MauSac",
                table: "PrjTagComment",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MauSac",
                table: "PrjTagCongViec");

            migrationBuilder.DropColumn(
                name: "MauSac",
                table: "PrjTagComment");
        }
    }
}
