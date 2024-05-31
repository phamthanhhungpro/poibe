using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poi.Id.InfraModel.Migrations
{
    /// <inheritdoc />
    public partial class Add3table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoQuanDonVis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoQuanDonVis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoQuanDonVis_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChiNhanhVanPhongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CoQuanDonViId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiNhanhVanPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiNhanhVanPhongs_CoQuanDonVis_CoQuanDonViId",
                        column: x => x.CoQuanDonViId,
                        principalTable: "CoQuanDonVis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiNhanhVanPhongs_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhongBanBoPhans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChiNhanhVanPhongId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanBoPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhongBanBoPhans_ChiNhanhVanPhongs_ChiNhanhVanPhongId",
                        column: x => x.ChiNhanhVanPhongId,
                        principalTable: "ChiNhanhVanPhongs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhongBanBoPhans_PhongBanBoPhans_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PhongBanBoPhans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhongBanBoPhans_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiNhanhVanPhongs_CoQuanDonViId",
                table: "ChiNhanhVanPhongs",
                column: "CoQuanDonViId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiNhanhVanPhongs_TenantId",
                table: "ChiNhanhVanPhongs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CoQuanDonVis_TenantId",
                table: "CoQuanDonVis",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanBoPhans_ChiNhanhVanPhongId",
                table: "PhongBanBoPhans",
                column: "ChiNhanhVanPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanBoPhans_ParentId",
                table: "PhongBanBoPhans",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanBoPhans_TenantId",
                table: "PhongBanBoPhans",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers",
                column: "PhongBanBoPhanId",
                principalTable: "PhongBanBoPhans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhongBanBoPhans_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PhongBanBoPhans");

            migrationBuilder.DropTable(
                name: "ChiNhanhVanPhongs");

            migrationBuilder.DropTable(
                name: "CoQuanDonVis");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhongBanBoPhanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhongBanBoPhanId",
                table: "AspNetUsers");
        }
    }
}
