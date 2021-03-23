using BlogCore.Models;
using BlogCore.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.DataAccess.Data.Initializer
{
    public class InitializerDB : IInitializerDB
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public InitializerDB(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

            if (_db.Roles.Any(p => p.Name == Constantes.ADMIN)) return;

            _roleManager.CreateAsync(new IdentityRole(Constantes.ADMIN)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constantes.USUARIO)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@blogcore.com",
                Email = "admin@blogcore.com",
                EmailConfirmed = true,
                Nombre = "Admin"
            }, "Admin.123").GetAwaiter().GetResult();

            ApplicationUser usuario = _db.ApplicationUser.Where(p => p.Email == "admin@blogcore.com")
                .FirstOrDefault();

            _userManager.AddToRoleAsync(usuario, Constantes.ADMIN).GetAwaiter().GetResult();
        }
    }
}
