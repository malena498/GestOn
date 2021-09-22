using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
using System.Drawing; 
using System.IO;
using System.Web.UI.HtmlControls;

namespace GestOn2.PaginasMaestras
{
    public partial class PageMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarMenues();
            if (Session["IdUsuario"] != null)
            {
                String id = Session["IdUsuario"].ToString();

                MostrarNotificaciones(int.Parse(id));
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        public void CargarMenues()
        {
            if (Session["IdUsuario"] != null)
            {
                String id = Session["IdUsuario"].ToString();
                if (!id.Equals("0") || String.IsNullOrEmpty(id))
                {
                    lnkLogin.Visible = false;
                    Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(id));
                    if (u.nivel.NombreNivel.Equals("Docente"))
                    {
                        Docente.Visible = true;
                        Admin.Visible = false;
                        hlConfiguraciones.Visible = false;
                        hlAdministrarCuenta.Visible = true;
                    }
                    if (u.nivel.NombreNivel.Equals("Personal"))
                    {
                        Docente.Visible = false;
                        Admin.Visible = true;
                        hlConfiguraciones.Visible = false;
                        hlAdministrarCuenta.Visible = true;
                    }
                    else if (u.nivel.NombreNivel.Equals("Estudiante"))
                    {
                        Admin.Visible = false;
                        formDoc.Visible = false;
                        Docente.Visible = true;
                        hlConfiguraciones.Visible = false;
                        hlAdministrarCuenta.Visible = true;
                    }
                    else if (u.nivel.UserEstandar)
                    {
                        Admin.Visible = false;
                        formDoc.Visible = false;
                        Docente.Visible = true;
                        hlConfiguraciones.Visible = false;
                        hlAdministrarCuenta.Visible = true;

                    }
                    else if (u.nivel.UserAdmin)
                    {
                        Docente.Visible = false;
                        Admin.Visible = true;
                        hlAdministrarCuenta.Visible = false;
                        hlConfiguraciones.Visible = true;


                    }
                }
            }
            else
            {
                Docente.Visible = false;
                Admin.Visible = false;
                hlConfiguraciones.Visible = false;
                hlAdministrarCuenta.Visible = false;
                formDoc.Visible = false;
                liConfig.Visible = false;
                linotificacion.Visible = false;

            }

        }

        protected void Salir_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void MostrarNotificaciones(int id)
        {
            Usuario u = Sistema.GetInstancia().BuscarUsuario(id);
            if (u.nivel.UserAdmin)
            {
                List<Notificaciones> notificaciones = Sistema.GetInstancia().UltimasNotificaciones();
                String texto = "";
                List<String> lista = new List<string>();
                foreach (Notificaciones n in notificaciones)
                {

                    if (n.AccionUsuario.Equals("NUEVO"))
                    {
                        if (n.TipoNotificacion.Equals("Notificaciones Pedido"))
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha ingresado un nuevo pedido";
                        }
                        else
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha ingresado un nuevo documento";

                        }
                    }
                    if (n.AccionUsuario.Equals("MODIFICACION"))
                    {
                        if (n.TipoNotificacion.Equals("Notificaciones Pedido"))
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha modificado un  pedido";

                        }
                        else
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha modificado un documento";

                        }

                    }
                    if (n.AccionUsuario.Equals("CANCELACION"))
                    {
                        if (n.TipoNotificacion.Equals("Notificaciones Pedido"))
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha cancelado un pedido";

                        }
                        else
                        {
                            texto = "El usuario " + n.NombreUsuario + " ha eliminado un documento";
                        }
                    }
                    lista.Add(texto);

                }
                foreach (string notif in lista)
                {
                    HtmlGenericControl label = new HtmlGenericControl("label");
                    label.InnerText = notif;
                    Div3.Controls.Add(label);
                }
                /*CARGAR ULTIMAS 10 N0TIFICACIONES SEGUN LA FECHA 
                 * MOSTRAR 
                 * CADA VEZ Q HAY NUEVAS*/
                linotificacion.Visible = true;
            }
            if (u.nivel.NombreNivel.Equals("Estudiante"))
            {
                List<Notificaciones> notificaciones = Sistema.GetInstancia().UltimasNotificaciones();
                CursoEstudiante c = Sistema.GetInstancia().BuscarCursoEstudiante(u);
                String texto = "";
                List<String> lista = new List<string>();
                foreach (Notificaciones n in notificaciones)
                {
                    if (n.TipoNotificacion.Equals("Notificaciones Documentos"))
                    {
                        Documento d = Sistema.GetInstancia().BuscarDocumento(n.IdDocumento);
                        if (c.IdCurso == d.gradoLiceal)
                        {
                            List<MateriaCursoDocente> m = Sistema.GetInstancia().ListadoMateriaCursoDocentexGrado(d.gradoLiceal);
                            foreach (MateriaCursoDocente mc in m)
                            {
                                if (n.AccionUsuario.Equals("NUEVO"))
                                {
                                    if (d.EsPractico)
                                    {
                                        texto = "El docente " + n.NombreUsuario + " ha ingresado el practico N°: " + d.NroPractico + " de la materia " + mc.materia + " para su impresión";
                                    }
                                    else
                                    {
                                        texto = "El docente " + n.NombreUsuario + " ha ingresado el documento" + d.NombreDocumento + " de la materia " + mc.materia + " para su impresión";

                                    }
                                }
                                if (n.AccionUsuario.Equals("CANCELACION"))
                                {
                                    if (d.EsPractico)
                                    {
                                        texto = "El practico N°: " + d.NroPractico + " +de la materia " + mc.materia + " ya no esta disponible para impresión";
                                    }
                                    else
                                    {
                                        texto = "El documento" + d.NombreDocumento + " +de la materia " + mc.materia + " ya no esta disponible para impresión";

                                    }
                                }
                                lista.Add(texto);
                            }
                        }
                    }
                }
                foreach (string notif in lista)
                {
                    HtmlGenericControl label = new HtmlGenericControl("label");
                    label.InnerText = notif;
                    Div3.Controls.Add(label);
                }
                /*CARGAR ULTIMAS 10 N0TIFICACIONES SEGUN LA FECHA 
                 * MOSTRAR 
                 * CADA VEZ Q HAY NUEVAS*/
                linotificacion.Visible = true;
            }
        }
    }
}
