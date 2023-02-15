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
    public class DetalleComprasController : Controller
    {
        private readonly DbcontableContext _context;

        public DetalleComprasController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var dbcontableContext = _context.DetalleCompras.Include(d => d.IdClienteNavigation).Include(d => d.IdEmpresaNavigation).Include(d => d.IdFacturaNavigation).Include(d => d.IdProductoNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: DetalleCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdClienteNavigation)
                .Include(d => d.IdEmpresaNavigation)
                .Include(d => d.IdFacturaNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: DetalleCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleCompra,IdFactura,IdProducto,Cantidad,Total,IdCliente,IdEmpresa")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", detalleCompra.IdCliente);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", detalleCompra.IdEmpresa);
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleCompra.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", detalleCompra.IdCliente);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", detalleCompra.IdEmpresa);
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleCompra.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleCompra,IdFactura,IdProducto,Cantidad,Total,IdCliente,IdEmpresa")] DetalleCompra detalleCompra)
        {
            if (id != detalleCompra.IdDetalleCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCompraExists(detalleCompra.IdDetalleCompra))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", detalleCompra.IdCliente);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", detalleCompra.IdEmpresa);
            ViewData["IdFactura"] = new SelectList(_context.Facturas, "IdFactura", "IdFactura", detalleCompra.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleCompra.IdProducto);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleCompras == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdClienteNavigation)
                .Include(d => d.IdEmpresaNavigation)
                .Include(d => d.IdFacturaNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleCompras == null)
            {
                return Problem("Entity set 'DbcontableContext.DetalleCompras'  is null.");
            }
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra != null)
            {
                _context.DetalleCompras.Remove(detalleCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(int id)
        {
          return (_context.DetalleCompras?.Any(e => e.IdDetalleCompra == id)).GetValueOrDefault();
        }
    }
}
