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
    public class ClientesController : Controller
    {
        private readonly DbcontableContext _context;

        public ClientesController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var dbcontableContext = _context.Clientes.Include(c => c.IdCiudadNavigation).Include(c => c.IdDepartamentoNavigation).Include(c => c.IdEmpresaNavigation).Include(c => c.IdPaisNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdCiudadNavigation)
                .Include(c => c.IdDepartamentoNavigation)
                .Include(c => c.IdEmpresaNavigation)
                .Include(c => c.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id");
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Celular,Cedula,Fecha,IdEmpresa,IdPais,IdDepartamento,IdCiudad")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", cliente.IdPais);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", cliente.IdPais);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Celular,Cedula,Fecha,IdEmpresa,IdPais,IdDepartamento,IdCiudad")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", cliente.IdPais);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdCiudadNavigation)
                .Include(c => c.IdDepartamentoNavigation)
                .Include(c => c.IdEmpresaNavigation)
                .Include(c => c.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'DbcontableContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
