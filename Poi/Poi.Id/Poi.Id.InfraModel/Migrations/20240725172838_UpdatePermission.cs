using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PerEndpointId",
                table: "PerRoleFunctionScope",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerRoleFunctionScope_PerEndpointId",
                table: "PerRoleFunctionScope",
                column: "PerEndpointId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId",
                table: "PerRoleFunctionScope",
                column: "PerEndpointId",
                principalTable: "PerEndpoint",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId",
                table: "PerRoleFunctionScope");

            migrationBuilder.DropIndex(
                name: "IX_PerRoleFunctionScope_PerEndpointId",
                table: "PerRoleFunctionScope");

            migrationBuilder.DropColumn(
                name: "PerEndpointId",
                table: "PerRoleFunctionScope");
        }
    }
}
