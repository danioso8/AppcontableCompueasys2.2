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
    public class EmpleadosController : Controller
    {
        private readonly DbcontableContext _context;

        public EmpleadosController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Empleadoes
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


                var dbcontableContext = _context.Empleados.Include(e => e.IdCiudadNavigation).Include(e => e.IdDepartamentoNavigation).Include(e => e.IdEmpresaNavigation).Include(e => e.IdPaisNavigation).Where(e => e.IdEmpresa == empresa.Id);
                return View(await dbcontableContext.ToListAsync());
            }
            return View();
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];

            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdEmpresaNavigation)
                .Include(e => e.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,Direccion,Telefono,IdPais,IdDepartamento,IdCiudad,Email,Activo,IdEmpresa, FechaRegistro")] Empleado empleado)
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
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empleado.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", empleado.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empleado.IdPais);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];

            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empleado.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", empleado.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empleado.IdPais);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,Direccion,Telefono,IdPais,IdDepartamento,IdCiudad,Email,Activo,IdEmpresa")] Empleado empleado)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];

            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empleado.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.IdDepartamento);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", empleado.IdEmpresa);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empleado.IdPais);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];

            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdEmpresaNavigation)
                .Include(e => e.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
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

            if (_context.Empleados == null)
            {
                return Problem("Entity set 'DbcontableContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
