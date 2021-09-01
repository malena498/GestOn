using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public List<Reporte> ReportBestClients(DateTime FechaDesde,DateTime FechaHasta)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    List<Reporte> report = null;
                    try
                    {
                        report = baseDatos.Database.SqlQuery<Reporte>("EXEC SP_USUARIOS_DESTACADOS @fchini,@fchfin",
                                                                    new SqlParameter("fchini", FechaDesde),
                                                                    new SqlParameter("fchfin", FechaHasta))
                                                                    .ToList();

                        report = baseDatos.Database.SqlQuery<Reporte>("select IdUser,NombreUser,CantPedidos from reporte").ToList();
                        return report;
                    }
                    catch(Exception ex)
                    {
                        return report;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
