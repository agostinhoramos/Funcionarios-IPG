using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class Professor {
        [Required]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nome do professor.")]
        [StringLength(maximumLength: 150, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o seu número de telemóvel/telefone!")]
        [RegularExpression(@"(2\d{8})|(9[0123456789]\d{7})", ErrorMessage = "Contacto inválido!")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }
        [Required]
        public string Gabinete { get; set; }
    }
}
