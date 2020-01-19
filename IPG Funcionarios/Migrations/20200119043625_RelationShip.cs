using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class RelationShip : Migration
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
                name: "EscolaForeignKey",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioForeignKey",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoForeignKey",
                table: "Professor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioForeignKey",
                table: "Ferias",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorForeignKey",
                table: "Ferias",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaForeignKey",
                table: "Departamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FuncionarioTarefaCargo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioForeignKey = table.Column<int>(nullable: false),
                    TarefaForeignKey = table.Column<int>(nullable: false),
                    CargoForeignKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioTarefaCargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncionarioTarefaCargo_Cargo_CargoForeignKey",
                        column: x => x.CargoForeignKey,
                        principalTable: "Cargo",
                        principalColumn: "CargoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioTarefaCargo_Funcionario_FuncionarioForeignKey",
                        column: x => x.FuncionarioForeignKey,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioTarefaCargo_Tarefa_TarefaForeignKey",
                        column: x => x.TarefaForeignKey,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorTarefaCargo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorForeignKey = table.Column<int>(nullable: false),
                    TarefaForeignKey = table.Column<int>(nullable: false),
                    CargoForeignKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorTarefaCargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProfessorTarefaCargo_Cargo_CargoForeignKey",
                        column: x => x.CargoForeignKey,
                        principalTable: "Cargo",
                        principalColumn: "CargoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorTarefaCargo_Professor_ProfessorForeignKey",
                        column: x => x.ProfessorForeignKey,
                        principalTable: "Professor",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorTarefaCargo_Tarefa_TarefaForeignKey",
                        column: x => x.TarefaForeignKey,
                        principalTable: "Tarefa",
                        principalColumn: "TarefaID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Servico_EscolaForeignKey",
                table: "Servico",
                column: "EscolaForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_FuncionarioForeignKey",
                table: "Servico",
                column: "FuncionarioForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_DepartamentoForeignKey",
                table: "Professor",
                column: "DepartamentoForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Ferias_FuncionarioForeignKey",
                table: "Ferias",
                column: "FuncionarioForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Ferias_ProfessorForeignKey",
                table: "Ferias",
                column: "ProfessorForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_EscolaForeignKey",
                table: "Departamento",
                column: "EscolaForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_CargoChefe",
                table: "Cargo",
                column: "CargoChefe");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioTarefaCargo_CargoForeignKey",
                table: "FuncionarioTarefaCargo",
                column: "CargoForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioTarefaCargo_FuncionarioForeignKey",
                table: "FuncionarioTarefaCargo",
                column: "FuncionarioForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioTarefaCargo_TarefaForeignKey",
                table: "FuncionarioTarefaCargo",
                column: "TarefaForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTarefaCargo_CargoForeignKey",
                table: "ProfessorTarefaCargo",
                column: "CargoForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTarefaCargo_ProfessorForeignKey",
                table: "ProfessorTarefaCargo",
                column: "ProfessorForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTarefaCargo_TarefaForeignKey",
                table: "ProfessorTarefaCargo",
                column: "TarefaForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Cargo_CargoChefe",
                table: "Cargo",
                column: "CargoChefe",
                principalTable: "Cargo",
                principalColumn: "CargoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Escola_EscolaForeignKey",
                table: "Departamento",
                column: "EscolaForeignKey",
                principalTable: "Escola",
                principalColumn: "EscolaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Funcionario_FuncionarioForeignKey",
                table: "Ferias",
                column: "FuncionarioForeignKey",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Professor_ProfessorForeignKey",
                table: "Ferias",
                column: "ProfessorForeignKey",
                principalTable: "Professor",
                principalColumn: "ProfessorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Departamento_DepartamentoForeignKey",
                table: "Professor",
                column: "DepartamentoForeignKey",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Escola_EscolaForeignKey",
                table: "Servico",
                column: "EscolaForeignKey",
                principalTable: "Escola",
                principalColumn: "EscolaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioForeignKey",
                table: "Servico",
                column: "FuncionarioForeignKey",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Cargo_CargoChefe",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Escola_EscolaForeignKey",
                table: "Departamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Ferias_Funcionario_FuncionarioForeignKey",
                table: "Ferias");

            migrationBuilder.DropForeignKey(
                name: "FK_Ferias_Professor_ProfessorForeignKey",
                table: "Ferias");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Departamento_DepartamentoForeignKey",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Escola_EscolaForeignKey",
                table: "Servico");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioForeignKey",
                table: "Servico");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Funcionario_FuncionariosFuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Professor_ProfessoresProfessorId",
                table: "Tarefa");

            migrationBuilder.DropTable(
                name: "FuncionarioTarefaCargo");

            migrationBuilder.DropTable(
                name: "ProfessorTarefaCargo");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FuncionariosFuncionarioId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_ProfessoresProfessorId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Servico_EscolaForeignKey",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Servico_FuncionarioForeignKey",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Professor_DepartamentoForeignKey",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Ferias_FuncionarioForeignKey",
                table: "Ferias");

            migrationBuilder.DropIndex(
                name: "IX_Ferias_ProfessorForeignKey",
                table: "Ferias");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_EscolaForeignKey",
                table: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_CargoChefe",
                table: "Cargo");

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
                name: "EscolaForeignKey",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "FuncionarioForeignKey",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "DepartamentoForeignKey",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "FuncionarioForeignKey",
                table: "Ferias");

            migrationBuilder.DropColumn(
                name: "ProfessorForeignKey",
                table: "Ferias");

            migrationBuilder.DropColumn(
                name: "EscolaForeignKey",
                table: "Departamento");
        }
    }
}
