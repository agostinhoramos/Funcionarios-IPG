using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class ProfessorViewModel {
        public IEnumerable<Professor> Professor { get; set; }
        public int CurrentPage { get; set; }
        public int AllPages { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public string CurrentSearch { get; set; }
        public string CurrentOption { get; set; }
        public string Sort { get; set; }
        public int entries_all { get; set; }
        public int entries_start { get; set; }
        public int entries_end { get; set; }
        public int entries_per_page { get; set; }
    }
}