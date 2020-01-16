using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<Professor> Professores { get; set; }

    }
}
