using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { get; set; }
        public String NombrePermiso { get; set; }
        public String URL { get; set; }
        public bool esVisible { get; set; }
    }
}
