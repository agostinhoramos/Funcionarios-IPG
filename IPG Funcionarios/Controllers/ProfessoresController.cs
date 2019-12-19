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
    public class ProfessoresController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        private int PRODUCTS_PER_PAGE = 10;

        public ProfessoresController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, string sort = null, string q = null, string o = "nome") {
            
            decimal nRows = _context.Professor.Count();

            int PAGES_BEFORE_AND_AFTER = ((int)nRows / PRODUCTS_PER_PAGE );

            if (nRows % PRODUCTS_PER_PAGE == 0) {
                PAGES_BEFORE_AND_AFTER -= 1;
            }

            ProfessorViewModel vm = new ProfessorViewModel {
                Professor = _context.Professor.Take((int)nRows),
                CurrentPage = page,
                AllPages = (int)Math.Ceiling(nRows / PRODUCTS_PER_PAGE),
                FirstPage = Math.Max(2, page - PAGES_BEFORE_AND_AFTER),


                entries_per_page = PRODUCTS_PER_PAGE,
                entries_start = PRODUCTS_PER_PAGE * (page - 1) > 0 ? PRODUCTS_PER_PAGE * (page - 1) + 1 : ((int)Math.Ceiling(nRows) < 1 ? 0 : 1),
                entries_end = PRODUCTS_PER_PAGE * page < (int)Math.Ceiling(nRows) ?
                PRODUCTS_PER_PAGE * page : (int)Math.Ceiling(nRows),
                entries_all = (int)Math.Ceiling(nRows)
            };

            if (!String.IsNullOrEmpty(q) && !String.IsNullOrEmpty(o)) {
                vm.CurrentSearch = q;
                switch (o) {
                    case "nome":
                        vm.Professor = vm.Professor.Where(p => p.Nome.Contains(q, StringComparison.CurrentCultureIgnoreCase));
                        vm.CurrentOption = "nome";
                        break;
                    case "contacto":
                        vm.Professor = vm.Professor.Where(p => p.Contacto.Contains(q, StringComparison.CurrentCultureIgnoreCase));
                        vm.CurrentOption = "contacto";
                        break;
                    case "email":
                        vm.Professor = vm.Professor.Where(p => p.Email.Contains(q, StringComparison.CurrentCultureIgnoreCase));
                        vm.CurrentOption = "email";
                        break;
                }
            }

            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o)) {
                switch (o) {
                    case "id":
                        vm.Professor = sort == "1" ? vm.Professor.OrderBy(p => p.ProfessorId) :
                                                     vm.Professor.OrderByDescending(p => p.ProfessorId);
                        break;
                    case "nome":
                        vm.Professor = sort == "1" ? vm.Professor.OrderBy(p => p.Nome) :
                                                     vm.Professor.OrderByDescending(p => p.Nome);
                        break;
                    case "contacto":
                        vm.Professor = sort == "1" ? vm.Professor.OrderBy(p => p.Contacto) :
                                                     vm.Professor.OrderByDescending(p => p.Contacto);
                        break;
                    case "email":
                        vm.Professor = sort == "1" ? vm.Professor.OrderBy(p => p.Email) :
                                                     vm.Professor.OrderByDescending(p => p.Email);
                        break;
                    case "gabinete":
                        vm.Professor = sort == "1" ? vm.Professor.OrderBy(p => p.Gabinete) :
                                                     vm.Professor.OrderByDescending(p => p.Gabinete);
                        break;
                }
                vm.Sort = sort;
            }

            vm.Professor = vm.Professor.Skip((page - 1) * PRODUCTS_PER_PAGE);
            vm.Professor = vm.Professor.Take(PRODUCTS_PER_PAGE);
            vm.LastPage = Math.Min(vm.AllPages, page + PAGES_BEFORE_AND_AFTER);
            vm.FirstPage = 1;
            vm.LastPage = vm.AllPages;
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

        // POST: Professores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,Contacto,Email,Gabinete")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        // POST: Professores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,Contacto,Email,Gabinete")] Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
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

        // POST: Professores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.ProfessorId == id);
        }
    }
}
