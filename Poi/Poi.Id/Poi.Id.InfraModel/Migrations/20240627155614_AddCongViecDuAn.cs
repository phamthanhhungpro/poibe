using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddCongViecDuAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DuAnNvChuyenMonId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_DuAnNvChuyenMonId",
                table: "PrjCongViec",
                column: "DuAnNvChuyenMonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                table: "PrjCongViec",
                column: "DuAnNvChuyenMonId",
                principalTable: "PrjDuAnNvChuyenMon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                table: "PrjCongViec");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_DuAnNvChuyenMonId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "DuAnNvChuyenMonId",
                table: "PrjCongViec");
        }
    }
}
