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
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        /* Función para loguearse y acceder a las distintas funcionalidades que brinda el sistema pidiendo email y contraseña*/
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario u = Sistema.GetInstancia().BuscarUsuarioEmail(txtEmail.Text);
            if (String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtPassUser.Text))
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos.";
            }
            else
            {
                if (u == null)
                {
                    lblResultado.Visible = true;
                    lblResultado.Text = "Este usuario no existe en el sistema.";
                }
                else
                {
                    string encriptada = Encriptar(txtPassUser.Text);
                    if (u.UserContrasenia.Equals(encriptada))
                    {
                        Session["IdUsuario"] = u.UserId;
                        System.Web.Security.FormsAuthentication.RedirectFromLoginPage(u.UserNombre.ToString(), false);
                        Response.Redirect("~/Inicio.aspx");
                    }
                    else
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = "Contraseña incorrecta";
                    }
                }
            }
        }

        /* Redirige al usuario a la pantalla de registro de usuarios*/
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

        /* Permite al usuario cambiar su contraseña */
        protected void lnkRestablecerContraseña_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblResultado.Text = "Ingrese su e-mail";
            }
            else
            {
                Usuario user = Sistema.GetInstancia().BuscarUsuarioEmail(txtEmail.Text);
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                if (user != null)
                {
                    EnviarMail(c.Valor, user.UserEmail, user);
                }
                else
                {
                    lblResultado.Text = "No se encontró un usuario con el e-mail especificado";
                }
            }
            
        }

        /* Envío de Email utilizado para restablecer la contraseña */
        protected void EnviarMail(String mailEmpresa, String mailDestino, Usuario u)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(mailDestino, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(mailEmpresa); //Correo destino?
            correo.Subject = "Restablecer contraseña."; //Asunto
            correo.Body = "Para restablecer su contraseña dirijase al siguiente link:https://bertinatpapeleria.com/CambiarContraseña.aspx?Id="+ u.UserId; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 25; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa, "");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);
        }
    }
}

