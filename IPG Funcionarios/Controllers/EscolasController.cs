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
    public class EscolasController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public EscolasController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Escolas  ########## MODIFICADO! ###########
        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "nome", int ipp = 10)
        {

            var prof = from p in _context.Escola select p;
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

            EscolaViewModel vm = new EscolaViewModel
            {
                mainURL = "Escolas/Index",
                column = new string[] { "id", "nome", "localizacao", "descricao" },
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
                        case "nome":
                            prof = prof.Where(p => p.Nome.Contains(q));
                            break;
                        case "localizacao":
                            prof = prof.Where(p => p.Localizacao.Contains(q));
                            break;
                        case "descricao":
                            prof = prof.Where(p => p.Descricao.Contains(q));
                            break;
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType())
                            {
                                Numq = Int32.Parse(q);
                            }
                            prof = prof.Where(p => p.EscolaID.CompareTo(Numq) == 0);
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
                            prof = prof.Where(p => p.Nome.Contains(data[i]));
                        }
                    }
                    else
                    {
                        prof = prof.Where(p => p.Nome.Contains(data[0]));
                    }
                }
            }

            // Algoritmo de ordenacao de caracteres
            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o))
            {
                switch (o)
                {
                    case "id":
                        vm.Escolas = (sort == "1") ?
                            (prof.OrderBy(p => p.EscolaID).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.EscolaID).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "nome":
                        vm.Escolas = (sort == "1") ?
                            (prof.OrderBy(p => p.Nome).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "localizacao":
                        vm.Escolas = (sort == "1") ?
                            (prof.OrderBy(p => p.Localizacao).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Localizacao).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "descricao":
                        vm.Escolas = (sort == "1") ?
                            (prof.OrderBy(p => p.Descricao).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Descricao).Skip((page - 1) * ipp).Take(ipp));
                        break;
                }
                vm.Sort = sort;
            }
            else
            {
                vm.Escolas = prof.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.CurrentOption = o;

            return View(vm);
        }

        // GET: Escolas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escola = await _context.Escola
                .FirstOrDefaultAsync(m => m.EscolaID == id);
            if (escola == null)
            {
                return NotFound();
            }

            return View(escola);
        }

        // GET: Escolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escolas/Create  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscolaID,Nome,Localizacao,Descricao")] Escola escola)
        {
            if (ModelState.IsValid)
            {
                if (
                   isEqual("Nome", escola.Nome)
                   )
                {
                    ViewBag.type = "alert-danger";
                    ViewBag.title = "Erro ao criar a escola";
                    ViewBag.message = "Não foi possível criar nova escola porque," +
                                      "existem dados repetidos no <strong>Nome</strong>.";

                    ViewBag.redirect = "/Escolas/Create"; // Request.Path
                    return View("message");
                }
                else
                {
                    _context.Add(escola);
                    await _context.SaveChangesAsync();

                    ViewBag.type = "alert-success";
                    ViewBag.title = "Criação da escola";
                    ViewBag.message = "A escola <strong>" + escola.Nome + "</strong> foi <strong>criada</strong> com sucesso!";
                    ViewBag.redirect = "/Escolas/Index"; // Request.Path
                    return View("message");
                }
            }

            return View(escola);
        }

        // GET: Escolas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escola = await _context.Escola.FindAsync(id);
            if (escola == null)
            {
                return NotFound();
            }
            return View(escola);
        }

        // POST: Escolas/Edit/5  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EscolaID,Nome,Localizacao,Descricao")] Escola escola)
        {
            if (id > 0 && EscolaExists(id))
            {
                escola.EscolaID = id;
            }

            if (ModelState.IsValid)
            {
                if (
                    !isUnique("Nome", escola.Nome, id)
                   )
                {
                    ViewBag.title = "Ocorreu um erro!";
                    ViewBag.type = "alert-danger";
                    ViewBag.message = "Já existe <strong>uma escola com o mesmo nome</strong>, por favor tente um nome diferente!";
                    ViewBag.redirect = Request.Path;
                    return View("message");
                }
                else
                {
                    try
                    {
                        _context.Update(escola);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EscolaExists(escola.EscolaID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    ViewBag.title = "Atualização da escola";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "Os dados da escola <strong>" + escola.Nome + "</strong> foram <strong>atualizados</strong> com sucesso!";
                    ViewBag.redirect = "/Escolas/Index"; // Request.Path
                    return View("message");
                }

            }
            return View(escola);
        }

        // GET: Escolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escola = await _context.Escola
                .FirstOrDefaultAsync(m => m.EscolaID == id);
            if (escola == null)
            {
                return NotFound();
            }

            return View(escola);
        }

        // POST: Escolas/Delete/5  ########## MODIFICADO! ###########
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escola = await _context.Escola.FindAsync(id);
            _context.Escola.Remove(escola);
            await _context.SaveChangesAsync();

            ViewBag.title = "Apagar tarefa";
            ViewBag.type = "alert-success";
            ViewBag.message = "Os dados da escola <strong>" + escola.Nome + "</strong> foram <strong>apagados</strong> com sucesso!";
            ViewBag.redirect = "/Escolas/Index"; // Request.Path

            return View("message");
        }

        private bool EscolaExists(int id)
        {
            return _context.Escola.Any(e => e.EscolaID == id);
        }

        private bool isEqual(string type, string value)
        {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Escola.Any(e => e.Nome == value);
                    break;
            }
            return result;
        }

        // É o único professor com [type], com exceção de [id]
        private bool isUnique(string type, string value, int id)
        {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Escola.Any(e => e.Nome == value && e.EscolaID != id);
                    break;
            }
            return !result;
        }
    }
}
