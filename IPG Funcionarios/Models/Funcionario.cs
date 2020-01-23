
+
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


        [Required(ErrorMessage = "Por favor, digite o seu Nome!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 50 caracteres!")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Por favor, digite o seu Telefone/Telemóvel!")]
        [RegularExpression(@"9[1236][0-9]{7}|(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Número inválido!")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "Por favor, digite o seu mail!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Por favor, digite o seu Genéro!")]
        [RegularExpression(@"[F|f|M|m]", ErrorMessage = "Genéro inválido!")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "Por favor, digite a sua Morada!")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
     //   [RegularExpression(@"^([A-Za-z0-9].*)", ErrorMessage = "Morada inválido")]
        public string Morada { get; set; }

          [Required(ErrorMessage = "Por favor, digite a data de nascimento")]
          [DataType(DataType.Date, ErrorMessage = "Data inválido")]
          [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
          /*
        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "Data deve ser preenchida")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Data invalida")]
         */
          public DateTime DataNascionento { get; set; }

        [Required(ErrorMessage = "Por favor, digite a data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Data inválido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAdmissao { get; set; }
        public Escola Escolas { get; set; }
        public int EscolasKey { get; set; }
        public Servico Servicos { get; set; }
        public int ServicosKey { get; set; }
        public ICollection < Feria> feria { get; set; }
        public ICollection < Tarefa> tarefa { get; set; }
        public ICollection< Tipos_Tarefas> TarefasT { get; set; }
        public ICollection<Escola> Escola { get; set; }
        public ICollection<Servico> Servico { get; set; }
       // public  Ferias ferias { get; set; }
       // public Tarefa Tarefa { get; set; }
        public Tarefas_Professor Tarefas { get; set; }
        
    }
}
