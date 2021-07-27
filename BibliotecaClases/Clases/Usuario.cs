using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Usuario
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public String UserNombre { get; set; }
        [Required ]
        public String UserEmail { get; set; }
        [Required]
        public String UserCedula { get; set; }
        public String UserTelefono { get; set; }
        public int IdNivel { get; set; }
        [ForeignKey("IdNivel")]
        public Nivel nivel { get; set; }
        public bool Activo { get; set; }
        [Required]
        public String UserContrasenia { get; set; }
    }
}
