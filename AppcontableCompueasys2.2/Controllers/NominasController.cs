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
    public class NominasController : Controller
    {
        private readonly DbcontableContext _context;

        public NominasController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Nominas
        public async Task<IActionResult> Index()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var dbcontableContext = _context.Nominas.Include(n => n.IdEmpleadoNavigation).Include(n => n.IdEmpresaNavigation).Include(n => n.IdUsuarioNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: Nominas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas
                .Include(n => n.IdEmpleadoNavigation)
                .Include(n => n.IdEmpresaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomina == null)
            {
                return NotFound();
            }

            return View(nomina);
        }

        // GET: Nominas/Create
        public IActionResult Create()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Id");
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Nominas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpleado,Cantidad,Valor,TotalApagar,IdUsuario,IdEmpresa,FechaPago,Descuentos,ValorEps,ValorPension,ConseptoNomina,ConseptoDescuentos,Auxilio")] Nomina nomina)
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
                _context.Add(nomina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Id", nomina.IdEmpleado);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", nomina.IdEmpresa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", nomina.IdUsuario);
            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Id", nomina.IdEmpleado);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", nomina.IdEmpresa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", nomina.IdUsuario);
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpleado,Cantidad,Valor,TotalApagar,IdUsuario,IdEmpresa,FechaPago,Descuentos,ValorEps,ValorPension,ConseptoNomina,ConseptoDescuentos,Auxilio")] Nomina nomina)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id != nomina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nomina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NominaExists(nomina.Id))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "Id", "Id", nomina.IdEmpleado);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", nomina.IdEmpresa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", nomina.IdUsuario);
            return View(nomina);
        }

        // GET: Nominas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Nominas == null)
            {
                return NotFound();
            }

            var nomina = await _context.Nominas
                .Include(n => n.IdEmpleadoNavigation)
                .Include(n => n.IdEmpresaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomina == null)
            {
                return NotFound();
            }

            return View(nomina);
        }

        // POST: Nominas/Delete/5
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
            if (_context.Nominas == null)
            {
                return Problem("Entity set 'DbcontableContext.Nominas'  is null.");
            }
            var nomina = await _context.Nominas.FindAsync(id);
            if (nomina != null)
            {
                _context.Nominas.Remove(nomina);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NominaExists(int id)
        {
          return (_context.Nominas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
