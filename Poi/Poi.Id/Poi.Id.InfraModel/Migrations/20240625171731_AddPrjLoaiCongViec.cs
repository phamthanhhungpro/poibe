using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPrjLoaiCongViec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjNhomCongViec_NhomCongViecId",
                table: "PrjCongViec");

            migrationBuilder.AlterColumn<Guid>(
                name: "NhomCongViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "LoaiCongViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrjLoaiCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenLoaiCongViec = table.Column<string>(type: "text", nullable: true),
                    MaLoaiCongViec = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    DuAnNvChuyenMonId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjLoaiCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjLoaiCongViec_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                        column: x => x.DuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjLoaiCongViec_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_LoaiCongViecId",
                table: "PrjCongViec",
                column: "LoaiCongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjLoaiCongViec_DuAnNvChuyenMonId",
                table: "PrjLoaiCongViec",
                column: "DuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjLoaiCongViec_TenantId",
                table: "PrjLoaiCongViec",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjLoaiCongViec_LoaiCongViecId",
                table: "PrjCongViec",
                column: "LoaiCongViecId",
                principalTable: "PrjLoaiCongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjNhomCongViec_NhomCongViecId",
                table: "PrjCongViec",
                column: "NhomCongViecId",
                principalTable: "PrjNhomCongViec",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjLoaiCongViec_LoaiCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjNhomCongViec_NhomCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropTable(
                name: "PrjLoaiCongViec");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_LoaiCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "LoaiCongViecId",
                table: "PrjCongViec");

            migrationBuilder.AlterColumn<Guid>(
                name: "NhomCongViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjNhomCongViec_NhomCongViecId",
                table: "PrjCongViec",
                column: "NhomCongViecId",
                principalTable: "PrjNhomCongViec",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
