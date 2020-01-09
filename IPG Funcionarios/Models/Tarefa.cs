using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Tarefa
    {
        [Key]
        public int TarefaID { get; set; }

        [Required(ErrorMessage = "Por favor, inserir um nome.")]
        [StringLength(maximumLength: 150, MinimumLength = 4)]
        [Display(Name = "Nome", Prompt = "Inserir um Cargo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, inserir uma descrição.")]
        [StringLength(500)]
        [Display(Name = "Descrição", Prompt = "Inserir uma Descrição ( Opcional )")]
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
