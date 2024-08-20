using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNguoiXacNhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_UserId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "HrmChamCongDiemDanh",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NguoiXacNhanId",
                table: "HrmChamCongDiemDanh",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmChamCongDiemDanh_NguoiXacNhanId",
                table: "HrmChamCongDiemDanh",
                column: "NguoiXacNhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_NguoiXacNhanId",
                table: "HrmChamCongDiemDanh",
                column: "NguoiXacNhanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_UserId",
                table: "HrmChamCongDiemDanh",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_NguoiXacNhanId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_UserId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropIndex(
                name: "IX_HrmChamCongDiemDanh_NguoiXacNhanId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.DropColumn(
                name: "NguoiXacNhanId",
                table: "HrmChamCongDiemDanh");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "HrmChamCongDiemDanh",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmChamCongDiemDanh_AspNetUsers_UserId",
                table: "HrmChamCongDiemDanh",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
