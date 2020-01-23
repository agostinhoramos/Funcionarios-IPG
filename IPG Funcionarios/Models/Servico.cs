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

        [Required(ErrorMessage = "Por favor, insira um nome do Serviço.")]
        [StringLength(maximumLength: 150, MinimumLength = 4)]
        [Display(Name = "Nome", Prompt = "Inserir um nome de Serviço")]
        public string Nome { get; set; }

        /* Fluent API in Entity Framework */
        public int EscolaForeignKey { get; set; }
        public Escola Escola { get; set; }
        public int FuncionarioForeignKey { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
