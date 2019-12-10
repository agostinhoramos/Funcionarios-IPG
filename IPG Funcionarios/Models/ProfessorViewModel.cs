using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class ProfessorViewModel {
        public IEnumerable<Professor> Professor { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int PrimeiraPagina { get; set; }
        public int UltimaPagina { get; set; }
        public string StringProcura { get; set; }
        public string Sort { get; set; }
    }
}
