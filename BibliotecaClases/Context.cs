using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BibliotecaClases
{
    class Context: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
