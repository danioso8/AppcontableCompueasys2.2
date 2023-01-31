﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppcontableCompueasys2._2.Models.Data;

namespace AppcontableCompueasys2._2.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly DbcontableContext _context;

        public EmpresasController(DbcontableContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            var dbcontableContext = _context.Empresas.Include(e => e.IdCiudadNavigation).Include(e => e.IdDepartamentoNavigation).Include(e => e.IdPaisNavigation).Include(e => e.IdPropietarioEmpresaNavigation);
            return View(await dbcontableContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdPaisNavigation)
                .Include(e => e.IdPropietarioEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id");
            ViewData["IdPropietarioEmpresa"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreEmpresa,DireecionEm,NitORut,TelefonoOCelular,Email,IdPais,IdDepartamento,IdCiudad,IdPropietarioEmpresa")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empresa.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empresa.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empresa.IdPais);
            ViewData["IdPropietarioEmpresa"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", empresa.IdPropietarioEmpresa);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empresa.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empresa.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empresa.IdPais);
            ViewData["IdPropietarioEmpresa"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", empresa.IdPropietarioEmpresa);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreEmpresa,DireecionEm,NitORut,TelefonoOCelular,Email,IdPais,IdDepartamento,IdCiudad,IdPropietarioEmpresa")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            ViewData["IdCiudad"] = new SelectList(_context.Ciudads, "Id", "Id", empresa.IdCiudad);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", empresa.IdDepartamento);
            ViewData["IdPais"] = new SelectList(_context.Pais, "Id", "Id", empresa.IdPais);
            ViewData["IdPropietarioEmpresa"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", empresa.IdPropietarioEmpresa);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.IdCiudadNavigation)
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdPaisNavigation)
                .Include(e => e.IdPropietarioEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresas == null)
            {
                return Problem("Entity set 'DbcontableContext.Empresas'  is null.");
            }
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
          return (_context.Empresas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
