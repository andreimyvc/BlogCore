using BlogCore.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsersController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            var claims = (ClaimsIdentity) this.User.Identity;
            var usuario = claims.FindFirst(ClaimTypes.NameIdentifier);
            var listaUsuario = _contenedorTrabajo.Usuario.GetAll(p => p.Id != usuario.Value);

            return View(listaUsuario);
        }
        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario (id);
            return RedirectToAction(nameof(Index));
        }
    }
}
