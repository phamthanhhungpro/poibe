using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermission1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerEndpointPerFunction_PerEndpoint_EndpointsId",
                table: "PerEndpointPerFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_PerEndpointPerFunction_PerFunction_FunctionsId",
                table: "PerEndpointPerFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId",
                table: "PerRoleFunctionScope");

            migrationBuilder.DropIndex(
                name: "IX_PerRoleFunctionScope_PerEndpointId",
                table: "PerRoleFunctionScope");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerEndpointPerFunction",
                table: "PerEndpointPerFunction");

            migrationBuilder.DropColumn(
                name: "PerEndpointId",
                table: "PerRoleFunctionScope");

            migrationBuilder.RenameTable(
                name: "PerEndpointPerFunction",
                newName: "PerFunctionEndPoint");

            migrationBuilder.RenameColumn(
                name: "FunctionsId",
                table: "PerFunctionEndPoint",
                newName: "PerFunctionId");

            migrationBuilder.RenameIndex(
                name: "IX_PerEndpointPerFunction_FunctionsId",
                table: "PerFunctionEndPoint",
                newName: "IX_PerFunctionEndPoint_PerFunctionId");

            migrationBuilder.AddColumn<Guid>(
                name: "MainEndPointId",
                table: "PerFunction",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerFunctionEndPoint",
                table: "PerFunctionEndPoint",
                columns: new[] { "EndpointsId", "PerFunctionId" });

            migrationBuilder.CreateIndex(
                name: "IX_PerFunction_MainEndPointId",
                table: "PerFunction",
                column: "MainEndPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerFunction_PerEndpoint_MainEndPointId",
                table: "PerFunction",
                column: "MainEndPointId",
                principalTable: "PerEndpoint",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerFunctionEndPoint_PerEndpoint_EndpointsId",
                table: "PerFunctionEndPoint",
                column: "EndpointsId",
                principalTable: "PerEndpoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerFunctionEndPoint_PerFunction_PerFunctionId",
                table: "PerFunctionEndPoint",
                column: "PerFunctionId",
                principalTable: "PerFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerFunction_PerEndpoint_MainEndPointId",
                table: "PerFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_PerFunctionEndPoint_PerEndpoint_EndpointsId",
                table: "PerFunctionEndPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_PerFunctionEndPoint_PerFunction_PerFunctionId",
                table: "PerFunctionEndPoint");

            migrationBuilder.DropIndex(
                name: "IX_PerFunction_MainEndPointId",
                table: "PerFunction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerFunctionEndPoint",
                table: "PerFunctionEndPoint");

            migrationBuilder.DropColumn(
                name: "MainEndPointId",
                table: "PerFunction");

            migrationBuilder.RenameTable(
                name: "PerFunctionEndPoint",
                newName: "PerEndpointPerFunction");

            migrationBuilder.RenameColumn(
                name: "PerFunctionId",
                table: "PerEndpointPerFunction",
                newName: "FunctionsId");

            migrationBuilder.RenameIndex(
                name: "IX_PerFunctionEndPoint_PerFunctionId",
                table: "PerEndpointPerFunction",
                newName: "IX_PerEndpointPerFunction_FunctionsId");

            migrationBuilder.AddColumn<Guid>(
                name: "PerEndpointId",
                table: "PerRoleFunctionScope",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerEndpointPerFunction",
                table: "PerEndpointPerFunction",
                columns: new[] { "EndpointsId", "FunctionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PerRoleFunctionScope_PerEndpointId",
                table: "PerRoleFunctionScope",
                column: "PerEndpointId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerEndpointPerFunction_PerEndpoint_EndpointsId",
                table: "PerEndpointPerFunction",
                column: "EndpointsId",
                principalTable: "PerEndpoint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerEndpointPerFunction_PerFunction_FunctionsId",
                table: "PerEndpointPerFunction",
                column: "FunctionsId",
                principalTable: "PerFunction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId",
                table: "PerRoleFunctionScope",
                column: "PerEndpointId",
                principalTable: "PerEndpoint",
                principalColumn: "Id");
        }
    }
}
