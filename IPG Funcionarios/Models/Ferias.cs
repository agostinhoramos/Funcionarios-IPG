using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class Ferias { 
        [Key]
        public int FeriasID { get; set; }

        [Required]
        public string TipoFerias { get; set; }

        [Required(ErrorMessage = "Por favor, digite a data do início")]
        [DataType(DataType.Date, ErrorMessage = "Data inválido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }


        [Required(ErrorMessage = "Por favor, digite a data do Fim")]
        [DataType(DataType.Date, ErrorMessage = "Data inválido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFim { get; set; }

        public int QuemID { get; set; }


        /* Fluent API in Entity Framework */
        public int FuncionarioForeignKey { get; set; }
        public Funcionario Funcionario { get; set; }
        public int ProfessorForeignKey { get; set; }
        public Professor Professor { get; set; }

    }
  
}
