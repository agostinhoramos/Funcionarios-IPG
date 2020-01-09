using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class TarefaViewModel
    {
        public IEnumerable<Tarefa> Tarefas { get; set; }
        public int CurrentPage { get; set; }
        public int AllPages { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public string CurrentSearch { get; set; }
        public string CurrentOption { get; set; }
        public string Sort { get; set; }
        public int EntriesAll { get; set; }
        public int EntriesStart { get; set; }
        public int EntriesEnd { get; set; }
        public int EntriesPerPage { get; set; }
        public string[] column;
        public string mainURL;
    }
}