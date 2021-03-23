using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre del Slider")]
        public string Nombre { get; set; }

        [Required]
        public bool Estado { get; set; } = true;

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }
    }
}
