using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_AspNetUsers_UserId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_UserId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_UserId",
                table: "HrmHoSoNhanSu",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_AspNetUsers_UserId",
                table: "HrmHoSoNhanSu",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmHoSoNhanSu_AspNetUsers_UserId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.DropIndex(
                name: "IX_HrmHoSoNhanSu_UserId",
                table: "HrmHoSoNhanSu");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "HrmHoSoNhanSu",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_HrmHoSoNhanSu_UserId",
                table: "HrmHoSoNhanSu",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmHoSoNhanSu_AspNetUsers_UserId",
                table: "HrmHoSoNhanSu",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
