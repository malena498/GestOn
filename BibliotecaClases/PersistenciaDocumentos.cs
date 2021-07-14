using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarDocumento(Documento documento)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    documento.FechaIngreso = DateTime.Today;
                    documento.Activo = true;
                    baseDatos.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
