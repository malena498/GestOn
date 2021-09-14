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
                if (id.Equals("0") || String.IsNullOrEmpty(id))
                {
                    Session["IdUsuario"] = "";
                    Server.Transfer("~/Login.aspx");
                }
                else
                {
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
                String texto = "";
                List<String> lista = new List<string>();
                foreach (Notificaciones n in notificaciones)
                {
                    if (n.AccionUsuario.Equals("NUEVO"))
                    {
                        if (n.TipoNotificacion.Equals("Notificaciones Documentos"))
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
                        if (n.TipoNotificacion.Equals("Notificaciones Documentos"))
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
                        if (n.TipoNotificacion.Equals("Notificaciones Documentos"))
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
            }
        }
    }
}/*                     
    tabs
    HtmlGenericControl anchor = new HtmlGenericControl("a");
    anchor.Attributes.Add("href", "#");
    anchor.InnerText = Convert.ToString(drOutput["ModuleGroup"]);
    li.Controls.Add(anchor);*/
