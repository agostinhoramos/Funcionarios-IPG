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

            var prof = from p in _context.Professor select p;

            decimal nRows = prof.Count();

            int PAGES_BEFORE_AND_AFTER = ((int)nRows / PRODUCTS_PER_PAGE );

            if (nRows % PRODUCTS_PER_PAGE == 0) {
                PAGES_BEFORE_AND_AFTER -= 1;
            }

            ProfessorViewModel vm = new ProfessorViewModel {
                CurrentPage = page,
                AllPages = (int)Math.Ceiling(nRows / PRODUCTS_PER_PAGE),
                FirstPage = Math.Max(1, page - PAGES_BEFORE_AND_AFTER),


                entries_per_page = PRODUCTS_PER_PAGE,
                entries_start = PRODUCTS_PER_PAGE * (page - 1) > 0 ? PRODUCTS_PER_PAGE * (page - 1) + 1 : ((int)Math.Ceiling(nRows) < 1 ? 0 : 1),
                entries_end = PRODUCTS_PER_PAGE * page < (int)Math.Ceiling(nRows) ?
                PRODUCTS_PER_PAGE * page : (int)Math.Ceiling(nRows),
                entries_all = (int)Math.Ceiling(nRows)
            };

            if (!String.IsNullOrEmpty(q)) {
                vm.CurrentSearch = q;
                if (!String.IsNullOrEmpty(o)) {
                    switch (o) {
                        case "nome":
                            prof = prof.Where(p => p.Nome.Contains(q));
                            break;
                        case "contacto":
                            prof = prof.Where(p => p.Contacto.Contains(q));
                            break;
                        case "email":
                            prof = prof.Where(p => p.Email.Contains(q));
                            break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(sort) && !String.IsNullOrEmpty(o)) {
                switch (o) {
                    case "id":
                        vm.Professor = (sort == "1") ? (prof.OrderBy(p => p.ProfessorId).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE)) :
                                                     (prof.OrderByDescending(p => p.ProfessorId).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE));
                        break;
                    case "nome":
                        vm.Professor = (sort == "1") ? (prof.OrderBy(p => p.Nome).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE)) :
                                                     (prof.OrderByDescending(p => p.Nome).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE));
                        break;
                    case "contacto":
                        vm.Professor = (sort == "1") ? (prof.OrderBy(p => p.Contacto).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE)) :
                                                     (prof.OrderByDescending(p => p.Contacto).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE));
                        break;
                    case "email":
                        vm.Professor = (sort == "1") ? (prof.OrderBy(p => p.Email).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE)) :
                                                     (prof.OrderByDescending(p => p.Email).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE));
                        break;
                    case "gabinete":
                        vm.Professor = (sort == "1") ? (prof.OrderBy(p => p.Gabinete).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE)) :
                                                     (prof.OrderByDescending(p => p.Gabinete).Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE));
                        break;
                }
                        vm.Sort = sort;
            } else {
                        vm.Professor = prof.Skip((page - 1) * PRODUCTS_PER_PAGE).Take(PRODUCTS_PER_PAGE);
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
            if (id > 0 && ProfessorExists(id)) {
                professor.ProfessorId = id;
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

                return View();
            }
            
            return View("message"); //professor 
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
