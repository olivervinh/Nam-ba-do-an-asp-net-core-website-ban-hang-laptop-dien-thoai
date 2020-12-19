using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CateandPros");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CateandPros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamMa = table.Column<int>(type: "int", nullable: true),
                    loaiSPMaLoai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateandPros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateandPros_LoaiSPs_loaiSPMaLoai",
                        column: x => x.loaiSPMaLoai,
                        principalTable: "LoaiSPs",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CateandPros_SanPhams_SanPhamMa",
                        column: x => x.SanPhamMa,
                        principalTable: "SanPhams",
                        principalColumn: "Ma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CateandPros_loaiSPMaLoai",
                table: "CateandPros",
                column: "loaiSPMaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_CateandPros_SanPhamMa",
                table: "CateandPros",
                column: "SanPhamMa");
        }
    }
}
