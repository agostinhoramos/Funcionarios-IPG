using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class ProfessorTarefaCargo
    {
        [Key]
        public int id { get; set; }
        public int ProfessorForeignKey { get; set; }
        public int TarefaForeignKey { get; set; }
        public int CargoForeignKey { get; set; }

        public Professor Professor { get; set; }
        public Tarefa Tarefa { get; set; }
        public Cargo Cargo { get; set; }
    }
}
