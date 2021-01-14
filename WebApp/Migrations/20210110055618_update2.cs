using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_SanPhams_productMa",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_productMa",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "productMa",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "MaSP",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MaSP",
                table: "CartItems",
                column: "MaSP");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_SanPhams_MaSP",
                table: "CartItems",
                column: "MaSP",
                principalTable: "SanPhams",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_SanPhams_MaSP",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_MaSP",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "MaSP",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "productMa",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_productMa",
                table: "CartItems",
                column: "productMa");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_SanPhams_productMa",
                table: "CartItems",
                column: "productMa",
                principalTable: "SanPhams",
                principalColumn: "Ma",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
