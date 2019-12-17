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
            if (db.Departamento.Any()) {
                return;
            }
       
                db.Departamento.AddRange(
                new Departamento { Nome = "Departamento de Engenharia Civil"},
                new Departamento { Nome = "Departamento de Engenharia Informática"},
                new Departamento { Nome = "Departamento de Física", },
                new Departamento { Nome = "Departamento de Engenharia Topográfica"},
                new Departamento { Nome = "Departamento de Energia e Ambiente" },
                new Departamento { Nome = "Departamento de Farmácia" },
                new Departamento { Nome = "Departamento de Hotelaria"},
                new Departamento { Nome = "	Departamento de Desporto"},
                new Departamento { Nome = "	Departamento de Comunicação Multimédia" }
             );
            
            db.SaveChanges();

          
        }
     }
 }
