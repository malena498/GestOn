using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_Configuraciones
    {
        [Key]
        public int IdConfiguraciones_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int IdConfiguracion { get; set; }
        public String Nombre { get; set; }
        public String Valor { get; set; }
        public bool Activo { get; set; }
    }
}
