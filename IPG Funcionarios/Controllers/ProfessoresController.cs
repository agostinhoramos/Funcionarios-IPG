using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPG_Funcionarios.Models;
using Nest;

namespace IPG_Funcionarios.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public ProfessoresController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Professores  ########## MODIFICADO! ###########
        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "name", int ipp = 10) {

            var prof = from p in _context.Professor select p;
            decimal nRows = prof.Count();

            if (ipp <= 1) {
                ipp = (int)Math.Ceiling(nRows);
            }

            int PAGES_BEFORE_AND_AFTER = ((int)nRows / ipp);

            if (nRows % ipp == 0) {
                PAGES_BEFORE_AND_AFTER -= 1;
            }

            ProfessorViewModel vm = new ProfessorViewModel {
                mainURL = "Professores/Index",
                column = new string[] { "id", "nome", "contacto", "email", "gabinete" },
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
            if (!String.IsNullOrEmpty(q)) {
                vm.CurrentSearch = q;
                if (!String.IsNullOrEmpty(o))
                {
                    switch (o)
                    {
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType()) { Numq = Int32.Parse(q); }
                            prof = prof.Where(p => p.ProfessorId.CompareTo(Numq) == 0);
                            break;
                        case "nome":
                            prof = prof.Where(p => p.Nome.Contains(q));
                            break;
                        case "contacto":
                            prof = prof.Where(p => p.Contacto.Contains(q));
                            break;
                        case "email":
                            prof = prof.Where(p => p.Email.Contains(q));
                            break;
                        case "gabinete":
                            prof = prof.Where(p => p.Gabinete.CompareTo(q) == 0);
                            break;
                    }
                }
                else { // Avançada
                    String[] sep = { " " };
                    int word_limit = 20;
                    String[] data = q.Split(sep, word_limit, StringSplitOptions.RemoveEmptyEntries);
                    int len = data.Length - 1;
                    if (len > 0) {
                        for (int i = 0; i < len; i++)
                        {
                            prof = prof.Where(p => p.Nome.Contains(data[i]));
                        }
                    }
                    else {
                        prof = prof.Where(p => p.Nome.Contains(data[0]));
                    }
                }
            }

            // Algoritmo de ordenação de caracteres
            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o)) {
                switch (o) {
                    case "id":
                        vm.Professor = (sort == "1") ?
                            (prof.OrderBy(p => p.ProfessorId).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.ProfessorId).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "nome":
                        vm.Professor = (sort == "1") ?
                            (prof.OrderBy(p => p.Nome).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "contacto":
                        vm.Professor = (sort == "1") ?
                            (prof.OrderBy(p => p.Contacto).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Contacto).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "email":
                        vm.Professor = (sort == "1") ?
                            (prof.OrderBy(p => p.Email).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Email).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "gabinete":
                        vm.Professor = (sort == "1") ?
                            (prof.OrderBy(p => p.Gabinete).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Gabinete).Skip((page - 1) * ipp).Take(ipp));
                        break;
                }
                vm.Sort = sort;
            } else {
                vm.Professor = prof.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.CurrentOption = o;

            return View(vm);
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professores/Create ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,Contacto,Email,Gabinete")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                if (
                   isEqual("Nome", professor.Nome) ||
                   isEqual("Contacto", professor.Contacto) ||
                   isEqual("Email", professor.Email) ||
                   isEqual("Gabinete", professor.Gabinete)
                   )
                {
                    string repeated = isEqual("Nome", professor.Nome) ? "Nome " : "";
                    repeated += isEqual("Contacto", professor.Contacto) ? "Contacto " : "";
                    repeated += isEqual("Email", professor.Email) ? "Email " : "";
                    repeated += isEqual("Gabinete", professor.Gabinete) ? "Gabinete " : "";

                    ViewBag.type = "alert-danger";
                    ViewBag.title = "Erro ao criar o professor";
                    ViewBag.message = "Não foi possível criar novo Professor porque," +
                                      "existem dados repetidos em todos ou um dos " +
                                      "campos <strong>" + repeated + "</strong>";

                    ViewBag.redirect = "/Professores/Create"; // Request.Path
                    return View("message");
                }
                else {
                    _context.Add(professor);
                    await _context.SaveChangesAsync();

                    ViewBag.type = "alert-success";
                    ViewBag.title = "Criação do professor";
                    ViewBag.message = "O professor <strong>" + professor.Nome + "</strong> <strong>criado</strong> com sucesso!";
                    ViewBag.redirect = "/Professores/Index"; // Request.Path
                    return View("message");
                }
            }

            return View(professor);
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5 ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,Contacto,Email,Gabinete")] Professor professor)
        {
            if (id > 0 && ProfessorExists(id)) {
                professor.ProfessorId = id;
            }

            if (ModelState.IsValid)
            {
                if (
                    !isUnique("Nome", professor.Nome, id) ||
                    !isUnique("Email", professor.Email, id) ||
                    !isUnique("Contacto", professor.Contacto, id) ||
                    !isUnique("Gabinete", professor.Gabinete, id)
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
                        _context.Update(professor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProfessorExists(professor.ProfessorId))
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
                    ViewBag.message = "Os dados do professor <strong>" + professor.Nome + "</strong> foram <strong>atualizados</strong> com sucesso!";
                    ViewBag.redirect = "/Professores/Index"; // Request.Path
                    return View("message");
                }

            }

            return View(professor);
        }

        // GET: Professores/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professores/Delete/5 ########## MODIFICADO! ###########
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();

            ViewBag.title = "Apagar professor";
            ViewBag.type = "alert-success";
            ViewBag.message = "Os dados do professor <strong>" + professor.Nome + "</strong> foram <strong>apagados</strong> com sucesso!";
            ViewBag.redirect = "/Professores/Index"; // Request.Path

            return View("message");
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.ProfessorId == id);
        }

        private bool isEqual(string type, string value) {
            bool result = false;
            switch (type) {
                case "Nome":
                    result = _context.Professor.Any(e => e.Nome == value);
                    break;
                case "Email":
                    result = _context.Professor.Any(e => e.Email == value);
                    break;
                case "Contacto":
                    result = _context.Professor.Any(e => e.Contacto == value);
                    break;
                case "Gabinete":
                    result = _context.Professor.Any(e => e.Gabinete == value);
                    break;
            }
            return result;
        }

        // É o único professor com [type], com exceção de [id]
        private bool isUnique(string type, string value, int id) {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Professor.Any(e => e.Nome == value && e.ProfessorId != id);
                    break;
                case "Email":
                    result = _context.Professor.Any(e => e.Email == value && e.ProfessorId != id);
                    break;
                case "Contacto":
                    result = _context.Professor.Any(e => e.Contacto == value && e.ProfessorId != id);
                    break;
                case "Gabinete":
                    result = _context.Professor.Any(e => e.Gabinete == value && e.ProfessorId != id);
                    break;
            }
            return !result;
        }

    }

}
