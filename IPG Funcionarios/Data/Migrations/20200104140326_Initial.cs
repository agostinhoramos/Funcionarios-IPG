using Microsoft.EntityFrameworkCore.Migrations;

namespace IPG_Funcionarios.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userAccount",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FnameAccount = table.Column<string>(maxLength: 50, nullable: false),
                    LnameAccount = table.Column<string>(maxLength: 75, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    PasswordHash = table.Column<string>(nullable: false),
                    IsValidAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAccount", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAccount");
        }
    }
}
