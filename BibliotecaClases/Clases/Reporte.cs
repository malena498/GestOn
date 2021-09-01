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
        public int IdUser { get; set; }
        public string NombreUser { get; set; }
        public int CantPedidos { get; set; }
    }
}
