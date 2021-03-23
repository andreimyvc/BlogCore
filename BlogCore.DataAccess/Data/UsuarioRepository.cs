using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.DataAccess.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuario = _db.ApplicationUser.FirstOrDefault(p => p.Id == IdUsuario);
            usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuario = _db.ApplicationUser.FirstOrDefault(p => p.Id == IdUsuario);
            usuario.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
