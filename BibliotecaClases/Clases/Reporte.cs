using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Reporte
    {
        public int USERID { get; set; }
        public string USERNOMBRE { get; set; }
        public int CANTIDAD { get; set; }
    }
}
