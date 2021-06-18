using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GestOn2
{
    public partial class PageMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///if (Session["IdUsuario"] == null)
        //            {
        //                Response.Redirect("~/Logon.aspx");
        //            }
        //            else
        //            {
        //                Usuario usu = Sistema.GetInstancia().BuscarUsuarioId(Int32.Parse(Session["IdUsuario"].ToString()), Session["NombreBase"].ToString());
        //                if (usu != null)
        //                {
        //                    if (usu.Rol != null)
        //                    {
        //                        CargarMenus(usu.Rol);
        //}

                //spanUsuario.InnerText = usu.Nombre.ToUpper();
                //                }
                //                spanEmpresa.InnerText = Sistema.GetInstancia().ObtenerConfiguracion("NombreEmpresaMenu", Session["NombreBase"].ToString());
                //            
        }
        //protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login.aspx");
        //}
        //        }
        //protected void CargarMenus(RolUsuario rol)
        //{
        //    List<CategoriaPermiso> menues;
        //    if (rol.EsAdmin)
        //    {
        //        //GENERO TODOS LOS PERMISOS
        //        menues = Sistema.GetInstancia().ObtenerMenues(Session["NombreBase"].ToString());
        //    }
        //    else
        //    {
        //        menues = Sistema.GetInstancia().ObtenerMenues(rol.IdRol, Session["NombreBase"].ToString());
        //    }
        //    foreach (CategoriaPermiso menu in menues)
        //    {
        //        System.Web.UI.HtmlControls.HtmlGenericControl li = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
        //        li.Attributes["class"] = "nav-item dropdown";
        //        HyperLink hy = new HyperLink();
        //        hy.Attributes["class"] = "nav-link dropdown-toggle pt-3";
        //        hy.Attributes["href"] = "#";
        //        hy.Attributes["data-toggle"] = "dropdown";
        //        hy.Text = menu.Nombre.ToUpper();
        //        System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
        //        div.Attributes["class"] = "dropdown-menu";
        //        List<Permiso> permisos;
        //        if (rol.EsAdmin)
        //        {
        //            //GENERO TODOS LOS PERMISOS
        //            permisos = Sistema.GetInstancia().ObtenerSubMenues(menu.IdCategoriaPermiso, Session["NombreBase"].ToString());
        //        }
        //        else
        //        {
        //            permisos = Sistema.GetInstancia().ObtenerSubMenuesRol(rol.IdRol, menu.IdCategoriaPermiso, Session["NombreBase"].ToString());
        //        }
        //        foreach (Permiso permiso in permisos)
        //        {
        //            HyperLink hl = new HyperLink();
        //            hl.ID = permiso.NombreClave;
        //            hl.Text = permiso.Nombre;
        //            hl.NavigateUrl = permiso.URL;
        //            hl.CssClass = "dropdown-item";
        //            div.Controls.Add(hl);
        //        }
        //        li.Controls.Add(hy);
        //        li.Controls.Add(div);
        //        NavBarMenu.Controls.Add(li);
        //    }
        //}

    }
}