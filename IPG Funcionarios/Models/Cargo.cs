using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Cargo
    {
        [Key]
        public int CargoID { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome de cargo")]
        [StringLength(250)]
        [Display(Name = "Nome")]
        public string NomeCargo { get; set; }

        public int CargoChefeId { get; set; }
    }
}
