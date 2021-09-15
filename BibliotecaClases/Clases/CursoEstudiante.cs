using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class CursoEstudiante 
    {
       
        public int idEstudiante { get; set; }

        public int IdCurso { get; set; }
    }
      
}
