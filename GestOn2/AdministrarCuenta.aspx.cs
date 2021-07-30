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
    public partial class CambiarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    String idUsuarioLogueado = Session["IdUsuario"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["IdUsuario"].ToString());
            if (txtConfirmarContraseña.Text.Equals(txtContraseña.Text))
            {
                string encriptada = Encriptar(txtConfirmarContraseña.Text);
                Usuario user = Sistema.GetInstancia().BuscarUsuario(id);
                user.UserContrasenia = encriptada;

                bool exito = Sistema.GetInstancia().ModificarUsuario(user);
                if (exito)
                {
                    lblResultado.Text = "La contraseña se modificó con éxito";
                    lblResultado.Visible = true;
                }
                else
                {
                    lblResultado.Text = "Error al modificar la contraseña";
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Las contraseñas no coinciden";
            }
        }

        private static string Encriptar(string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

    }
}