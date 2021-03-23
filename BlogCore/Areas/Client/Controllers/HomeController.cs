using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = new HomeViewModel
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                Articulos = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(data);
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            return View(data);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
