using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class SeedDataDepartamento
    {
        public static void Populate(IPGFuncionariosDbContext db)
        {
            PopulateDepartamento(db);
        }
        private static void PopulateDepartamento(IPGFuncionariosDbContext db)
        {
            if (db.Departamento.Any()) return;

            db.Departamento.AddRange(
                new Departamento { Nome = "Departamento de Engenharia Civil", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Engenharia Informática", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Física", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Engenharia Topográfica", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Energia e Ambiente", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Farmácia", Contacto = " 222384839" },
                new Departamento { Nome = "Departamento de Hotelaria", Contacto = " 222384839" },
                new Departamento { Nome = "	Departamento de Desporto", Contacto = " 222384839" },
                new Departamento { Nome = "	Departamento de Comunicação Multimédia", Contacto = " 222384839" }
             );
            db.SaveChanges();

          
        }
     }
 }
