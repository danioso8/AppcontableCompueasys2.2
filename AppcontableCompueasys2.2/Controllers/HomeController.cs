
using AppcontableCompueasys2._2.Models;
using AppcontableCompueasys2._2.Models.Data;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Net.Http;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.Data.Entity;
using Microsoft.AspNetCore.Http;

namespace CompueasysContable_2._2.Controllers
{
    public class HomeController : Controller
    {
        public DbcontableContext _context;

       datosLayout _companny = new datosLayout();
      

        public HomeController(DbcontableContext context)
        {

            _context = context;
           
            



        }

        public IActionResult Home()
        {
            return View();
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            TempData["nameEmpresa"] = Request.Cookies["claimsIdetity"];
            TempData["name"] = Request.Cookies["ClaimsPrincipal"];



            return View();
        }

        public IActionResult Login()
        {
            return View();
        } 
        
        public IActionResult Registro()
        {
            return View();
        }


        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("IdUsuario,Nombres,Apellidos,Correo,Contrasena,EsAdministrador,Activo,FechaRegistro,Direccion,IdPais,IdDepartamento,IdCiudad,NombreEmpresa")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Home");
            }
            return Error();

           
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {

            var usuario = _context.Usuarios.Where(user => user.Correo == correo && user.Contrasena == contraseña).FirstOrDefault();
            if (usuario != null)
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, usuario.Nombres!),
                    new Claim("correo", usuario.Correo!),
                    new Claim("Empresa", usuario.NombreEmpresa!),
                   
                };
                
                TempData["name"] = usuario.Nombres;
                TempData["company"] = usuario.NombreEmpresa;

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                
                //Cookies para mostrar datos en la vista principla

                
                

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                ViewBag.User = "Usuario no encontrado  ";
                return View();
            }


        }
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}