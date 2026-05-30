using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DucManhJr1234.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseProducts",
                columns: table => new
                {
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseProducts", x => new { x.EnterpriseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_EnterpriseProducts_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterpriseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "Id", "Address", "Name", "TaxCode" },
                values: new object[,]
                {
                    { 1, "Ha Noi", "Cong ty ABC", "MST001" },
                    { 2, "TP HCM", "Cong ty XYZ", "MST002" },
                    { 3, "Da Nang", "Cong ty Demo", "MST003" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "ImportDate", "Name" },
                values: new object[,]
                {
                    { 1, "SP001", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptop Dell" },
                    { 2, "SP002", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ban phim co" },
                    { 3, "SP003", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuot khong day" },
                    { 4, "SP004", new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Man hinh 24 inch" }
                });

            migrationBuilder.InsertData(
                table: "EnterpriseProducts",
                columns: new[] { "EnterpriseId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 20 },
                    { 1, 2, 50 },
                    { 1, 3, 50 },
                    { 2, 1, 15 },
                    { 2, 4, 70 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseProducts_ProductId",
                table: "EnterpriseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_Name",
                table: "Enterprises",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_TaxCode",
                table: "Enterprises",
                column: "TaxCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterpriseProducts");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
