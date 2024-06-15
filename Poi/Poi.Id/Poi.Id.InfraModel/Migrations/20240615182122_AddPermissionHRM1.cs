using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionHRM1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "HrmNhomChucNang",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "HrmNhomChucNang",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HrmNhomChucNang_TenantId",
                table: "HrmNhomChucNang",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmNhomChucNang_Tenants_TenantId",
                table: "HrmNhomChucNang",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmNhomChucNang_Tenants_TenantId",
                table: "HrmNhomChucNang");

            migrationBuilder.DropIndex(
                name: "IX_HrmNhomChucNang_TenantId",
                table: "HrmNhomChucNang");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "HrmNhomChucNang");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "HrmNhomChucNang");
        }
    }
}
