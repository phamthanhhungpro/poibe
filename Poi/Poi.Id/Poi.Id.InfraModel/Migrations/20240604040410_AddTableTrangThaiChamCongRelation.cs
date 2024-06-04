using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTrangThaiChamCongRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "HrmTrangThaiChamCong",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmTrangThaiChamCong_TenantId",
                table: "HrmTrangThaiChamCong",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmTrangThaiChamCong_Tenants_TenantId",
                table: "HrmTrangThaiChamCong",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmTrangThaiChamCong_Tenants_TenantId",
                table: "HrmTrangThaiChamCong");

            migrationBuilder.DropIndex(
                name: "IX_HrmTrangThaiChamCong_TenantId",
                table: "HrmTrangThaiChamCong");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "HrmTrangThaiChamCong");
        }
    }
}
