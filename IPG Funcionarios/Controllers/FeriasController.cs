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
    public class FeriasController : Controller
    {
        private readonly IPGFuncionariosDbContext _context;

        public FeriasController(IPGFuncionariosDbContext context)
        {
            _context = context;
        }

        // GET: Ferias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feria.ToListAsync());
        }

        // GET: Ferias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feria = await _context.Feria
                .FirstOrDefaultAsync(m => m.FeriasID == id);
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
        public async Task<IActionResult> Create([Bind("FeriasID,TipoFerias,DataInicio,DataFim,QuemID")] Feria feria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feria);
        }

        // GET: Ferias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feria = await _context.Feria.FindAsync(id);
            if (feria == null)
            {
                return NotFound();
            }
            return View(feria);
        }

        // POST: Ferias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeriasID,TipoFerias,DataInicio,DataFim,QuemID")] Feria feria)
        {
            if (id != feria.FeriasID)
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
                    if (!FeriaExists(feria.FeriasID))
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
                .FirstOrDefaultAsync(m => m.FeriasID == id);
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
            return RedirectToAction(nameof(Index));
        }

        private bool FeriaExists(int id)
        {
            return _context.Feria.Any(e => e.FeriasID == id);
        }
    }
}
