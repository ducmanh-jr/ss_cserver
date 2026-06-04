using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nguyen_Khanh_Thu_193865.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product193865De3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenProduct = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product193865De3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipper193865De3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaShipper = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipper193865De3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipperProduct193865De3",
                columns: table => new
                {
                    ShipperID = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperProduct193865De3", x => new { x.ShipperID, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Product193865De3_ShipperProduct193865De3",
                        column: x => x.ProductId,
                        principalTable: "Product193865De3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipper193865_ShipperProduct193865De3",
                        column: x => x.ShipperID,
                        principalTable: "Shipper193865De3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperProduct193865De3_ProductId",
                table: "ShipperProduct193865De3",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipperProduct193865De3");

            migrationBuilder.DropTable(
                name: "Product193865De3");

            migrationBuilder.DropTable(
                name: "Shipper193865De3");
        }
    }
}
