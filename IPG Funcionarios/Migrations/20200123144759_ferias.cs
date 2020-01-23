using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class ferias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuncionarioForeignKey",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuncionariosFuncionarioId",
                table: "Tarefa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorForeignKey",
                table: "Tarefa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessoresProfessorId",
                table: "Tarefa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tarefas_ProfessorID",
                table: "Tarefa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoForeignKey",
                table: "Professor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentosDepartamentoId",
                table: "Professor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tarefas_ProfessorID",
                table: "Professor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tarefas_ProfessorID",
                table: "Funcionario",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feria",
                columns: table => new
                {
                    FeriasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFerias = table.Column<string>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    QuemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feria", x => x.FeriasID);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas_Professor",
                columns: table => new
                {
                    Tarefas_ProfessorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas_Professor", x => x.Tarefas_ProfessorID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FuncionariosFuncionarioId",
                table: "Tarefa",
                column: "FuncionariosFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProfessoresProfessorId",
                table: "Tarefa",
                column: "ProfessoresProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_Tarefas_ProfessorID",
                table: "Tarefa",
                column: "Tarefas_ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_DepartamentosDepartamentoId",
                table: "Professor",
                column: "DepartamentosDepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Tarefas_ProfessorID",
                table: "Professor",
                column: "Tarefas_ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Tarefas_ProfessorID",
                table: "Funcionario",
                column: "Tarefas_ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Funcionario",
                column: "Tarefas_ProfessorID",
                principalTable: "Tarefas_Professor",
                principalColumn: "Tarefas_ProfessorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Departamento_DepartamentosDepartamentoId",
                table: "Professor",
                column: "DepartamentosDepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Professor",
                column: "Tarefas_ProfessorID",
                principalTable: "Tarefas_Professor",
                principalColumn: "Tarefas_ProfessorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Funcionario_FuncionariosFuncionarioId",
                table: "Tarefa",
                column: "FuncionariosFuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Professor_ProfessoresProfessorId",
                table: "Tarefa",
                column: "ProfessoresProfessorId",
                principalTable: "Professor",
                principalColumn: "ProfessorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Tarefa",
                column: "Tarefas_ProfessorID",
                principalTable: "Tarefas_Professor",
                principalColumn: "Tarefas_ProfessorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Departamento_DepartamentosDepartamentoId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Funcionario_FuncionariosFuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Professor_ProfessoresProfessorId",
                table: "Tarefa");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Tarefas_Professor_Tarefas_ProfessorID",
                table: "Tarefa");

            migrationBuilder.DropTable(
                name: "Feria");

            migrationBuilder.DropTable(
                name: "Tarefas_Professor");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FuncionariosFuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_ProfessoresProfessorId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_Tarefas_ProfessorID",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Professor_DepartamentosDepartamentoId",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Professor_Tarefas_ProfessorID",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_Tarefas_ProfessorID",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "FuncionarioForeignKey",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "FuncionariosFuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "ProfessorForeignKey",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "ProfessoresProfessorId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "Tarefas_ProfessorID",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "DepartamentoForeignKey",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "DepartamentosDepartamentoId",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Tarefas_ProfessorID",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Tarefas_ProfessorID",
                table: "Funcionario");
        }
    }
}
