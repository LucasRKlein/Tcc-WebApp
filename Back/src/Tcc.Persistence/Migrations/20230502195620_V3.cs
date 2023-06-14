using Microsoft.EntityFrameworkCore.Migrations;

namespace Tcc.Persistence.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_AspNetUsers_UserId",
                table: "Associado");

            migrationBuilder.DropIndex(
                name: "IX_Associado_UserId",
                table: "Associado");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Associado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Associado",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Associado_UserId",
                table: "Associado",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_AspNetUsers_UserId",
                table: "Associado",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
