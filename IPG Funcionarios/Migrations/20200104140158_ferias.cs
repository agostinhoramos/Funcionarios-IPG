using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IPG_Funcionarios.Migrations
{
    public partial class ferias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ferias",
                columns: table => new
                {
                    FeriasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFerias = table.Column<string>(maxLength: 200, nullable: false),
					DataInicio = table.Column<DateTime>(nullable: false),
					DataFim = table.Column<DateTime>(nullable: false),
					QuemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias", x => x.FeriasID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ferias");
        }
    }
}