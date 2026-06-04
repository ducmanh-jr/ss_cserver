using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nguyenducmanh0210668.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaSanPham = table.Column<string>(type: "TEXT", nullable: false),
                    TenSanPham = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaShipper = table.Column<string>(type: "TEXT", nullable: false),
                    TenShipper = table.Column<string>(type: "TEXT", nullable: false),
                    CCCD = table.Column<string>(type: "TEXT", nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoHangs",
                columns: table => new
                {
                    ShipperId = table.Column<int>(type: "INTEGER", nullable: false),
                    SanPhamId = table.Column<int>(type: "INTEGER", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiaoHangs", x => new { x.ShipperId, x.SanPhamId });
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoHangs_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoHangs_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoHangs_SanPhamId",
                table: "ChiTietGiaoHangs",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaSanPham",
                table: "SanPhams",
                column: "MaSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_TenSanPham",
                table: "SanPhams",
                column: "TenSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_CCCD",
                table: "Shippers",
                column: "CCCD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_MaShipper",
                table: "Shippers",
                column: "MaShipper",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_TenShipper",
                table: "Shippers",
                column: "TenShipper",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGiaoHangs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "Shippers");
        }
    }
}
