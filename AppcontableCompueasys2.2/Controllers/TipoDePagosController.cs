using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;

namespace AppcontableCompueasys2._2.Controllers
{
    public class TipoDePagosController : Controller
    {
        private readonly DbcontableContext _context;

        public TipoDePagosController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: TipoDePagoes
        public async Task<IActionResult> Index()
        {
              return _context.TipoDePagos != null ? 
                          View(await _context.TipoDePagos.ToListAsync()) :
                          Problem("Entity set 'DbcontableContext.TipoDePagos'  is null.");
        }

        // GET: TipoDePagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoDePagos == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDePago == null)
            {
                return NotFound();
            }

            return View(tipoDePago);
        }

        // GET: TipoDePagoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDePagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] TipoDePago tipoDePago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDePago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDePago);
        }

        // GET: TipoDePagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoDePagos == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePagos.FindAsync(id);
            if (tipoDePago == null)
            {
                return NotFound();
            }
            return View(tipoDePago);
        }

        // POST: TipoDePagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] TipoDePago tipoDePago)
        {
            if (id != tipoDePago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDePago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDePagoExists(tipoDePago.Id))
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
            return View(tipoDePago);
        }

        // GET: TipoDePagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoDePagos == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDePago == null)
            {
                return NotFound();
            }

            return View(tipoDePago);
        }

        // POST: TipoDePagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoDePagos == null)
            {
                return Problem("Entity set 'DbcontableContext.TipoDePagos'  is null.");
            }
            var tipoDePago = await _context.TipoDePagos.FindAsync(id);
            if (tipoDePago != null)
            {
                _context.TipoDePagos.Remove(tipoDePago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDePagoExists(int id)
        {
          return (_context.TipoDePagos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
