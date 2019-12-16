using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacto",
                table: "Departamento");

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
                    Morada = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.AddColumn<string>(
                name: "Contacto",
                table: "Departamento",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
