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
    public class ProductoPedidoCantidad
    {
        public int IdPedido { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        [ForeignKey("ProductoId")]
        public Producto producto{ get; set; }
    }
}
