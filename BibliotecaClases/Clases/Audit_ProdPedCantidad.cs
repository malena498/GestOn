using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_ProdPedCantidad
    {
        [Key]
        public int IdCategoriaProd_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public bool Activo { get; set; }
        public int IdPedido { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public Producto producto { get; set; }
    }
}
