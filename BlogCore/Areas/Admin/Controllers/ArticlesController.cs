﻿using BlogCore.DataAccess.Data.Repository;
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
    public class ArticlesController : Controller
    {
        public readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticlesController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = new ArticuloViewModel
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var data = new ArticuloViewModel
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(ArticuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (model.Articulo.Id == 0)
                {
                    //Nuevo Artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var sUrl = Path.Combine(@"imagenes\articulos", nombreArchivo + extension);

                    using (var fileStreams = new FileStream(Path.Combine(rutaPrincipal, sUrl), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    model.Articulo.UrlImagen = sUrl;
                    model.Articulo.FechaCreacion = DateTime.Now;
                    _contenedorTrabajo.Articulo.Add(model.Articulo);
                }
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            model.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var model = new ArticuloViewModel
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if (id != null)
            {
                model.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var oldModel = _contenedorTrabajo.Articulo.Get(model.Articulo.Id);

                if (archivos.Count > 0)
                {
                    //Nuevo Artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var sUrl = Path.Combine(@"imagenes\articulos", nombreArchivo + extension);

                    using (var fileStreams = new FileStream(Path.Combine(rutaPrincipal, sUrl), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    model.Articulo.UrlImagen = sUrl;
                }
                else
                {
                    model.Articulo.UrlImagen = oldModel.UrlImagen;
                }

                model.Articulo.FechaCreacion = DateTime.Now;

                _contenedorTrabajo.Articulo.Update(model.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oldModel = _contenedorTrabajo.Articulo.Get(id);
            if (oldModel == null)
            {
                return Json(new { success = false, message = "Error borrando artículo" });
            }
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            string rutaImagen = Path.Combine(rutaPrincipal, oldModel.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            _contenedorTrabajo.Articulo.Remove(oldModel);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Artículo borrado correctamente" });
        }

        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json( new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        #endregion
    }
}
