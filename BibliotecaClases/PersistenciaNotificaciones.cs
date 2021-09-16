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
        public bool GuardarNotificacionPedido(int idUsuario,String NombreUsuario, String Accion)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Notificaciones n = new Notificaciones();
                    n.IdUsuario = idUsuario;
                    n.AccionUsuario = Accion;
                    n.TipoNotificacion = "Notificaciones Pedido";
                    n.NombreUsuario = NombreUsuario;
                    n.Fecha = DateTime.Now;
                    baseDatos.Notificaciones.Add(n);
                    baseDatos.SaveChanges();


                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GuardarNotificacionDocumento(int idUsuario, String NombreUsuario,String Accion, int idDocumento)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    
                        Notificaciones n = new Notificaciones();
                        n.IdUsuario = idUsuario;
                        n.AccionUsuario = Accion;
                        n.TipoNotificacion = "Notificaciones Documentos";
                        n.NombreUsuario = NombreUsuario;
                        n.Fecha = DateTime.Now;
                        n.IdDocumento = idDocumento;
                        baseDatos.Notificaciones.Add(n);
                        baseDatos.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public List<Notificaciones> UltimasNotificaciones()
        {
            try
            {
                List<Notificaciones> lista = null;
                using (var baseDatos = new Context())
                {
                    lista = baseDatos.Database.SqlQuery<Notificaciones>("select TOP(10) * from Notificaciones order by Fecha").ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
