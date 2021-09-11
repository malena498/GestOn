using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public List<ReporteProductosMasVendidos> ReporteProductosMasVendidos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                List<ReporteProductosMasVendidos> lista = null;
                using (var baseDatos = new Context())
                {




                    baseDatos.Database.ExecuteSqlCommand("EXEC SP_PRODUCTOS_MAS_VENDIDOS @fchini,@fchfin",
                                                                   new SqlParameter("fchini", FechaDesde),
                                                                   new SqlParameter("fchfin", FechaHasta));

                    lista = baseDatos.Database.SqlQuery<ReporteProductosMasVendidos>("SELECT PRODUCTOID,PRODUCTONOMBRE,CANTIDAD FROM ReporteProductosMasVendidos GROUP BY PRODUCTOID,PRODUCTONOMBRE,CANTIDAD ORDER BY Cantidad ASC ").ToList();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
