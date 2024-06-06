using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPhongBan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId1",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PhongBanBoPhanId1",
                table: "AspNetUsers",
                newName: "ManagerOfPhongBanBoPhanId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId1",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ManagerOfPhongBanBoPhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                column: "ManagerOfPhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                newName: "PhongBanBoPhanId1");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ManagerOfPhongBanBoPhanId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PhongBanBoPhanId1");

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
    }
}
