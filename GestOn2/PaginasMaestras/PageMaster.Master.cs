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

namespace GestOn2.PaginasMaestras
{
    public partial class PageMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             CargarMenues();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        public void CargarMenues()
         {
            if(Session["IdUsuario"] != null)
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
                    }
                    else if (u.nivel.UserEstandar) {
                        Admin.Visible = false;
                        formDoc.Visible = false;
                        Docente.Visible = true;
                    }
                    else if (u.nivel.UserAdmin)
                    {
                        Docente.Visible = false;
                        Admin.Visible = true;
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


    }
}