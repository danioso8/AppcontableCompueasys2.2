using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NuGet.Packaging;

namespace AppcontableCompueasys2._2.Controllers
{
    [Authorize]
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

            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();

            //Obtener datos de la vista
            //(IFormColletion form)
            // int idProductoSeleccionado = int.Parse(form["producto"]);
            //var productoSeleccionado = _dbContext.Productos.FirstOrDefault(p => p.Id == idProductoSeleccionado);

           


            var dbcontableContext = _context.Facturas.Include(f => f.IdClienteNavigation).Include(f => f.IdUsuarioNavigation).Include(f => f.IdEmpresaNavigation).Where(e=> e.IdEmpresa == empresa.Id);

            return View(await dbcontableContext.ToListAsync());
        }


        public IActionResult Get()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();


            var dbcontableContext = _context.Facturas.Where(f => f.IdEmpresa == empresa.Id);

            return StatusCode(StatusCodes.Status200OK, dbcontableContext);
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
            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();
            ViewBag.detalleCompra = _context.DetalleCompras.Where(c => c.IdEmpresa == empresa.Id && c.IdFactura == id).ToList();
           
           
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }
            

           
            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdUsuarioNavigation)
                .Include(f => f.IdEmpresaNavigation)
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

            //Cargando datos del cliente
            ViewBag.iduser = TempData["iduser"];
            ViewBag.cedula = TempData["cedula"];
            ViewBag.nombre = TempData["nombre"];
            ViewBag.direccion = TempData["direccion"];
            ViewBag.ciudad = TempData["ciudad"];
            ViewBag.correo = TempData["correo"];
            ViewBag.celular = TempData["celular"];
            ViewBag.mensaje = TempData["mensaje"];






            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();

            if (empresa.NombreEmpresa == company)
            {
                ViewBag.IdEmpresa = empresa.Id;
               
                var tiposdePagos = _context.TipoDePagos.Where(t => t.IdEmpresa == empresa.Id).ToList();
                ViewBag.tipodePago = tiposdePagos;
                
                var productos = _context.Productos.Where(p => p.IdEmpresa == empresa.Id).ToList();
                ViewBag.productos = productos;
               
                //var dbcontableContext = _context.Productos.Where(e => e.IdEmpresa == empresa.Id);
                var NumeroFactura = _context.Facturas.Where(f => f.IdEmpresa == empresa.Id).ToList();               
                double numeroFac = Convert.ToDouble(tiposdePagos.LongCount());
                ViewBag.NumeroFactura = numeroFac + 1;

                ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
                ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
                ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion");
                ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
               

                return View();
            }
            return View();



            //ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            //ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "Nombre");
            //ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion");
            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            //ViewBag.company = TempData["company"];
            //ViewBag.name = TempData["name"];
            //string company = ViewBag.company;
            //var name = ViewBag.name;
            //TempData["company"] = company;
            //TempData["name"] = name;
            //ViewBag.id = TempData["id"];
            //var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();
            //ViewBag.IdEmpresa = empresa.Id;


            //return View();
        }

      
        
        
        
        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,IdUsuario,NumeroFactura,Total,FechaCompra,IdEmpresa,IdCliente,Iva,Descuento,Observaciones,EstadoFactura,IdTipoDePago,IdDetalleCompra")] Factura factura)
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
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion", factura.IdTipoDePago);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", factura.IdUsuario);

            List<string> listaProductos = ViewBag.listaProductos;

            return View(factura.ToString(), listaProductos);
        }


        [HttpPost]
        public async Task<IActionResult> CreateFactu([FromHeader] Factura factura)
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
            ViewData["IdTipoDePago"] = new SelectList(_context.TipoDePagos, "Id", "Descripcion", factura.IdTipoDePago);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", factura.IdUsuario);

            List<string> listaProductos = ViewBag.listaProductos;

            return View(factura.ToString(), listaProductos);
        }




        //Buscar datos del cliente
        [HttpPost]
        public IActionResult BuscarCliente(string BuscarCliente)
        {
            var cliente = _context.Clientes.Where(c => c.Id == Convert.ToInt32(BuscarCliente) || c.Cedula == BuscarCliente || c.Nombre == BuscarCliente).FirstOrDefault();
            if (cliente !=null)
            {
               TempData["iduser"] = cliente.Id;
                TempData["cedula"] = cliente.Cedula;
                TempData["nombre"] = cliente.Nombre;
                TempData["direccion"] = cliente.Direccion;
                TempData["ciudad"] = cliente.IdCiudad;
                TempData["correo"] = cliente.Correo;
                TempData["celular"] = cliente.Celular;
            }
            else
            {
                TempData["mensaje"] = "Cliente no encontrado";
               
            }
            return RedirectToAction(nameof(Create));

        }
        

        //Buscar Productos
        [HttpPost]
        public IActionResult buscarProducto(string ProductoSeleccionado)
        {


            ViewBag.Nombre = ProductoSeleccionado;
            List<string> listaProductos = ViewBag.listaProductos;
            return View(listaProductos);


            //var producto = _context.Productos.Where(c => c.IdProducto == Convert.ToInt32(ProductoSeleccionado) || c.Nombre == ProductoSeleccionado);
            //if (producto !=null)
            //{

            //    return View(nameof(Create));

            //}
            //else
            //{
            //    TempData["mensaje"] = "Producto no encontrado";

            //}
            //return RedirectToAction(nameof(Create));

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
                .Include(f => f.IdUsuarioNavigation)
                .Include(f => f.IdEmpresaNavigation)
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
