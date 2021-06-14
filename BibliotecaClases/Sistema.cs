using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace BibliotecaClases
{
    public partial class Sistema
    {
        private static Sistema sistema;
        public static Sistema GetInstancia()
        {

            if (sistema == null)
            {
                sistema = new Sistema();

            }

            return sistema;
        }

     

      
    }
}
