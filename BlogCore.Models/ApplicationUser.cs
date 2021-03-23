using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(255)]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Pais es obligatorio")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatorio")]
        public string Ciudad { get; set; }

    }
}
