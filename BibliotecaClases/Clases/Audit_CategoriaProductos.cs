using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BibliotecaClases.Clases
{
    public class Audit_CategoriaProductos
    {
        [Key]
        public int IdCategoriaProd_auditoria { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String Accion { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        public String NombreCategoria { get; set; }
    }
}
