using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPrjTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrjTagCongViecId",
                table: "PrjCongViec",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrjComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CongViecId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjComment_PrjCongViec_CongViecId",
                        column: x => x.CongViecId,
                        principalTable: "PrjCongViec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjComment_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjTagComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenTag = table.Column<string>(type: "text", nullable: true),
                    MaTag = table.Column<string>(type: "text", nullable: true),
                    YeuCauXacThuc = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_PrjTagComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjTagComment_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                        column: x => x.DuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjTagComment_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjTagCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenTag = table.Column<string>(type: "text", nullable: true),
                    MaTag = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_PrjTagCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrjTagCongViec_PrjDuAnNvChuyenMon_DuAnNvChuyenMonId",
                        column: x => x.DuAnNvChuyenMonId,
                        principalTable: "PrjDuAnNvChuyenMon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjTagCongViec_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrjCommentPrjTagComment",
                columns: table => new
                {
                    CommentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagCommentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrjCommentPrjTagComment", x => new { x.CommentsId, x.TagCommentsId });
                    table.ForeignKey(
                        name: "FK_PrjCommentPrjTagComment_PrjComment_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "PrjComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrjCommentPrjTagComment_PrjTagComment_TagCommentsId",
                        column: x => x.TagCommentsId,
                        principalTable: "PrjTagComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrjCongViec_PrjTagCongViecId",
                table: "PrjCongViec",
                column: "PrjTagCongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjComment_CongViecId",
                table: "PrjComment",
                column: "CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjComment_TenantId",
                table: "PrjComment",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjCommentPrjTagComment_TagCommentsId",
                table: "PrjCommentPrjTagComment",
                column: "TagCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjTagComment_DuAnNvChuyenMonId",
                table: "PrjTagComment",
                column: "DuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjTagComment_TenantId",
                table: "PrjTagComment",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjTagCongViec_DuAnNvChuyenMonId",
                table: "PrjTagCongViec",
                column: "DuAnNvChuyenMonId");

            migrationBuilder.CreateIndex(
                name: "IX_PrjTagCongViec_TenantId",
                table: "PrjTagCongViec",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrjCongViec_PrjTagCongViec_PrjTagCongViecId",
                table: "PrjCongViec",
                column: "PrjTagCongViecId",
                principalTable: "PrjTagCongViec",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrjCongViec_PrjTagCongViec_PrjTagCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropTable(
                name: "PrjCommentPrjTagComment");

            migrationBuilder.DropTable(
                name: "PrjTagCongViec");

            migrationBuilder.DropTable(
                name: "PrjComment");

            migrationBuilder.DropTable(
                name: "PrjTagComment");

            migrationBuilder.DropIndex(
                name: "IX_PrjCongViec_PrjTagCongViecId",
                table: "PrjCongViec");

            migrationBuilder.DropColumn(
                name: "PrjTagCongViecId",
                table: "PrjCongViec");
        }
    }
}
