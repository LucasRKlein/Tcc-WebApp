using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tcc.Persistence.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Associado_AssociadoId",
                table: "Veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos");

            migrationBuilder.RenameTable(
                name: "Veiculos",
                newName: "Veiculo");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculos_AssociadoId",
                table: "Veiculo",
                newName: "IX_Veiculo_AssociadoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInclusao",
                table: "Veiculo",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VistoriaImagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    ImagemBase64 = table.Column<string>(type: "TEXT", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsuarioIdInclusao = table.Column<Guid>(type: "TEXT", nullable: true),
                    UsuarioIdAlteracao = table.Column<Guid>(type: "TEXT", nullable: true),
                    RegistroAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VistoriaImagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VistoriaImagem_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VistoriaImagem_VeiculoId",
                table: "VistoriaImagem",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Associado_AssociadoId",
                table: "Veiculo",
                column: "AssociadoId",
                principalTable: "Associado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Associado_AssociadoId",
                table: "Veiculo");

            migrationBuilder.DropTable(
                name: "VistoriaImagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.RenameTable(
                name: "Veiculo",
                newName: "Veiculos");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_AssociadoId",
                table: "Veiculos",
                newName: "IX_Veiculos_AssociadoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInclusao",
                table: "Veiculos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculos",
                table: "Veiculos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Associado_AssociadoId",
                table: "Veiculos",
                column: "AssociadoId",
                principalTable: "Associado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
