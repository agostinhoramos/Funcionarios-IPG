using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class DepartamentoViewsModels
    {
        public IEnumerable <Departamento> Departamentos {get; set;}
        public int PaginaCorrente {get; set;}
        public int PaginaTotal {get; set;}
        public int MostrarPrimeiraPagina {get; set;}
        public  int MostrarUltimaPagina {get; set;}
        public string StringProcura { get; set; }
        public string Sort { get; set; }
    }
}
