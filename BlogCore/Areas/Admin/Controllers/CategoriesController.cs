using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriesController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(model);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _contenedorTrabajo.Categoria.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(model);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        #region Llamadas a la API

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _contenedorTrabajo.Categoria.GetAll();
            return Json( new { data = lista });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var model = _contenedorTrabajo.Categoria.Get(id);
            if (model == null)
            {
                return Json( new { success = false, message = "Error borrando categoría" });
            }
            _contenedorTrabajo.Categoria.Remove(model);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoría borrada correctamente" });
        }

        #endregion
    }
}
