using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;
using Microsoft.AspNetCore.Authorization;
using AppcontableCompueasys2._2.Models;
using Microsoft.VisualBasic;

namespace AppcontableCompueasys2._2.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
       datosLayout _companny = new datosLayout();
        private readonly DbcontableContext _context;
       
        public ClientesController(DbcontableContext context)
        {
            _context = context;
           
        }

        // GET: Clientes
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
            

          
                if (empresa.NombreEmpresa == company)
                {
                    var dbcontableContext = _context.Clientes.Include(c => c.IdCiudadNavigation).Include(c => c.IdDepartamentoNavigation).Include(c => c.IdEmpresaNavigation).Include(c => c.IdPaisNavigation).Where(c => c.IdEmpresa == empresa.Id);
                    return View(await dbcontableContext.ToListAsync());
            }
            else
            {
                return View();
            }
                
          
        }
        [HttpPost]
        public IActionResult Get(string BuscarCliente)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();
            var cliente = _context.Clientes.Where(c => c.Id == Convert.ToInt32(BuscarCliente) || c.Nombre == BuscarCliente || c.Cedula == BuscarCliente).Include(c => c.IdCiudadNavigation).Include(c => c.IdDepartamentoNavigation).Include(c => c.IdEmpresaNavigation).Include(c => c.IdPaisNavigation).FirstOrDefault(c => c.IdEmpresa == empresa.Id);
            if (cliente != null)
            {
                TempData["iduser"] = cliente.Id;
                TempData["cedula"] = cliente.Cedula;
                TempData["nombre"] = cliente.Nombre;
                TempData["direccion"] = cliente.Direccion;
                TempData["ciudad"] = cliente.IdCiudad;
                TempData["correo"] = cliente.Correo;
                TempData["celular"] = cliente.Celular;

               

                return RedirectToAction("Create", "Facturas");

            }
            else
            {
                TempData["mensaje"] = "Cliente no se encontro";
                return RedirectToAction("Create", "Facturas");
            }


            //var clientes = _context.Clientes.Where(c => c.Id == Convert.ToInt16(BuscarCliente) || c.Nombre == BuscarCliente || c.Celular == BuscarCliente && c.IdEmpresa == empresa.Id).FirstOrDefault();
            //return StatusCode(StatusCodes.Status200OK, cliente );  

            //return Json(cliente);
        }

       


        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];


            var empresa = _context.Empresas.Where(e => e.NombreEmpresa == company).FirstOrDefault();


            ViewBag.IdEmpresa = empresa.Id;
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Correo,Celular,Cedula,Fecha,IdEmpresa,IdPais,IdDepartamento,IdCiudad")] Cliente cliente)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;

            var usuarioBuscado = _context.Clientes.Where(e => e.Cedula == cliente.Cedula).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (usuarioBuscado != null)
                {
                    TempData["cliente"] = "Cliente ya existe";
                }
                else
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
                
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", cliente.IdPais);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", cliente.IdPais);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo,Direccion,Celular,Cedula,Fecha,IdEmpresa,IdPais,IdDepartamento,IdCiudad")] Cliente cliente)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", cliente.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", cliente.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", cliente.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", cliente.IdPais);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
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
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
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
