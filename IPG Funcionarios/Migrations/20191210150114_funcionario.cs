using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Funcionario");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Funcionario",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Funcionario",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "Funcionario",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Morada",
                table: "Funcionario");

            migrationBuilder.AddColumn<int>(
                name: "FuncionariosId",
                table: "Funcionario",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Funcionario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "FuncionarioId");
        }
    }
}
