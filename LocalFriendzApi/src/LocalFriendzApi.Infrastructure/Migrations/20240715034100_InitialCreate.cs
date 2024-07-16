using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFriendzApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_CONTACT_TB_AREA_CODE_AreaCodeIdAreaCode",
                table: "TB_CONTACT");

            migrationBuilder.DropTable(
                name: "TB_AREA_CODE");

            migrationBuilder.DropIndex(
                name: "IX_TB_CONTACT_AreaCodeIdAreaCode",
                table: "TB_CONTACT");

            migrationBuilder.DropColumn(
                name: "AreaCodeIdAreaCode",
                table: "TB_CONTACT");

            migrationBuilder.AddColumn<string>(
                name: "DDD",
                table: "TB_CONTACT",
                type: "VARCHAR(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD",
                table: "TB_CONTACT");

            migrationBuilder.AddColumn<Guid>(
                name: "AreaCodeIdAreaCode",
                table: "TB_CONTACT",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_AREA_CODE",
                columns: table => new
                {
                    id_area_code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code_region = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AREA_CODE", x => x.id_area_code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTACT_AreaCodeIdAreaCode",
                table: "TB_CONTACT",
                column: "AreaCodeIdAreaCode");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CONTACT_TB_AREA_CODE_AreaCodeIdAreaCode",
                table: "TB_CONTACT",
                column: "AreaCodeIdAreaCode",
                principalTable: "TB_AREA_CODE",
                principalColumn: "id_area_code");
        }
    }
}
