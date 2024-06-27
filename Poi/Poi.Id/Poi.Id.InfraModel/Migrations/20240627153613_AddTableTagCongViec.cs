using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTagCongViec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjTagCongViec_PrjTagCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_PrjTagCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "PrjTagCongViecId",
                table: "PrjCongViec");

            migrationBuilder.CreateTable(
                name: "PrjCongViecPrjTagCongViec",
                columns: table => new
                {
                    CongViecId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagCongViecId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjCongViecPrjTagCongViec", x => new { x.CongViecId, x.TagCongViecId });
                    table.ForeignKey(
                        name: "FK_PrjCongViecPrjTagCongViec_PrjCongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "PrjCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjCongViecPrjTagCongViec_PrjTagCongViec_TagCongViecId",
                        column: x => x.TagCongViecId,
                        principalTable: "PrjTagCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViecPrjTagCongViec_TagCongViecId",
                table: "PrjCongViecPrjTagCongViec",
                column: "TagCongViecId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrjCongViecPrjTagCongViec");

            migrationBuilder.AddColumn<Guid>(
                name: "PrjTagCongViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_PrjTagCongViecId",
                table: "PrjCongViec",
                column: "PrjTagCongViecId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjTagCongViec_PrjTagCongViecId",
                table: "PrjCongViec",
                column: "PrjTagCongViecId",
                principalTable: "PrjTagCongViec",
                principalColumn: "Id");
        }
    }
}
