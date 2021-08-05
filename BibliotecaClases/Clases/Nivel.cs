using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Nivel
    {
        [Key]
        public int IdNivel { get; set; }
        public bool Activo { get; set; }
        public String NombreNivel { get; set; }
        public bool UserAdmin { get; set; }
        public bool UserEstandar { get; set; }
    }
}
