using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class EditToNhomRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhomThanhVien_AspNetUsers_MembersId",
                table: "PrjToNhomThanhVien");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhomThanhVien_PrjToNhom_PrjToNhomId",
                table: "PrjToNhomThanhVien");

            migrationBuilder.DropIndex(
                name: "IX_PrjToNhom_TruongNhomId",
                table: "PrjToNhom");

            migrationBuilder.DropColumn(
                name: "TruongNhomId",
                table: "PrjToNhom");

            migrationBuilder.RenameColumn(
                name: "PrjToNhomId",
                table: "PrjToNhomThanhVien",
                newName: "ThanhVienToNhomId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "PrjToNhomThanhVien",
                newName: "ThanhVienId");

            migrationBuilder.RenameIndex(
                name: "IX_PrjToNhomThanhVien_PrjToNhomId",
                table: "PrjToNhomThanhVien",
                newName: "IX_PrjToNhomThanhVien_ThanhVienToNhomId");

            migrationBuilder.CreateTable(
                name: "PrjToNhomLanhDao",
                columns: table => new
                {
                    LanhDaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanhDaoToNhomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjToNhomLanhDao", x => new { x.LanhDaoId, x.LanhDaoToNhomId });
                    table.ForeignKey(
                        name: "FK_PrjToNhomLanhDao_AspNetUsers_LanhDaoId",
                        column: x => x.LanhDaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjToNhomLanhDao_PrjToNhom_LanhDaoToNhomId",
                        column: x => x.LanhDaoToNhomId,
                        principalTable: "PrjToNhom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjToNhomLanhDao_LanhDaoToNhomId",
                table: "PrjToNhomLanhDao",
                column: "LanhDaoToNhomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhomThanhVien_AspNetUsers_ThanhVienId",
                table: "PrjToNhomThanhVien",
                column: "ThanhVienId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhomThanhVien_PrjToNhom_ThanhVienToNhomId",
                table: "PrjToNhomThanhVien",
                column: "ThanhVienToNhomId",
                principalTable: "PrjToNhom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhomThanhVien_AspNetUsers_ThanhVienId",
                table: "PrjToNhomThanhVien");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjToNhomThanhVien_PrjToNhom_ThanhVienToNhomId",
                table: "PrjToNhomThanhVien");

            migrationBuilder.DropTable(
                name: "PrjToNhomLanhDao");

            migrationBuilder.RenameColumn(
                name: "ThanhVienToNhomId",
                table: "PrjToNhomThanhVien",
                newName: "PrjToNhomId");

            migrationBuilder.RenameColumn(
                name: "ThanhVienId",
                table: "PrjToNhomThanhVien",
                newName: "MembersId");

            migrationBuilder.RenameIndex(
                name: "IX_PrjToNhomThanhVien_ThanhVienToNhomId",
                table: "PrjToNhomThanhVien",
                newName: "IX_PrjToNhomThanhVien_PrjToNhomId");

            migrationBuilder.AddColumn<Guid>(
                name: "TruongNhomId",
                table: "PrjToNhom",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrjToNhom_TruongNhomId",
                table: "PrjToNhom",
                column: "TruongNhomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhom_AspNetUsers_TruongNhomId",
                table: "PrjToNhom",
                column: "TruongNhomId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhomThanhVien_AspNetUsers_MembersId",
                table: "PrjToNhomThanhVien",
                column: "MembersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrjToNhomThanhVien_PrjToNhom_PrjToNhomId",
                table: "PrjToNhomThanhVien",
                column: "PrjToNhomId",
                principalTable: "PrjToNhom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
