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
    public class UsuariosController : Controller
    {
        private readonly DbcontableContext _context;

        public UsuariosController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public  async Task<IActionResult> Index()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = idUser;
            TempData["admin"] = admin;




            var usuarios = _context.Usuarios;
           
                    return View(await usuarios.ToListAsync());
             
          
            
        }




        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = idUser;
            TempData["admin"] = admin;


            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdCiudadNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = idUser;
            TempData["admin"] = admin;


            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre");
            return View();
        }

        

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = id;
            TempData["admin"] = admin;
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", usuario.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", usuario.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", usuario.IdPais);
            return View(usuario);
        }




        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Cedula,Nombres,Apellidos,Celular,Correo,Contrasena,EsAdministrador,Activo,FechaRegistro,Direccion,IdPais,IdDepartamento,IdCiudad,NombreEmpresa")] Usuario usuario)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = id;
            TempData["admin"] = admin;


            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("Edit");
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Nombre", usuario.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", usuario.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Nombre", usuario.IdPais);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = id;
            TempData["admin"] = admin;
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdCiudadNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Include(u => u.IdPaisNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            ViewBag.id = TempData["idUser"];
            ViewBag.admin = TempData["admin"];
            var company = ViewBag.company;
            var name = ViewBag.name;
            var idUser = ViewBag.id;
            var admin = ViewBag.admin;
            TempData["company"] = company;
            TempData["name"] = name;
            TempData["idUser"] = id;
            TempData["admin"] = admin;
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DbcontableContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
