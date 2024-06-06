using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongBanBoPhanUser");

            migrationBuilder.AddColumn<Guid>(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhongBanBoPhanId1",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId1",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId1",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId1",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhongBanBoPhanId1",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PhongBanBoPhanUser",
                columns: table => new
                {
                    ManagersId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhongBanBoPhansId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanBoPhanUser", x => new { x.ManagersId, x.PhongBanBoPhansId });
                    table.ForeignKey(
                        name: "FK_PhongBanBoPhanUser_AspNetUsers_ManagersId",
                        column: x => x.ManagersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongBanBoPhanUser_PhongBanBoPhans_PhongBanBoPhansId",
                        column: x => x.PhongBanBoPhansId,
                        principalTable: "PhongBanBoPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanBoPhanUser_PhongBanBoPhansId",
                table: "PhongBanBoPhanUser",
                column: "PhongBanBoPhansId");
        }
    }
}
