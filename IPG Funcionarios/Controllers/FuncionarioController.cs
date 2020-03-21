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
            ViewBag.NameSortParm = String.IsNullOrEmpty(ordem) ? "name_desc" : "";

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
            int numeroPage = (pagina ?? 1);
            return View(await PaginacaoViewModel<Funcionario>.CreateAsync(funcionario.AsNoTracking(), numeroPage, PAGE_SIZE));
           
        }

        // GET: Funcionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .SingleOrDefaultAsync(m => m.FuncionarioId == id);
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
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Telefone,Email,Genero,Morada,DataNascionento")] Funcionario funcionario)
        {
            var email = funcionario.Email;
            var telefone = funcionario.Telefone;

            if (emailInvalido(email) == true) {
                //Mensagem de erro se o email for inválido
                ModelState.AddModelError("ERRO!","Este email já existe");
            }
            if (telefoneInvalido(telefone)) 
            {
                //Mensagem de erro se o nº de t já existe
                ModelState.AddModelError("ERRO!","Este email já existe");
            }

            /************/
            if (ModelState.IsValid)

            {
                if (!telefoneInvalido(telefone) || !emailInvalido(email))
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();

                    ViewBag.Title = " Adicionado!";
                    ViewBag.Message = "Novo funcionario criado Sucesso.";

                    // return RedirectToAction(nameof(Index));
                    return View("Sucesso");
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Telefone,Email,Genero,Morada,DataNascionento")] Funcionario funcionario)
        {
            var email = funcionario.Email;
            var telefone = funcionario.Telefone;
            var idf = funcionario.FuncionarioId;

            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }
            if (emailInvalidoEdit(email,idf)) {
                //Mensagem de erro se o email já existir

                ModelState.AddModelError("Email", "Email já existente");
             }

            //Validar telefone
            if (telefoneInvalidoEdit(telefone, idf))
            {
                //Mensagem de erro se o t já existir
                ModelState.AddModelError("Telefone", "Telefone já existente");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!telefoneInvalidoEdit(telefone,idf)||!emailInvalidoEdit(email,idf)) 
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
                //  return RedirectToAction(nameof(Index));
                ViewBag.Title = "Editado!";
                ViewBag.Message = "O funcionario foi editado com Sucesso.";

                return View("Sucesso");
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
                .SingleOrDefaultAsync(m => m.FuncionarioId == id);
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
            if (funcionario==null) {
                return NotFound();
             }
            try
            {


                _context.Funcionario.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
            catch
            {
                //  return RedirectToAction(nameof(Index));
                return View("ErrorDeleting");
            }

            ViewBag.Title = " Deletado!";
            ViewBag.Message = "Funcionario Deletado com  Sucesso.";
            return View("Sucesso");

            }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }

        private bool emailInvalido(string email)
        {
            bool invalido = false;

            //Procura na BD se existem  com o mesmo email
            var funcionario = from e in _context.Funcionario
                              where e.Email.Contains(email)
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }

        private bool telefoneInvalido(string telefone)
        {
            bool invalido = false;


            var funcionario = from e in _context.Funcionario
                              where e.Telefone.Contains(telefone)
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
        private bool emailInvalidoEdit(string email, int idf)
        {
            bool invalido = false;

            var funcionario = from e in _context.Funcionario
                              where e.Email.Contains(email) && e.FuncionarioId != idf
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
        private bool telefoneInvalidoEdit(string telefone, int idf)
        {
            bool invalido = false;

            var funcionario = from e in _context.Funcionario
                              where e.Telefone.Contains(telefone) && e.FuncionarioId != idf
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
    }
}
