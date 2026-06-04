using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nguyen_Khanh_Thu_193865.Migrations
{
    /// <inheritdoc />
    public partial class addSoLuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "ShipperProduct193865De3",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "ShipperProduct193865De3");
        }
    }
}
