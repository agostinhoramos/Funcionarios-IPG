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
        
        [Required(ErrorMessage = "Por favor, digite o seu Nome completo!")]
        [StringLength(50, MinimumLength = 3)]
        public string Nome{ get; set;}
        [Required(ErrorMessage = "Por favor, digite o seu número de telemóvel/telefone!")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Número inválido!")]
        public string Número { get; set; }
        [Required(ErrorMessage = "Por favor, Digite o email!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, Digite a sua Morada!")]
        [RegularExpression(@"[A-Z][a-z].+", ErrorMessage = "Morada inválido!")]
        public string Morada { get; set; }

        [Required(ErrorMessage = "Por favor, Digite o seu Sexo!")]
        [RegularExpression(@"[(m|M|f|F|)$]", ErrorMessage = "Sexo inválido!")]
        public string Sexo { get; set; }




    }
}
