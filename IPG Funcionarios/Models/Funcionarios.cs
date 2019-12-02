using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class Funcionarios

        {

    [Key]
    public int FuncionariosId { get; set; }

    [Required(ErrorMessage = "Por favor, digite o nome do Funcionarios")]
    [StringLength(maximumLength: 50, MinimumLength = 3)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Por favor, digite o número do telefone")]
    [RegularExpression(@"(2\d)|(9[1236]\d{7})", ErrorMessage = "Número inválido")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Por favor, digite o número do telemóvel") ]
    [RegularExpression(@"9\d{8}|[1236]\d{7})", ErrorMessage = "Número inválido")]
    public string Telemovel { get; set; }

    [Required(ErrorMessage = "Por favor, introduza o email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    public string Horarios_de_trabalho { get; set; }


}
}
