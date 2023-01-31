
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

namespace CompueasysContable_2._2.Controllers
{
    public class HomeController : Controller
    {
        public DbcontableContext _context;
        private readonly string ?CadenaSQL;

        public HomeController(IConfiguration config, DbcontableContext context)
        {

            _context = context;
            CadenaSQL = config.GetConnectionString("CadenaSql");
             

        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {

            var usuario = _context.Usuarios.Where(user => user.Correo == correo && user.Contrasena == contraseña).FirstOrDefault();
            if (usuario != null)
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, usuario.Nombres!),
                    new Claim("corre", usuario.Correo!),
                    //new Claim(ClaimTypes.Role, usuario.Rol!)
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewData["Mensage"] = "Usuario no encontrado  ";
                return View();
            }


        }

              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}