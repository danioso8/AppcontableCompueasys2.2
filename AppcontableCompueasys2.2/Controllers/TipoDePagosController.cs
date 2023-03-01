using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppcontableCompueasys2._2.Controllers
{
    [Authorize]
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            return _context.TipoDePagos != null ? 
                          View(await _context.TipoDePagos.ToListAsync()) :
                          Problem("Entity set 'DbcontableContext.TipoDePagos'  is null.");
        }

        // GET: TipoDePagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();
            ViewBag.IdEmpresa = empresa.Id;
            return View();
        }

        // POST: TipoDePagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,IdEmpresa")] TipoDePago tipoDePago)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (ModelState.IsValid)
            {
                _context.Add(tipoDePago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDePago);
        }

        
        public  IActionResult Get()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();

          
                var dbcontableContext =  _context.TipoDePagos.Where(e => e.IdEmpresa == empresa.Id);
              
            return StatusCode( StatusCodes.Status200OK,  dbcontableContext);
        }

        // GET: TipoDePagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
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
