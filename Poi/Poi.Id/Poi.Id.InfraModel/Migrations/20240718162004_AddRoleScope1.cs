using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleScope1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerRoleFunctionScope_PerScope_PerScopeId",
                table: "PerRoleFunctionScope");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerScopeId",
                table: "PerRoleFunctionScope",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PerRoleFunctionScope_PerScope_PerScopeId",
                table: "PerRoleFunctionScope",
                column: "PerScopeId",
                principalTable: "PerScope",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerRoleFunctionScope_PerScope_PerScopeId",
                table: "PerRoleFunctionScope");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerScopeId",
                table: "PerRoleFunctionScope",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PerRoleFunctionScope_PerScope_PerScopeId",
                table: "PerRoleFunctionScope",
                column: "PerScopeId",
                principalTable: "PerScope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
