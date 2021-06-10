using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotecaClases
{
    public class Sistema
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
