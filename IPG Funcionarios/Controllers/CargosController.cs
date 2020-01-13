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
    public class CargosController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public CargosController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Cargos  ########## MODIFICADO! ###########
        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "nome", int ipp = 10)
        {

            var prof = from p in _context.Cargo select p;
            decimal nRows = prof.Count();

            if (ipp <= 1)
            {
                ipp = (int)Math.Ceiling(nRows);
            }

            int PAGES_BEFORE_AND_AFTER = ((int)nRows / ipp);

            if (nRows % ipp == 0)
            {
                PAGES_BEFORE_AND_AFTER -= 1;
            }

            CargoViewModel vm = new CargoViewModel
            {
                mainURL = "Cargos/Index",
                column = new string[] { "id", "cargo" },
                CurrentPage = page,
                AllPages = (int)Math.Ceiling(nRows / ipp),
                FirstPage = Math.Max(1, page - PAGES_BEFORE_AND_AFTER),

                EntriesPerPage = ipp,
                EntriesStart = ipp * (page - 1) > 0 ? ipp * (page - 1) + 1 : ((int)Math.Ceiling(nRows) < 1 ? 0 : 1),
                EntriesEnd = ipp * page < (int)Math.Ceiling(nRows) ?
                ipp * page : (int)Math.Ceiling(nRows),
                EntriesAll = (int)Math.Ceiling(nRows)
            };

            // Algoritmo de pesquisa
            if (!String.IsNullOrEmpty(q))
            {
                vm.CurrentSearch = q;
                if (!String.IsNullOrEmpty(o))
                {
                    switch (o)
                    {
                        case "cargo":
                            prof = prof.Where(p => p.NomeCargo.Contains(q));
                            break;
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType())
                            {
                                Numq = Int32.Parse(q);
                            }
                            prof = prof.Where(p => p.CargoID.CompareTo(Numq) == 0);
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
                            prof = prof.Where(p => p.NomeCargo.Contains(data[i]));
                        }
                    }
                    else
                    {
                        prof = prof.Where(p => p.NomeCargo.Contains(data[0]));
                    }
                }
            }

            // Algoritmo de ordenação de caracteres
            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o))
            {
                switch (o)
                {
                    case "id":
                        vm.Cargos = (sort == "1") ?
                            (prof.OrderBy(p => p.CargoID).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.CargoID).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "cargo":
                        vm.Cargos = (sort == "1") ?
                            (prof.OrderBy(p => p.NomeCargo).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.NomeCargo).Skip((page - 1) * ipp).Take(ipp));
                        break;
                }
                vm.Sort = sort;
            }
            else
            {
                vm.Cargos = prof.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.CurrentOption = o;

            return View(vm);
        }

        // GET: Cargos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.CargoID == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargos/Create  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CargoID,NomeCargo")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                if (
                   isEqual("NomeCargo", cargo.NomeCargo)
                   )
                {
                    string repeated = isEqual("NomeCargo", cargo.NomeCargo) ? "NomeCargo " : "";

                    ViewBag.type = "alert-danger";
                    ViewBag.title = "Erro ao criar o Cargo";
                    ViewBag.message = "Não foi possível criar novo Cargo porque," +
                                      "existem dados repetidos no " +
                                      "campo <strong>" + repeated + "</strong>";

                    ViewBag.redirect = "/Cargos/Create"; // Request.Path
                    return View("message");
                }
                else
                {
                    _context.Add(cargo);
                    await _context.SaveChangesAsync();

                    ViewBag.type = "alert-success";
                    ViewBag.title = "Criação do professor";
                    ViewBag.message = "O professor <strong>" + cargo.NomeCargo + "</strong> <strong>criado</strong> com sucesso!";
                    ViewBag.redirect = "/Cargos/Index"; // Request.Path
                    return View("message");
                }
            }

            return View(cargo);
        }

        // GET: Cargos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST: Cargos/Edit/5  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CargoID,NomeCargo")] Cargo cargo)
        {
            if (id > 0 && CargoExists(id))
            {
                cargo.CargoID = id;
            }

            if (ModelState.IsValid)
            {
                if (
                    !isUnique("NomeCargo", cargo.NomeCargo, id)
                   )
                {
                    ViewBag.title = "Ocorreu um erro!";
                    ViewBag.type = "alert-danger";
                    ViewBag.message = "Já existe <strong>cargo com o mesmo dados</strong>, por favor tente um dado diferente!";
                    ViewBag.redirect = Request.Path;
                    return View("message");
                }
                else
                {
                    try
                    {
                        _context.Update(cargo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CargoExists(cargo.CargoID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    ViewBag.title = "Atualização do Cargo";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "O dado do cargo <strong>" + cargo.NomeCargo + "</strong> foi <strong>atualizados</strong> com sucesso!";
                    ViewBag.redirect = "/Cargos/Index"; // Request.Path
                    return View("message");
                }

            }

            return View(cargo);
        }

        // GET: Cargos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.CargoID == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargos/Delete/5  ########## MODIFICADO! ###########
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);
            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();

            ViewBag.title = "Apagar cargo";
            ViewBag.type = "alert-success";
            ViewBag.message = "O dado do cargo <strong>" + cargo.NomeCargo + "</strong> foi <strong>apagado</strong> com sucesso!";
            ViewBag.redirect = "/Cargos/Index"; // Request.Path

            return View("message");
        }

        private bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.CargoID == id);
        }

        private bool isEqual(string type, string value)
        {
            bool result = false;
            switch (type)
            {
                case "NomeCargo":
                    result = _context.Cargo.Any(e => e.NomeCargo == value);
                    break;
            }
            return result;
        }

        private bool isUnique(string type, string value, int id)
        {
            bool result = false;
            switch (type)
            {
                case "NomeCargo":
                    result = _context.Cargo.Any(e => e.NomeCargo == value && e.CargoID != id);
                    break;
            }
            return !result;
        }
    }
}
