using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using BibliotecaClases.Clases;


namespace BibliotecaClases.Clases
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }

        public string NombreMarca { get; set;}
    }
}
