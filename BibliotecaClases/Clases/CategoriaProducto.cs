﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
namespace BibliotecaClases.Clases
{
    public class CategoriaProducto
    {
        [Key]
        public int IdCategoria { get; set; }

        public bool Activo { get; set; }

        public String NombreCategoria { get; set; }

        public int porcentaje { get; set; }
    }
}
