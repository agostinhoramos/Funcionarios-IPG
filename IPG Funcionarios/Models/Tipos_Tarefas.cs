using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class Tipos_Tarefas
    {
        public int Tipos_TarefasID { get; set; }
        public string Nome { get; set; }
        public ICollection<Funcionario>Funcionarios { set; get; }
        public ICollection<Tarefa> Tarefas { set; get; }
        public ICollection<Cargo>Cargos { set; get; }
    }
}
