using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class SeedDataFuncionario
    {
        public static void Populate(IPGFuncionariosDbContext db)
        {
            SeedFuncionario(db);

        }
        private static void SeedFuncionario(IPGFuncionariosDbContext db) {
            if (db.Funcionario.Any()) return;

            db.Funcionario.AddRange(
                new Funcionario { Nome = "Lina Sousa", Telefone = "234567890", Email= "lina@gmail.com", Genero = "F", Morada = "Rua Xanana Gusmão " },
                new Funcionario { Nome = "Lara Lima", Telefone = "912344567", Email = "lima@gmail.com", Genero = "F", Morada = "Rua Páiva" },
                new Funcionario { Nome = "João Rita", Telefone = "923456211", Email = "joaorita@gmail.com", Genero = "m", Morada = "Rua da Alegria " }
                );
            db.SaveChanges();
        }
    }
}
