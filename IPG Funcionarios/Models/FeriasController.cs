using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IPG_Funcionarios.Models
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
            return View(await _context.Ferias.ToListAsync());
        }

        // GET: Ferias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ferias = await _context.Ferias
                .FirstOrDefaultAsync(m => m.FeriasID == id);
            if (ferias == null)
            {
                return NotFound();
            }

            return View(ferias);
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
        public async Task<IActionResult> Create([Bind("FeriasID,TipoFerias,DataInicio,DataFim,QuemID")] Ferias ferias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ferias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ferias);
        }

        // GET: Ferias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ferias = await _context.Ferias.FindAsync(id);
            if (ferias == null)
            {
                return NotFound();
            }
            return View(ferias);
        }

        // POST: Ferias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeriasID,TipoFerias,DataInicio,DataFim,QuemID")] Ferias ferias)
        {
            if (id != ferias.FeriasID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ferias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeriasExists(ferias.FeriasID))
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
            return View(ferias);
        }

        // GET: Ferias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ferias = await _context.Ferias
                .FirstOrDefaultAsync(m => m.FeriasID == id);
            if (ferias == null)
            {
                return NotFound();
            }

            return View(ferias);
        }

        // POST: Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ferias = await _context.Ferias.FindAsync(id);
            _context.Ferias.Remove(ferias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeriasExists(int id)
        {
            return _context.Ferias.Any(e => e.FeriasID == id);
        }
    }
}
