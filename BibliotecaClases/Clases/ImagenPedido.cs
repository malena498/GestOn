using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class ImagenPedido
    {
        [Key]
        public int ImagenPedidoId { get; set; }
        [Required]
        public String ImagenPedidoURL { get; set; }
        public int IdPedido { get; set; }
    }
}
