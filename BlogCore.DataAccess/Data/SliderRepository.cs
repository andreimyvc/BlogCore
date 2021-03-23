using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.DataAccess.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Slider entity)
        {
            var oldEntity = _db.Slider.FirstOrDefault(p => p.Id == entity.Id);
            oldEntity.Nombre = entity.Nombre;
            oldEntity.Estado = entity.Estado;
            oldEntity.UrlImagen = entity.UrlImagen;
            oldEntity.FechaCreacion = entity.FechaCreacion;
            _db.SaveChanges();
        }
    }
}
