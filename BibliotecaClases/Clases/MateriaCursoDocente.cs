using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
namespace BibliotecaClases.Clases
{
    public class MateriaCursoDocente
    {
       
        public int idDocente { get; set; }
        public int IdCurso { get; set; }
        public String materia { get; set; }
    }
}
