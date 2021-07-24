using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool Activo { get; set; }
        public String Direccion { get; set; }
        public String Descripcion { get; set; }
        public virtual List<Producto> productos { get; set; }
        public int UserId { get; set; }
        public int Precio { get; set; }
        public String Estado { get; set; }
        public int idDocumento { get; set; }

    }
}
