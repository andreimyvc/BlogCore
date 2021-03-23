using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        public readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(Slider model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (model.Id == 0)
                {
                    //Nuevo Artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var sUrl = Path.Combine(@"imagenes\sliders", nombreArchivo + extension);

                    using (var fileStreams = new FileStream(Path.Combine(rutaPrincipal, sUrl), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    model.UrlImagen = sUrl;
                    model.FechaCreacion = DateTime.Now;
                    _contenedorTrabajo.Slider.Add(model);
                }
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var model = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var oldModel = _contenedorTrabajo.Slider.Get(model.Id);

                if (archivos.Count > 0)
                {
                    //Nuevo Artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var sUrl = Path.Combine(@"imagenes\sliders", nombreArchivo + extension);

                    using (var fileStreams = new FileStream(Path.Combine(rutaPrincipal, sUrl), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    model.UrlImagen = sUrl;
                }
                else
                {
                    model.UrlImagen = oldModel.UrlImagen;
                }

                model.FechaCreacion = DateTime.Now;

                _contenedorTrabajo.Slider.Update(model);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oldModel = _contenedorTrabajo.Slider.Get(id);
            if (oldModel == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            string rutaImagen = Path.Combine(rutaPrincipal, oldModel.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            _contenedorTrabajo.Slider.Remove(oldModel);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrado correctamente" });
        }

        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json( new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}
