using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Escola
    {
        [Key]
        public int EscolaID { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome da Escola!")]
        [StringLength(maximumLength: 180, MinimumLength = 2)]
        [Display(Name = "Nome", Prompt = "Inserir um nome de Escola")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Por favor, insira a Localização!")]
        [StringLength(maximumLength: 200, MinimumLength = 2)]
        [Display(Name = "Localização", Prompt = "Inserir nome de Localização")]
        public String Localizacao { get; set; }

        [StringLength(maximumLength: 250)]
        [Display(Name = "Descrição", Prompt = "Inserir uma Descrição ( Opcional )")]
        public String Descricao { get; set; }

        /* Fluent API in Entity Framework */
        public ICollection<Departamento> Departamentos { get; set; }
        public ICollection<Servico> Servicos { get; set; }
    }
}
