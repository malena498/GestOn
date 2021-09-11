using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Estudiante : Usuario
    {
        [Key]
        public int idEstudiante { get; set; }
        //[ForeignKey("Id")]
        //public int Id { get; set; }
        //public Curso curso { get; set; }
   }
}
