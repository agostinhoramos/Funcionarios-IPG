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
    public class ServicosController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public ServicosController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Servicos ########## MODIFICADO! ###########
        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "nome", int ipp = 10)
        {

            var prof = from p in _context.Servico select p;
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

            ServicoViewModel vm = new ServicoViewModel
            {
                mainURL = "Servicos/Index",
                column = new string[] { "id", "nome" },
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
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType())
                            {
                                Numq = Int32.Parse(q);
                            }
                            prof = prof.Where(p => p.ServicoId.CompareTo(Numq) == 0);
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

            // Algoritmo de ordenação de caracteres
            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o))
            {
                switch (o)
                {
                    case "id":
                        vm.Servicos = (sort == "1") ?
                            (prof.OrderBy(p => p.ServicoId).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "nome":
                        vm.Servicos = (sort == "1") ?
                            (prof.OrderBy(p => p.ServicoId).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                        break;
                }
                vm.Sort = sort;
            }
            else
            {
                vm.Servicos = prof.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.CurrentOption = o;

            return View(vm);
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoId,Nome")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                if (
                   isEqual("Nome", servico.Nome)
                   )
                {
                    ViewBag.type = "alert-danger";
                    ViewBag.title = "Erro ao criar o serviço";
                    ViewBag.message = "Não foi possível criar novo serviço porque," +
                                      "existem dados repetidos no <strong>Nome</strong>.";

                    ViewBag.redirect = "/Servicos/Create"; // Request.Path
                    return View("message");
                }
                else
                {
                    _context.Add(servico);
                    await _context.SaveChangesAsync();

                    ViewBag.type = "alert-success";
                    ViewBag.title = "Criação do professor";
                    ViewBag.message = "A tarefa <strong>" + servico.Nome + "</strong> foi <strong>criada</strong> com sucesso!";
                    ViewBag.redirect = "/Servicos/Index"; // Request.Path
                    return View("message");
                }
            }

            return View(servico);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoId,Nome")] Servico servico)
        {
            if (id > 0 && ServicoExists(id))
            {
                servico.ServicoId = id;
            }

            if (ModelState.IsValid)
            {
                if (
                    !isUnique("Nome", servico.Nome, id)
                   )
                {
                    ViewBag.title = "Ocorreu um erro!";
                    ViewBag.type = "alert-danger";
                    ViewBag.message = "Já existe <strong>um serviço com o mesmo nome</strong>, por favor tente um nome diferente!";
                    ViewBag.redirect = Request.Path;
                    return View("message");
                }
                else
                {
                    try
                    {
                        _context.Update(servico);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ServicoExists(servico.ServicoId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    ViewBag.title = "Atualização do serviço";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "Os dados do serviço <strong>" + servico.Nome + "</strong> foram <strong>atualizados</strong> com sucesso!";
                    ViewBag.redirect = "/Servicos/Index"; // Request.Path
                    return View("message");
                }

            }
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5  ########## MODIFICADO! ###########
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(servico);
            await _context.SaveChangesAsync();

            ViewBag.title = "Apagar tarefa";
            ViewBag.type = "alert-success";
            ViewBag.message = "Os dados do serviço <strong>" + servico.Nome + "</strong> foram <strong>apagados</strong> com sucesso!";
            ViewBag.redirect = "/Servicos/Index"; // Request.Path

            return View("message");
        }

        private bool ServicoExists(int id)
        {
            return _context.Servico.Any(e => e.ServicoId == id);
        }

        private bool isEqual(string type, string value)
        {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Servico.Any(e => e.Nome == value);
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
                    result = _context.Servico.Any(e => e.Nome == value && e.ServicoId != id);
                    break;
            }
            return !result;
        }
    }
}
