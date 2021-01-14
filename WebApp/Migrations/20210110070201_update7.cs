using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThanhTienDonHang",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "DonHangs");

            migrationBuilder.CreateTable(
                name: "ChitietDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSPBill = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false),
                    Madonhang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitietDonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChitietDonHangs_DonHangs_Madonhang",
                        column: x => x.Madonhang,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChitietDonHangs_Madonhang",
                table: "ChitietDonHangs",
                column: "Madonhang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChitietDonHangs");

            migrationBuilder.AddColumn<double>(
                name: "ThanhTienDonHang",
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
    }
}
