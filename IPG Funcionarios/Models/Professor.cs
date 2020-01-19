using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models {
    public class Professor {
        [Key]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "Por favor, insira um Nome!")]
        [StringLength(maximumLength: 150, MinimumLength = 4)]
        [Display(Name = "Nome", Prompt = "Inserir um Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, insira um Contacto!")]
        [RegularExpression(@"(2\d{8})|(9[0123456789]\d{7})", ErrorMessage = "Contacto inválido!")]
        [Display(Name = "Contacto", Prompt = "Inserir um Contacto")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor, insira um Email!")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        [Display(Name = "Email", Prompt = "Inserir um Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, insira um Gabinete!")]
        [Display(Name = "Gabinete", Prompt = "Inserir um Gabinete")]
        public string Gabinete { get; set; }

        /* Fluent API in Entity Framework */
        public int DepartamentoForeignKey { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Ferias> Ferias { get; set; }
        public ICollection<ProfessorTarefaCargo> ProfessorTarefaCargos { get; set; }
    }
}
