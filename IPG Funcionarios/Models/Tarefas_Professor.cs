using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class Tarefas_Professor
    {
        public int Tarefas_ProfessorID { get; set; }

        public string Nome { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; }
        public ICollection<Professor> Professors { set; get; }
    }
}
