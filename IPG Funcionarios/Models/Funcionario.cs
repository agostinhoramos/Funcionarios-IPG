
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }


        [Required(ErrorMessage = "Por favor, digite o seu Nome!")]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, digite o seu Telefone/Telemóvel!")]
        [RegularExpression(@"9[1236][0-9]{7}|(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Número inválido!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Por favor, introduz o seu mail!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, digite o seu Genéro!")]
        [RegularExpression(@"[F|f|M|m]", ErrorMessage = "Genéro inválido!")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Por favor, digite a sua Morada!")]
     
        public string Morada { get; set; }



    }
}
