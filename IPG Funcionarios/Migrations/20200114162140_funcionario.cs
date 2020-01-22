using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Tarefa",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Servico",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Servico",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Escola",
                maxLength: 180,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(248)",
                oldMaxLength: 248);

            migrationBuilder.AlterColumn<string>(
                name: "Localizacao",
                table: "Escola",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Escola",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Escola",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCargo",
                table: "Cargo",
                maxLength: 220,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Genero = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(maxLength: 100, nullable: false),
                    DataNascionento = table.Column<DateTime>(nullable: false),
                    DataAdmissao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servico_FuncionarioId",
                table: "Servico",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_FuncionarioId",
                table: "Escola",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Funcionario_FuncionarioId",
                table: "Escola",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioId",
                table: "Servico",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Funcionario_FuncionarioId",
                table: "Escola");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioId",
                table: "Servico");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Servico_FuncionarioId",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Escola_FuncionarioId",
                table: "Escola");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Escola");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Tarefa",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Servico",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Escola",
                type: "nvarchar(248)",
                maxLength: 248,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 180);

            migrationBuilder.AlterColumn<string>(
                name: "Localizacao",
                table: "Escola",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Escola",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCargo",
                table: "Cargo",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 220);
        }
    }
}
