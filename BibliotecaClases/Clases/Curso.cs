using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        public string Grupo { get; set; }
        public string Grado { get; set; }
    }
}
