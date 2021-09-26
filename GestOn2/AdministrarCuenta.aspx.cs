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
                    CargarDatos(int.Parse(idUsuarioLogueado));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["IdUsuario"].ToString());
            if (txtConfirmarContraseña.Text.Equals(txtContraseña.Text))
            {
                string encriptada = Encriptar(txtConfirmarContraseña.Text);
                Usuario user = Sistema.GetInstancia().BuscarUsuario(id);
                user.UserContrasenia = encriptada;
                user.UserCedula = txtCedulaUser.Text;
                user.UserEmail = txtEmailUser.Text;
                user.UserNombre= txtNombreUser.Text;
                user.UserTelefono= txtTelefonoUser.Text;
                user.UserContrasenia= txtContraseña.Text;
                bool exito = Sistema.GetInstancia().ModificarUsuario(user);
                if (exito)
                {
                    lblResultado.Text = "Su cuenta se modificó con éxito";
                    lblResultado.Visible = true;
                }
                else
                {
                    lblResultado.Text = "Error al modificar su cuenta";
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

        protected void CargarDatos(int id)
        {
            Usuario user = Sistema.GetInstancia().BuscarUsuario(id);
            txtCedulaUser.Text = user.UserCedula;
            txtEmailUser.Text = user.UserEmail;
            txtNombreUser.Text = user.UserNombre;
            txtTelefonoUser.Text = user.UserTelefono;
            txtContraseña.Text = user.UserContrasenia;
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                int id = int.Parse(Session["IdUsuario"].ToString());
                bool ex = Sistema.GetInstancia().EliminarUsuario(id);
                if (ex)
                {
                    lblResultado.Text = "Cuenta eliminada con exito.";
                    Session.Remove("IdUsuario");
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
    

    }