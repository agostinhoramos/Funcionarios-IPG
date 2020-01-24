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
        public Tarefa Tarefas { get; set; }
        public int TarefaKey { get; set; }
        public Professor Professors { set; get; }
        public int ProfessorsKey { get; set; }
        public Cargo Cargo { set; get; }
        public int CargoKey { get; set; }
    }
}
