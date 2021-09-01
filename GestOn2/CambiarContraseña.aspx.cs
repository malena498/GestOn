using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GestOn2
{
    public partial class CambiarContraseña1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtContrasenia.Text) || String.IsNullOrEmpty(txtConfirmarContrasenia.Text))
            {
                string valor = Convert.ToString(Request.QueryString["Id"]);
                Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(valor));
                if (txtContrasenia.Text.Equals(txtConfirmarContrasenia.Text))
                {
                    string encriptada = Encriptar(txtContrasenia.Text);
                    u.UserContrasenia = txtContrasenia.Text;
                    bool exito = Sistema.GetInstancia().ModificarUsuario(u);
                    if (exito)
                    {
                        lblResultado.Text = "Contraseña modificada con exito.";
                        divCamContraseña.Visible = false;


                    }
                    else
                    {
                        lblResultado.Text = "No se pudo cambiar la contraseña.";

                    }
                }
                else
                {
                    lblResultado.Text = "Las contraseñas no coinciden.";
                }
            }
            else
            {
                lblResultado.Text = "Complete los campos.";
            }
        }
        private static string Encriptar(string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        //
    }
}