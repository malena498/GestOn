using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;

namespace GestOn2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario u = Sistema.GetInstancia().BuscarUsuarioEmail(txtEmail.Text);
            if (String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtPassUser.Text))
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos.";
            }
            else if (u == null)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Este usuario no existe en el sistema.";
            }
            else
            {
                //HttpCookie userIdCookie = new HttpCookie("UserID");
                //userIdCookie.Value = u.UserId.ToString();
                //Response.Cookies.Add(userIdCookie);
                string encriptada = Encriptar(txtPassUser.Text);
                if (u.UserContrasenia.Equals(encriptada))
                {
                    Session["IdUsuario"] = u.UserId;
                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(u.UserNombre.ToString(), false);
                    Response.Redirect("~/Inicio.aspx");
                }
                else {
                    lblResultado.Visible = true;
                    lblResultado.Text = "Contraseña incorrecta";
                }
            }
        }

        protected void lnkRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registrarse.aspx");
        }

        // Encripta la contraseña para compararla con la guardada en BD
        private static string Encriptar(string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }
    }
}
/*
  Usuario usu = Sistema.GetInstancia().BuscarUsuarioId(int.Parse(txtIdUsuario.Text), Session["NombreBase"].ToString());
            if (usu == null)
            {
                lbMensaje.Text = "Error al iniciar sesión, inténtelo nuevamente";
            }
            else
            {
                if (String.IsNullOrEmpty(ddlEmisores.SelectedValue))
                {
                    lbMensaje.Text = "Debe seleccionar un emisor";
                }
                else if (String.IsNullOrEmpty(ddlSucursales.SelectedValue))
                {
                    lbMensaje.Text = "Debe seleccionar una sucursal";
                }
                else
                {
                    EmpresaGeneral empresa = Sistema.GetInstancia().ObtenerDatosEmisorId(int.Parse(ddlEmisores.SelectedValue));
                    Session["rut"] = empresa.ruc;
                    Session["NombreBase"] = empresa.NombreBaseDatos;
                    Session["idSucursal"] = ddlSucursales.SelectedValue;
                    Session["IdUsuario"] = usu.IdUsuario;
                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(usu.Nombre.ToString(), false);
                }
            }
     */
