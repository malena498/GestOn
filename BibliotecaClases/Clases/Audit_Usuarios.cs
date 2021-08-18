using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_Usuarios
    {
        [Key]
        public int IdOfertaImagen_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int UserId { get; set; }
        [Required]
        public String UserNombre { get; set; }
        [Required]
        public String UserEmail { get; set; }
        [Required]
        public String UserCedula { get; set; }
        public int NroCarpeta { get; set; }
        public String UserTelefono { get; set; }
        public int IdNivel { get; set; }
        public Nivel nivel { get; set; }
        public bool Activo { get; set; }
        [Required]
        public String UserContrasenia { get; set; }
    }
}
