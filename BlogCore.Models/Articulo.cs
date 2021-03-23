using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del artículo")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La descripción es obligatorio")]
        [Display(Name = "Descripción del artículo")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria Categoria { get; set; }
    }
}
