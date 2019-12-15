using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class EFIPGFuncionariosRepository :IIPGFuncionariosRepository
    {
        private IPGFuncionariosDbContext db;

        public EFIPGFuncionariosRepository (IPGFuncionariosDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Departamento> Departamentos => db.Departamento; 
    }
}
