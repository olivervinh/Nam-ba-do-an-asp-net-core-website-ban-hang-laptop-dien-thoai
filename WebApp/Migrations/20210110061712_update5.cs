using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChiShipping",
                table: "DonHangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailKhach",
                table: "DonHangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ThanhTienDonHang",
                table: "DonHangs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TongTienDonHang",
                table: "DonHangs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "DonHangs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChiShipping",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "EmailKhach",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "ThanhTienDonHang",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "TongTienDonHang",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "DonHangs");
        }
    }
}
