using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    public class PedidoDocumento
    {
        public int idDocumento { get; set; }
        [ForeignKey("idDocumento")]
        public Documento documento { get; set; }
        public int idPedido { get; set; }
    }
}
