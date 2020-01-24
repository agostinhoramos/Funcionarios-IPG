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

    /*public class FeriasController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public FeriasController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Ferias
        public IActionResult  Index(int page = 1, string sort = null, string q = null,string o = "nome",int ipp = 10) {
            var fer = from p in _context.Feria select p;
            decimal nRows = fer.Count();

            if (ipp <= 1)
            {
                ipp = (int)Math.Ceiling(nRows);
            }

            int PAGES_BEFORE_AND_AFTER = ((int)nRows / ipp);

            if (nRows % ipp == 0)
            {
                PAGES_BEFORE_AND_AFTER -= 1;
            }


            FeriaViewModel vm =  new FeriaViewModel {
                mainURL = "Professores/Index",
                column = new string[] { "id", "nome", "contacto", "email", "gabinete" },
                PaginaCorrente = page,
                PaginaTotal = (int)Math.Ceiling(nRows / ipp),
                MostrarPrimeiraPagina = Math.Max(1, page - PAGES_BEFORE_AND_AFTER),
                IntensPorPagina = ipp,
                IntensInicial = ipp * (page - 1) > 0 ? ipp * (page - 1) + 1 : ((int)Math.Ceiling(nRows) < 1 ? 0 : 1),
                IntensFinal = ipp * page < (int)Math.Ceiling(nRows) ?
                ipp * page : (int)Math.Ceiling(nRows),
                TodosIntens = (int)Math.Ceiling(nRows)
            };
            // Algoritmo de pesquisa
            if (!String.IsNullOrEmpty(q))
            {
                vm.OpcaoCorrente = q;
                if (!String.IsNullOrEmpty(o))
                {
                    switch (o)
                    {
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType()) { Numq = Int32.Parse(q); }
                            fer = fer.Where(p => p.FeriaID.CompareTo(Numq) == 0);
                            break;
                        case "TipoFeria":
                            fer = fer.Where(p => p.TipoFeria.Contains(q));
                            break;
                        case "DataInicio":
                            fer = fer.Where(p => p.DataInicio.Contains(q));
                            break;
                        case "DataFim":
                            fer = fer.Where(p => p.DataFim.Contains(q));
                            break;
                        
                    }
                }
                else
                { // Avançada
                    String[] sep = { " " };
                    int word_limit = 20;
                    String[] data = q.Split(sep, word_limit, StringSplitOptions.RemoveEmptyEntries);
                    int len = data.Length - 1;
                    if (len > 0)
                    {
                        for (int i = 0; i < len; i++)
                        {
                            fer = fer.Where(p => p.TipoFeria.Contains(data[i]));
                        }
                    }
                    else
                    {
                        fer = fer.Where(p => p.TipoFeria.Contains(data[0]));
                    }
                }
            }


            // Algoritmo de ordenação de caracteres
            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o))
            {
                switch (o)
                {
                    case "id":
                        vm.Ferias = (sort == "1") ?
                            (fer.OrderBy(p => p.FeriaID).Skip((page - 1) * ipp).Take(ipp)) :
                            (fer.OrderByDescending(p => p.FeriaID).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "TipoFeria":
                        vm.Ferias = (sort == "1") ?
                            (fer.OrderBy(p => p.TipoFeria).Skip((page - 1) * ipp).Take(ipp)) :
                            (fer.OrderByDescending(p => p.TipoFeria).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "DataInicio":
                        vm.Ferias = (sort == "1") ?
                            (fer.OrderBy(p => p.DataInicio).Skip((page - 1) * ipp).Take(ipp)) :
                            (fer.OrderByDescending(p => p.DataInicio).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "DataFim":
                        vm.Ferias = (sort == "1") ?
                            (fer.OrderBy(p => p.DataFim).Skip((page - 1) * ipp).Take(ipp)) :
                            (fer.OrderByDescending(p => p.DataFim).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    
                }
                vm.Sort = sort;
            }
            else
            {
                vm.Ferias = fer.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.MostrarUltimaPagina= Math.Min(vm.PaginaTotal, page + PAGES_BEFORE_AND_AFTER);
            vm.OpcaoCorrente = o;

            return View(vm);
        }
    }

        // GET: Ferias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feria = await _context.Feria
                .FirstOrDefaultAsync(m => m.FeriaID == id);
            if (feria == null)
            {
                return NotFound();
            }

            return View(feria);
        }

        // GET: Ferias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ferias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeriaID,TipoFeria,DataInicio,DataFim")] Feria feria)
        {
        if (
                isEqual("Nome", feria.TipoFeria) ||
                isEqual("Contacto", feria.DataInicio) ||
                isEqual("Email", feria.DataFim) ||
                
                )
        {
            string repeated = isEqual("Nome", feria.TipoFeria) ? "Nome " : "";
            repeated += isEqual("Contacto", feria.DataInicio) ? "Contacto " : "";
            repeated += isEqual("Email", feria.DataFim) ? "Email " : "";
           

            ViewBag.type = "alert-danger";
            ViewBag.title = "Erro ao criar o professor";
            ViewBag.message = "Não foi possível criar novo Professor porque," +
                              "existem dados repetidos em todos ou um dos " +
                              "campos <strong>" + repeated + "</strong>";

            ViewBag.redirect = "/Feria/Create"; // Request.Path
            return View("message");
        }

        return View(feria);
        }

        // GET: Ferias/Edit/5
        public async Task<IActionResult> Edit(int? id,[Bind("FeriaID,TipoFeria,DataInicio,DataFim")] Feria feria)
        {
        if (id > 0 && FeriaExists(id))
        {
            feria.FeriaID = id;
        }

        if (ModelState.IsValid)
        {
            if (
                !isUnique("Nome", feria.TipoFeria, id) ||
                !isUnique("Email", feria.DataInicio, id) ||
                !isUnique("Contacto", feria.DataFim, id) 
               
               )
            {
                ViewBag.title = "Ocorreu um erro!";
                ViewBag.type = "alert-danger";
                ViewBag.message = "Já existe <strong>professores com o mesmo dados</strong>, por favor tente um dado diferente!";
                ViewBag.redirect = Request.Path;
                return View("message");
            }
            else
            {
                try
                {
                    _context.Update(feria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeriaExists(feria.FeriaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                ViewBag.title = "Atualização do professor";
                ViewBag.type = "alert-success";
                ViewBag.message = "Os dados do professor <strong>" + feria.TipoFeria + "</strong> foram <strong>atualizados</strong> com sucesso!";
                ViewBag.redirect = "/Professores/Index"; // Request.Path
                return View("message");
            }

        }
        return View(feria);
        }

        // POST: Ferias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeriaID,TipoFeria,DataInicio,DataFim,FuncionarioForeignKey,ProfessorForeignKey")] Feria feria)
        {
            if (id != feria.FeriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeriaExists(feria.FeriaID))
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
            return View(feria);
        }

        // GET: Ferias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feria = await _context.Feria
                .FirstOrDefaultAsync(m => m.FeriaID == id);
            if (feria == null)
            {
                return NotFound();
            }

            return View(feria);
        }

        // POST: Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feria = await _context.Feria.FindAsync(id);
            _context.Feria.Remove(feria);
            await _context.SaveChangesAsync();


        ViewBag.title = "Apagar Feria";
        ViewBag.type = "alert-success";
        ViewBag.message = "Os dados do professor <strong>" + feria.TipoFeria + "</strong> foram <strong>apagados</strong> com sucesso!";
        ViewBag.redirect = "/Ferias/Index"; // Request.Path




        return View ("message");
        }

        private bool FeriaExists(int id)
        {
            return _context.Feria.Any(e => e.FeriaID == id);
        }
    private bool isEqual(string type, string value)
    {
        bool result = false;
        switch (type)
        {
            case "TipoFeria":
                result = _context.Professor.Any(e => e.TipoFeria == value);
                break;
            case "DataInicio":
                result = _context.Professor.Any(e => e.DataInicio == value);
                break;
            case "DataFim":
                result = _context.Professor.Any(e => e.DataFim == value);
                break;
            
        }
        return result;
    }
*/
}


