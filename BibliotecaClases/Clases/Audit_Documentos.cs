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
    public class Audit_Documentos
    {
        [Key]
        public int IdDocumento_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int IdDocumento { get; set; }
        public String Formato { get; set; }
        public bool EsDobleFaz { get; set; }
        public bool AColor { get; set; }
        public String Descripcion { get; set; }
        public bool esEnvio { get; set; }
        public String Direccion { get; set; }
        public int gradoLiceal { get; set; }
        public bool EsPractico { get; set; }
        public int NroPractico { get; set; }
        public String NombreDocumento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
        public int UserId { get; set; }
        public String ruta { get; set; }
        public bool EsImagen { get; set; }
        public String estado { get; set; }
    }
}
