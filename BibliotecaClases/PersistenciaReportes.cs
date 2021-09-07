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
        public List<Reporte> ReportCliProducto(DateTime FechaDesde,DateTime FechaHasta)
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

                        report = baseDatos.Database.SqlQuery<Reporte>("SELECT USERID,USERNOMBRE,CANTIDAD " +
                                                                      "FROM REPORTE " +
                                                                      "GROUP BY USERID,USERNOMBRE,CANTIDAD " +
                                                                      "ORDER BY CANTIDAD DESC")
                                                                      .ToList();
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

        public List<Reporte> ReportCliDocumentos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    List<Reporte> report = null;
                    try
                    {
                        report = baseDatos.Database.SqlQuery<Reporte>("EXEC SP_CLIENTE_MAX_DOC @fchini,@fchfin",
                                                                    new SqlParameter("fchini", FechaDesde),
                                                                    new SqlParameter("fchfin", FechaHasta))
                                                                    .ToList();

                        report = baseDatos.Database.SqlQuery<Reporte>("SELECT USERID,USERNOMBRE,CANTIDAD " +
                                                                      "FROM REPORTE " +
                                                                      "GROUP BY USERID,USERNOMBRE,CANTIDAD " +
                                                                      "ORDER BY CANTIDAD DESC")
                                                                      .ToList();
                        return report;
                    }
                    catch (Exception ex)
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

        public List<Reporte> ReportCliGastos(DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    List<Reporte> report = null;
                    try
                    {
                        report = baseDatos.Database.SqlQuery<Reporte>("EXEC SP_CLIENTE_GASTOS @fchini,@fchfin",
                                                                    new SqlParameter("fchini", FechaDesde),
                                                                    new SqlParameter("fchfin", FechaHasta))
                                                                    .ToList();

                        report = baseDatos.Database.SqlQuery<Reporte>("SELECT USERID,USERNOMBRE,CANTIDAD " +
                                                                      "FROM REPORTE " +
                                                                      "GROUP BY USERID,USERNOMBRE,CANTIDAD " +
                                                                      "ORDER BY CANTIDAD DESC")
                                                                      .ToList();
                        return report;
                    }
                    catch (Exception ex)
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
