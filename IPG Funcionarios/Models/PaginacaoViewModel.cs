using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class PaginacaoViewModel<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginacaoViewModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        public static async Task<PaginacaoViewModel<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginacaoViewModel<T>(items, count, pageIndex, pageSize);
        }

    
     
    }

/*  public class PaginacaoViewModel
  {
      public int PaginaCorrente { get; set; }
      public int TotalItens { get; set; }
      public int TamanhoPagina { get; set; }
      public int NumeroPagina => (int)Math.Ceiling((double)TotalItens / TamanhoPagina);
   //   public String Nome { get; set; }
      public string Order { get; set; }
  }*/
}
