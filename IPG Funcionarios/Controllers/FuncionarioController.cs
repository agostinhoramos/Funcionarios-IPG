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
    public class FuncionarioController : Controller


    {
        private readonly IPGFuncionariosDbContext _context;
        private const int PAGE_SIZE = 5;
        public FuncionarioController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Funcionario
        public async Task<IActionResult> Index(string ordem, string filtroAtual, string filtro, int? pagina)
        {
            ViewData["ordemAtual"] = ordem;
            ViewData["NomeParm"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["DataParm"] = ordem == "Data" ? "data_desc" : "Data";

            if (filtro != null)
            {
                pagina = 1;
            }
            else
            {
                filtro = filtroAtual;
            }
            ViewData["filtroAtual"] = filtro;
            var funcionario = from est in _context.Funcionario select est;
            if (!String.IsNullOrEmpty(filtro))
            {
                funcionario = funcionario.Where(est => est.Email.Contains(filtro) || est.Nome.Contains(filtro));
            }
            switch (ordem)
            {
                case "nome_desc":
                    funcionario = funcionario.OrderByDescending(est => est.Nome);
                    break;
                case "Data":
                    funcionario = funcionario.OrderBy(est => est.DataNascionento);
                    break;
                case "data_desc":
                    funcionario = funcionario.OrderByDescending(est => est.DataNascionento);
                    break;
                case "telefone":
                    funcionario = funcionario.OrderBy(est => est.Telefone);
                    break;
                case "morada":
                    funcionario = funcionario.OrderBy(est => est.Morada);
                    break;
                default:
                    funcionario = funcionario.OrderBy(est => est.Email);
                    break;
            }
            return View(await PaginacaoViewModel<Funcionario>.CreateAsync(funcionario.AsNoTracking(), pagina ?? 1, PAGE_SIZE));

            //  public async Task<IActionResult> Index(FuncionarioViewList model = null, int pagina = 1, string order = null)
            //{
                //            string Funcionario = null;
                /*    if (model != null !! model.Nome!=null)
                    {
                        nome= model.Nome;
                    }
                    var funcionario = _context.Funcionario
                        .Where(p => Funcionario == null || p.Nome.Contains(Funcionario));
                    int numFuncionario = await funcionario.CountAsync();

                    if (pagina > (numFuncionario / PAGE_SIZE) + 1)
                    {
                        pagina = 1;
                    }
                    IEnumerable<Funcionario> TipoList;
                    if (order == "ID")
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.FuncionarioId)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }
                    else if (order == "Nome")
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.Nome)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }
                    else if (order == "Telefone")
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.Telefone)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }
                    else if (order == "Género")
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.Genero)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }
                    else if (order == "Morada")
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.Morada)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }
                    else
                    {
                        TipoList = await funcionario
                            .OrderBy(p => p.FuncionarioId)
                            .Skip(PAGE_SIZE * (pagina - 1))
                            .Take(PAGE_SIZE)
                            .ToListAsync();
                    }

                    return View(
                        new FuncionarioViewList
                        {
                            Funcionario = TipoList,
                            Paginacao = new PaginacaoViewModel
                            {
                                PaginaCorrente = pagina,
                                TamanhoPagina = PAGE_SIZE,
                                TotalItens = numFuncionario,

                                CurrentNome = Funcionario
                            },
                            CurrentNome = Funcionario
                        }
                    );*/
            }

            // GET: Funcionario/Details/5
            public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Telefone,Email,Genero,Morada")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Telefone,Email,Genero,Morada")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}
