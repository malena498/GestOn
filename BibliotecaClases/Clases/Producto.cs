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
    public class Producto
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoProducto { get; set; }
        
        public string ProductoNombre{ get;set; }

        public decimal ProductoPrecioVenta { get; set; }

        public bool Activo { get; set; }

        public decimal ProductoPrecioCompra { get; set; }

        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public CategoriaProducto Categoria { get; set; }

        public int IdMarca { get; set; }
        [ForeignKey("IdMarca")]
        public Marca Marca { get; set; }
        
        public int Cantidad { get; set; }

        public string UnidadMedida { get; set; }

         public DateTime FechaCarga { get; set; }

        public override string ToString()
        {
            return ProductoNombre.ToString();
        }
    }
}
