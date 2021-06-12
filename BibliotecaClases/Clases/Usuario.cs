using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Usuario
    {
        [Key]
        public int UserId { get; set; }
        public String UserNombre { get; set; }
        public String UserEmail { get; set; }
        public String UserCedula { get; set; }
        public String UserTelefono { get; set; }
        public int IdNivel { get; set; }

        public bool Activo { get; set; }
        public String UserContraseña { get; set; }
    }
}
