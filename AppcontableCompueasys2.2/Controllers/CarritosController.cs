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
    public class CarritosController : Controller
    {
        private readonly DbcontableContext _context;

        public CarritosController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Carritoes
        public async Task<IActionResult> Index()
        {
            var dbcontableContext = _context.Carritos.Include(c => c.IdClienteNavigation).Include(c => c.IdProductoNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: Carritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCarrito == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Carritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarrito,IdProducto,IdCliente")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carrito.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carrito.IdProducto);
            return View(carrito);
        }

        // GET: Carritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carrito.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carrito.IdProducto);
            return View(carrito);
        }

        // POST: Carritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarrito,IdProducto,IdCliente")] Carrito carrito)
        {
            if (id != carrito.IdCarrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.IdCarrito))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", carrito.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", carrito.IdProducto);
            return View(carrito);
        }

        // GET: Carritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdCarrito == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carritos == null)
            {
                return Problem("Entity set 'DbcontableContext.Carritos'  is null.");
            }
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
          return (_context.Carritos?.Any(e => e.IdCarrito == id)).GetValueOrDefault();
        }
    }
}
