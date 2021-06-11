using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Usuario usuario { get; set; }

    }
}
