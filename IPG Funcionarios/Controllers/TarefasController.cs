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
    public class TarefasController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public TarefasController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Tarefas  ########## MODIFICADO! ###########
        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "nome", int ipp = 10)
        {

            var prof = from p in _context.Tarefa select p;
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

            TarefaViewModel vm = new TarefaViewModel
            {
                mainURL = "Tarefas/Index",
                column = new string[] { "id", "nome", "descricao", "data"},
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
                        case "descricao":
                            prof = prof.Where(p => p.Descricao.Contains(q));
                            break;
                        case "id":
                            int Numq = 0;
                            if (q.IsNumericType())
                            {
                                Numq = Int32.Parse(q);
                            }
                            prof = prof.Where(p => p.TarefaID.CompareTo(Numq) == 0);
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
                        vm.Tarefas = (sort == "1") ?
                            (prof.OrderBy(p => p.TarefaID).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.TarefaID).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "descricao":
                        vm.Tarefas = (sort == "1") ?
                            (prof.OrderBy(p => p.Descricao).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Descricao).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "data":
                        vm.Tarefas = (sort == "1") ?
                            (prof.OrderBy(p => p.Data).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Data).Skip((page - 1) * ipp).Take(ipp));
                        break;
                    case "nome":
                        vm.Tarefas = (sort == "1") ?
                            (prof.OrderBy(p => p.Nome).Skip((page - 1) * ipp).Take(ipp)) :
                            (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * ipp).Take(ipp));
                        break;
                }
                vm.Sort = sort;
            }
            else
            {
                vm.Tarefas = prof.Skip((page - 1) * ipp).Take(ipp);
            }

            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.CurrentOption = o;

            return View(vm);
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.TarefaID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaID,Descricao,Nome,Data")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                if (
                   isEqual("Nome", tarefa.Nome)
                   )
                {
                    ViewBag.type = "alert-danger";
                    ViewBag.title = "Erro ao criar a tarfefa";
                    ViewBag.message = "Não foi possível criar nova tarefa porque," +
                                      "existem dados repetidos no <strong>Nome</strong>.";

                    ViewBag.redirect = "/Tarefas/Create"; // Request.Path
                    return View("message");
                }
                else
                {
                    _context.Add(tarefa);
                    await _context.SaveChangesAsync();

                    ViewBag.type = "alert-success";
                    ViewBag.title = "Criação do professor";
                    ViewBag.message = "A tarefa <strong>" + tarefa.Nome + "</strong> foi <strong>criada</strong> com sucesso!";
                    ViewBag.redirect = "/Tarefas/Index"; // Request.Path
                    return View("message");
                }
            }

            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5  ########## MODIFICADO! ###########
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaID,Descricao,Nome,Data")] Tarefa tarefa)
        {
            if (id > 0 && TarefaExists(id))
            {
                tarefa.TarefaID = id;
            }

            if (ModelState.IsValid)
            {
                if (
                    !isUnique("Nome", tarefa.Nome, id)
                   )
                {
                    ViewBag.title = "Ocorreu um erro!";
                    ViewBag.type = "alert-danger";
                    ViewBag.message = "Já existe <strong>uma tarefa com o mesmo nome</strong>, por favor tente um nome diferente!";
                    ViewBag.redirect = Request.Path;
                    return View("message");
                }
                else
                {
                    try
                    {
                        _context.Update(tarefa);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TarefaExists(tarefa.TarefaID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    ViewBag.title = "Atualização da tarefa";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "Os dados da tarefa <strong>" + tarefa.Nome + "</strong> foram <strong>atualizados</strong> com sucesso!";
                    ViewBag.redirect = "/Tarefas/Index"; // Request.Path
                    return View("message");
                }

            }
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.TarefaID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5  ########## MODIFICADO! ###########
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();

            ViewBag.title = "Apagar tarefa";
            ViewBag.type = "alert-success";
            ViewBag.message = "Os dados da tarefa <strong>" + tarefa.Nome + "</strong> foram <strong>apagados</strong> com sucesso!";
            ViewBag.redirect = "/Tarefas/Index"; // Request.Path

            return View("message");
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.TarefaID == id);
        }

        // 2 tarefas não podem ter o mesmo nome
        private bool isEqual(string type, string value)
        {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Tarefa.Any(e => e.Nome == value);
                    break;
            }
            return result;
        }

        // É a única tarefa
        private bool isUnique(string type, string value, int id)
        {
            bool result = false;
            switch (type)
            {
                case "Nome":
                    result = _context.Tarefa.Any(e => e.Nome == value && e.TarefaID != id);
                    break;
            }
            return !result;
        }
    }
}