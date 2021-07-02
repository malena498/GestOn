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
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        
        public string ProductoNombre{ get;set; }

        public decimal ProductoPrecioVenta { get; set; }

        public bool Activo { get; set; }

        public decimal ProductoPrecioCompra { get; set; }

        public int IdCategoria { get; set; }

        public string ProductoMarca { get; set; }

        public int Cantidad { get; set; }

        public override string ToString()
        {
            return ProductoNombre.ToString();
        }
    }
}
