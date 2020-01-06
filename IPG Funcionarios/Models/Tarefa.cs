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

        [Required(ErrorMessage = "Por favor, inserir uma descrição.")]
        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, inserir um nome.")]
        [StringLength(500)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
