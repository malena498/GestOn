using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_ImagenPedido
    {
        [Key]
        public int IdOfertaImagen_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int ImagenPedidoId { get; set; }
        [Required]
        public String ImagenPedidoURL { get; set; }
        public int IdPedido { get; set; }
        public Pedido pedido { get; set; }
        public bool Activo { get; set; }
    }
}
