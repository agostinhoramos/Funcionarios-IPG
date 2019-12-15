using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public interface IDepartamentoRepository
     {
        public IEnumerable<Departamento> Departamentos { get; }
    }
}
