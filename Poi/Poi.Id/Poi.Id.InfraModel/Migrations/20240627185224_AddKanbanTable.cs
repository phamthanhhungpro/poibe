using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddKanbanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrjKanban",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenCot = table.Column<string>(type: "text", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    DuAnNvChuyenMonId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrangThaiCongViec = table.Column<int>(type: "integer", nullable: false),
                    ThuTu = table.Column<int>(type: "integer", nullable: false),
                    YeuCauXacNhan = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjKanban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjKanban_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                        column: x => x.DuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjKanban_DuAnNvChuyenMonId",
                table: "PrjKanban",
                column: "DuAnNvChuyenMonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrjKanban");
        }
    }
}
