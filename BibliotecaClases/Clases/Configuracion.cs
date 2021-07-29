using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Configuracion
    {
        [Key]
        public int IdConfiguracion { get; set; }
        public String Nombre { get; set; }
        public String Valor { get; set; }
    }
}
