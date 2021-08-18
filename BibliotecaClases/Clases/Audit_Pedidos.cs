using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_Pedidos
    {
        [Key]
        public int IdPedido_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int IdPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool Activo { get; set; }
        public String Direccion { get; set; }
        public String Descripcion { get; set; }
        public int UserId { get; set; }
        public Usuario usuario { get; set; }
        public decimal Precio { get; set; }
        public String Estado { get; set; }
        public String HoraEntrega { get; set; }
    }
}
