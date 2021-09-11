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
    public class Docente: Usuario 
    {
        [Key]
        public int idDocebte { get; set; }
        //public virtual ICollection<Curso> cursos { get; set; }
        public String materia { get; set; }
    }
}
