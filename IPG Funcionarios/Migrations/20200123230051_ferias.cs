using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class ferias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feria",
                columns: table => new
                {
                    FeriaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFeria = table.Column<string>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    FuncionarioForeignKey = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: true),
                    ProfessorForeignKey = table.Column<int>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feria", x => x.FeriaID);
                    table.ForeignKey(
                        name: "FK_Feria_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feria_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "ProfessorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feria_FuncionarioId",
                table: "Feria",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Feria_ProfessorId",
                table: "Feria",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feria");
        }
    }
}
