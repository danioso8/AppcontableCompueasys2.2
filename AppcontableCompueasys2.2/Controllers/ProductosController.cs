﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;
using HtmlAgilityPack;

namespace AppcontableCompueasys2._2.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DbcontableContext _context;
        HtmlWeb oWeb = new HtmlWeb();
       

        public ProductosController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Productoes
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

                var dbcontableContext = _context.Productos.Where(e => e.IdEmpresa == empresa.Id);
                return View(await dbcontableContext.ToListAsync());
            }
            return View();
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEmpresaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
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

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion");            
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");

            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen,Activo,FechaRegistro,IdEmpresa")] Producto producto)
        //{
        //    ViewBag.company = TempData["company"];
        //    ViewBag.name = TempData["name"];
        //    string company = ViewBag.company;
        //    var name = ViewBag.name;
        //    TempData["company"] = company;
        //    TempData["name"] = name;
        //    ViewBag.id = TempData["id"];
        //    if (ModelState.IsValid)
        //    {
                
        //        _context.Add(producto);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
        //    ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", producto.IdEmpresa);
        //    ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
        //    return View(producto);
        //}

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre1,Descripcion1,IdMarca1,IdCategoria1,Precio1,Stock1,RutaImagen1,Activo1,IdEmpresa1")] Producto producto)
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
                
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", producto.IdEmpresa);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "Id", producto.IdEmpresa);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen,Activo,FechaRegistro,IdEmpresa")] Producto producto)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "Id", "NombreEmpresa", producto.IdEmpresa);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.company = TempData["company"];
            ViewBag.name = TempData["name"];
            string company = ViewBag.company;
            var name = ViewBag.name;
            TempData["company"] = company;
            TempData["name"] = name;
            ViewBag.id = TempData["id"];
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdEmpresaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
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
            if (_context.Productos == null)
            {
                return Problem("Entity set 'DbcontableContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}

