using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace BibliotecaClases.Clases
{
    public class Notificaciones
    {
        [Key]
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public String AccionUsuario { get; set; }
        public String TipoNotificacion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdDocumento { get; set; }

    }
}
