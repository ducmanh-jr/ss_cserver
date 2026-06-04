using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenDucManh0210668.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate0210668De1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DuAns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenDuAn = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaDuAn = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuAns", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenNhanVien = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaNhanVien = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhanCongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),
                    DuAnId = table.Column<int>(type: "int", nullable: false),
                    SoGioLamViec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhanCongs_DuAns_DuAnId",
                        column: x => x.DuAnId,
                        principalTable: "DuAns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCongs_NhanViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DuAns_MaDuAn",
                table: "DuAns",
                column: "MaDuAn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DuAns_TenDuAn",
                table: "DuAns",
                column: "TenDuAn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_Email",
                table: "NhanViens",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaNhanVien",
                table: "NhanViens",
                column: "MaNhanVien",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_DuAnId",
                table: "PhanCongs",
                column: "DuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_NhanVienId_DuAnId",
                table: "PhanCongs",
                columns: new[] { "NhanVienId", "DuAnId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanCongs");

            migrationBuilder.DropTable(
                name: "DuAns");

            migrationBuilder.DropTable(
                name: "NhanViens");
        }
    }
}
