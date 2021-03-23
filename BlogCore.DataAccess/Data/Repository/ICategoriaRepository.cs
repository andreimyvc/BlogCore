using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.DataAccess.Data.Repository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();

        void Update(Categoria entity);
    }
}
