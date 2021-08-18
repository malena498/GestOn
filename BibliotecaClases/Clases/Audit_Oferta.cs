using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_Oferta
    {
        [Key]
        public int IdOferta_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int IdOferta { get; set; }
        [Required]
        public String OfertaTitulo { get; set; }
        public bool Activo { get; set; }
        public DateTime OfertaFechaDesde { get; set; }
        public DateTime OfertaFechaHasta { get; set; }
        public int UserId { get; set; }
        public Usuario usuario { get; set; }
        public virtual List<Imagen> Imagenes { get; set; }
        public String OfertaDescripcion { get; set; }
        [Required]
        public decimal OfertaPrecio { get; set; }
    }
}
