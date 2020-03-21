using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPG_Funcionarios.Models
{
    public class Cargo
    {
        [Key]
        public int CargoID { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome do Cargo")]
        [StringLength(maximumLength: 220, MinimumLength = 3)]
        [Display(Name = "Nome", Prompt = "Inserir um nome de Cargo")]
        public string NomeCargo { get; set; }
        public int? CargoChefe { get; set; }


        /* Fluent API in Entity Framework */
        public Cargo Chefe { get; set; }
        public ICollection<FuncionarioTarefaCargo> FuncionarioTarefaCargos { get; set; }
        public ICollection<ProfessorTarefaCargo> ProfessorTarefaCargos { get; set; }
    }
}
