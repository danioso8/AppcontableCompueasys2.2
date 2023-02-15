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
    public class FacturasController : Controller
    {
        private readonly DbcontableContext _context;

        public FacturasController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var dbcontableContext = _context.Facturas.Include(f => f.IdClienteNavigation).Include(f => f.IdProductoNavigation).Include(f => f.IdTipoDePagoNavigation).Include(f => f.IdUsuarioNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }
            

           
            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdProductoNavigation)
                .Include(f => f.IdTipoDePagoNavigation)
                .Include(f => f.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }


        //public  Task<IActionResult> imprimirFactura(int? id)
        //{
        //    ViewBag.company = TempData["company"];
        //    ViewBag.name = TempData["name"];
        //    string company = ViewBag.company;
        //    var name = ViewBag.name;
        //    TempData["company"] = company;
        //    TempData["name"] = name;
        //    ViewBag.id = TempData["id"];
        //    //if (id == null || _context.Facturas == null)
        //    //{
        //    //    return NotFound();
        //    //}



        //    //var factura = await _context.Facturas
        //    //    .Include(f => f.IdClienteNavigation)
        //    //    .Include(f => f.IdProductoNavigation)
        //    //    .Include(f => f.IdTipoDePagoNavigation)
        //    //    .Include(f => f.IdUsuarioNavigation)
        //    //    .FirstOrDefaultAsync(m => m.IdFactura == id);
        //    //if (factura == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View();
        //}



        // GET: Facturas/Create
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
           
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,IdUsuario,CantidadProducto,Total,FechaCompra,IdCliente,IdProducto,Iva,Descuento,Observaciones,EstadoFactura,IdTipoDePago")] Factura factura)
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
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", factura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", factura.IdProducto);
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion", factura.IdTipoDePago);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", factura.IdUsuario);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", factura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", factura.IdProducto);
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion", factura.IdTipoDePago);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", factura.IdUsuario);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,IdUsuario,CantidadProducto,Total,FechaCompra,IdCliente,IdProducto,Iva,Descuento,Observaciones,EstadoFactura,IdTipoDePago")] Factura factura)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", factura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre", factura.IdProducto);
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion", factura.IdTipoDePago);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", factura.IdUsuario);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdProductoNavigation)
                .Include(f => f.IdTipoDePagoNavigation)
                .Include(f => f.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
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
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'DbcontableContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
