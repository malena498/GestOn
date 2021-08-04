using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Imagen
    {
        [Key]
        public int ImagenId { get; set; }
        [Required]
        public String ImagenURL { get; set; }
        public int IdOferta { get; set; }
        public bool Activo { get; set; }
    }
}
