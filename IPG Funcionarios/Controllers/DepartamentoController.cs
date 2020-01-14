using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPG_Funcionarios.Models;

namespace IPG_Funcionarios.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public DepartamentoController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Departamento
        public IActionResult Index (int page = 1, string sort = null, string q= null, string o = "nome", int ipp = 10) { 
             
         var departamento = from p in _context.Departamento
          select p;
          decimal nLinhas = departamento.Count();
        

          if (ipp <= 1) {
                ipp = (int)Math.Ceiling(nLinhas);
            }

            int Pagina_antes_e_depois = ((int)nLinhas / ipp);

            if (nLinhas % ipp == 0)
            {
                Pagina_antes_e_depois -= 1;
            }

            DepartamentoViewsModels vm = new DepartamentoViewsModels
            {
                mainURL = "Departamento/Index",
                column = new string[] { "nome" },
                PaginaCorrente = page,
                PaginaTotal = (int)Math.Ceiling(nLinhas / ipp),
                MostrarPrimeiraPagina = Math.Max(1, page - Pagina_antes_e_depois),

                IntensPorPagina = ipp,
                IntensInicial = ipp * (page - 1) > 0 ? ipp * (page - 1) + 1 : ((int)Math.Ceiling(nLinhas) < 1 ? 0 : 1),
                IntensFinal = ipp * page < (int)Math.Ceiling(nLinhas) ?
                ipp * page : (int)Math.Ceiling(nLinhas),
                TodosIntens = (int)Math.Ceiling(nLinhas)
            };

            //Pesquisa

            if (!String.IsNullOrEmpty(q))
            {
                vm.StringProcura = q;
                if (!String.IsNullOrEmpty(o))

                {
                    switch (o)
                    {
                        case "id":
                            int Numeroquery = 0;
                            departamento= departamento.Where(p => p.DepartamentoId.CompareTo(Numeroquery) == 0);
                            break;
                        case "nome":
                            departamento = departamento.Where(p => p.Nome.Contains(q));
                            break;


                    }

                }
            }


                //Ordenação do Caracteres

                if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o))
                {


                    switch (o)
                    {
                        case "id":
                            vm.Departamentos = (sort == "1") ?
                               (departamento.OrderBy(p => p.DepartamentoId).Skip((page - 1) * ipp).Take(ipp)) :
                               (departamento.OrderByDescending(p => p.DepartamentoId).Skip((page - 1) * ipp).Take(ipp));
                            break;
                        case "nome":
                            vm.Departamentos = (sort == "1") ?
                            (departamento.OrderBy(p => p.Nome).Skip((page - 1) * ipp).Take(ipp)) :
                            (departamento.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                            break;

                    }
                    vm.Sort = sort;

                }
                else
                {
                    vm.Departamentos = departamento.Skip((page - 1) * ipp).Take(ipp);
                }
                vm.MostrarUltimaPagina = Math.Min(vm.PaginaTotal, page + Pagina_antes_e_depois);
                vm.OpcaoCorrente = o;

                    return View(vm);
            }
        
  

        // GET: Departamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoId,Nome,Contacto")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartamentoId,Nome,Contacto")] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}
