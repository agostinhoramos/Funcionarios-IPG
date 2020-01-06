using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Migrations
{
    public partial class escola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    EscolaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false),
                    Localizacao = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 248, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.EscolaID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escola");
        }
    }
}