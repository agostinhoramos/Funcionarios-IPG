using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class UserAccount
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Por favor, insira o seu nome")]
        [StringLength(50)]
        [Display(Name = "Nome:")]
        public string FnameAccount { get; set; }

        [Required(ErrorMessage = "Por favor, insira o seu apelido")]
        [StringLength(75)]
        [Display(Name = "Sobrenome:")]

        public string LnameAccount { get; set; }
        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        [StringLength(200)]

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira uma senha.")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        
        [Required]
        public bool IsValidAccount { get; set; }
    }
}
