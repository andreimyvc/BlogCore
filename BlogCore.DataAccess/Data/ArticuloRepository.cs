using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.DataAccess.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Articulo entity)
        {
            var oldEntity = _db.Articulo.FirstOrDefault(p => p.Id == entity.Id);
            oldEntity.Nombre = entity.Nombre;
            oldEntity.UrlImagen = entity.UrlImagen;
            oldEntity.Descripcion = entity.Descripcion;
            oldEntity.CategoriaId = entity.CategoriaId;
        }
    }
}
