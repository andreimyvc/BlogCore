using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre para la categoría")]
        [Display(Name = "Nombre de la categoría")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un orden válido")]
        [Display(Name = "Orden de visualización")]
        public int Orden { get; set; }
    }
}
