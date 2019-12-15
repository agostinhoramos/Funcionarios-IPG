using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class PaginacaoViewModel
    {
        public int Totaltems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int NumberPages => (int)Math.Ceiling((double)Totaltems / PageSize);
        public string Order { get; set; }
    }
}
