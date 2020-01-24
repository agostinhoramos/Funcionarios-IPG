using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Departamento
    {   
        [Key]
        public int DepartamentoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Professor> Professores { get; set; }
        public int EscolaForeignKey { get; set; }
        public Escola Escola { get; set; }
     

    }
}
