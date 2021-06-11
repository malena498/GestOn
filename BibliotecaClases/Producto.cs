using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO; 

namespace BibliotecaClases
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        public String ProductoNombre{ get;set; }

        public int ProductoPrecioVenta { get; set; }

        public bool Activo { get; set; }

        public float ProductoPrecioCompra { get; set; }

        public String ProductoCategoría { get; set; }

        public String ProductoMarca { get; set; }
    }
}
