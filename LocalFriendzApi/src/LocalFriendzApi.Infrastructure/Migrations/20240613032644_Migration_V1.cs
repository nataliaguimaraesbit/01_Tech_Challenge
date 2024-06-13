using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFriendzApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AREA_CODE",
                columns: table => new
                {
                    IdAreaCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code_region = table.Column<string>(type: "NVARCHAR(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_area_code", x => x.IdAreaCode);
                });

            migrationBuilder.CreateTable(
                name: "TB_CONTACT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR(40)", maxLength: 40, nullable: false),
                    AreaCodeIdAreaCode = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_CONTACT_TB_AREA_CODE_AreaCodeIdAreaCode",
                        column: x => x.AreaCodeIdAreaCode,
                        principalTable: "TB_AREA_CODE",
                        principalColumn: "IdAreaCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTACT_AreaCodeIdAreaCode",
                table: "TB_CONTACT",
                column: "AreaCodeIdAreaCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CONTACT");

            migrationBuilder.DropTable(
                name: "TB_AREA_CODE");
        }
    }
}
