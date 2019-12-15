using System.ComponentModel.DataAnnotations;

namespace IPG_Funcionarios.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Contacto { get; set; }
        
       // public string NomeRespo { get; set; }
    }
}
