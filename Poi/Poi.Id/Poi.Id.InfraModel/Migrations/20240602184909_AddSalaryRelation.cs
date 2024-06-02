using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "HrmCongThucLuong",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HrmCongThucLuong_TenantId",
                table: "HrmCongThucLuong",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HrmCongThucLuong_Tenants_TenantId",
                table: "HrmCongThucLuong",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrmCongThucLuong_Tenants_TenantId",
                table: "HrmCongThucLuong");

            migrationBuilder.DropIndex(
                name: "IX_HrmCongThucLuong_TenantId",
                table: "HrmCongThucLuong");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "HrmCongThucLuong");
        }
    }
}
