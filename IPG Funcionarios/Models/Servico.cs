using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "Por favor insira o nome do serviço.")]
        [StringLength(200)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
