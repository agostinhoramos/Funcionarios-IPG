using Microsoft.EntityFrameworkCore.Migrations;

namespace IPGFuncionarios.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    FuncionariosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Telemovel = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Horas_de_trabalho = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.FuncionariosId);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Contacto = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Gabinete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ProfessorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}
