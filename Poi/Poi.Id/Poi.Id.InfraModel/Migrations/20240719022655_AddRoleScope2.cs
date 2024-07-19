using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleScope2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerRole_AspNetUsers_UserId",
                table: "PerRole");

            migrationBuilder.DropIndex(
                name: "IX_PerRole_UserId",
                table: "PerRole");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PerRole");

            migrationBuilder.CreateTable(
                name: "PerRoleUser",
                columns: table => new
                {
                    PerRolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerRoleUser", x => new { x.PerRolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PerRoleUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerRoleUser_PerRole_PerRolesId",
                        column: x => x.PerRolesId,
                        principalTable: "PerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerRoleUser_UsersId",
                table: "PerRoleUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerRoleUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PerRole",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerRole_UserId",
                table: "PerRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerRole_AspNetUsers_UserId",
                table: "PerRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
