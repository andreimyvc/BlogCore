using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.DataAccess.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categorias.Select( p => new SelectListItem
            {
                Text = p.Nombre,
                Value = p.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var oldEntity = _db.Categorias.FirstOrDefault(p => p.Id == categoria.Id);
            oldEntity.Nombre = categoria.Nombre;
            oldEntity.Orden = categoria.Orden;

            _db.SaveChanges();
        }
    }
}
