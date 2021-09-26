using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;

using System.Threading;
using Microsoft.SqlServer.Server;
using System.IO;
namespace GestOn2
{
    public partial class Configuraciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    String idUsuarioLogueado = Session["IdUsuario"].ToString();
                    CargarConfiguraciones();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool ex = Sistema.GetInstancia().EliminarConfiguraciones();
            List<Configuracion> configuraciones = new List<Configuracion>();

            Configuracion conf1 = new Configuracion();
            conf1.Nombre = "CorreoEmpresa";
            conf1.Valor = txtEmail.Text;
            configuraciones.Add(conf1);

            Configuracion conf2 = new Configuracion();
            conf2.Nombre = "TelefonoEmpresa";
            conf2.Valor = txtTelefono.Text;
            configuraciones.Add(conf2);

            Configuracion conf3 = new Configuracion();
            conf3.Nombre = "LinkIG";
            conf3.Valor = txtLinkIG.Text;
            configuraciones.Add(conf3);

            Configuracion conf4 = new Configuracion();
            conf4.Nombre = "LinkFB";
            conf4.Valor = txtLinkFB.Text;
            configuraciones.Add(conf4);

            Configuracion conf5 = new Configuracion();
            conf5.Nombre = "CostoEnvio";
            conf5.Valor = txtCostoEnvio.Text;
            configuraciones.Add(conf5);

            Configuracion conf6 = new Configuracion();
            conf6.Nombre = "CorreoAdmin";
            conf6.Valor = txtEmailAdmin.Text;
            configuraciones.Add(conf6);

            Configuracion conf7 = new Configuracion();
            conf7.Nombre = "Contraseñamail";
            conf7.Valor = txtContrasenia.Text;
            configuraciones.Add(conf7);

            if (txtCostoEnvio.Text.Equals("") || txtLinkFB.Text.Equals("") || txtLinkIG.Text.Equals("") ||
                txtEmail.Text.Equals("") || txtTelefono.Text.Equals(""))
            {
                lblInformativo.Text= "Debe completar todos los campos.";
            }
            else
            {
                bool exito = Sistema.GetInstancia().GuardarConfiguraciones(configuraciones);
                if (exito)
                {
                    lblInformativo.Text = "Configuraciónes moddificadas con éxito.";
                   
                }
                else
                {
                    lblInformativo.Text = "No se logró modificar.";
                }
            }

        }

        protected void CargarConfiguraciones()
        {
            List<Configuracion> c = Sistema.GetInstancia().ListadoConfiguraciones();
            if (c != null)
            {
                Configuracion conf1 = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                if (conf1 != null)
                {
                    txtEmail.Text = conf1.Valor;
                }
                Configuracion conf2 = Sistema.GetInstancia().BuscarConfiguracion("TelefonoEmpresa");
                if (conf2 != null)
                {
                    txtTelefono.Text = conf2.Valor;
                }
                Configuracion conf3 = Sistema.GetInstancia().BuscarConfiguracion("LinkIG");
                if (conf3 != null)
                {
                    txtLinkIG.Text = conf3.Valor;
                }

                Configuracion conf4 = Sistema.GetInstancia().BuscarConfiguracion("LinkFB");
                if (conf4 != null)
                {
                    txtLinkFB.Text = conf4.Valor;
                }

                Configuracion conf5 = Sistema.GetInstancia().BuscarConfiguracion("CostoEnvio");
                if (conf5 != null)
                {
                    txtCostoEnvio.Text = conf5.Valor;
                }
                Configuracion conf6 = Sistema.GetInstancia().BuscarConfiguracion("CorreoAdmin");
                if (conf6 != null)
                {
                    txtEmailAdmin.Text = conf6.Valor;
                }
                Configuracion conf7 = Sistema.GetInstancia().BuscarConfiguracion("Contraseñamail");
                if (conf7 != null)
                {
                    txtContrasenia.Text = conf7.Valor;
                }
            }
        }
        protected void button1_Click(object sender, EventArgs e)
        {
            bool desea_respaldar = true;

            //poner cursor de relojito mintras respalda

            if (Directory.Exists(@"c:\ Respaldo"))
            {
                if (File.Exists(@"c:\ Respaldo\resp.bak"))
                {
                        File.Delete(@"c:\ Respaldo\resp.bak");
                }
            }
            else
                Directory.CreateDirectory(@"c:\ Respaldo");
            if (desea_respaldar)
            {
                bool ex = Sistema.GetInstancia().BackUp();
                if (ex)
                {
                    lblInformativo.Text = "El Respaldo de la base de datos fue realizado satisfactoriamente";
                }
            }
        }
    }
}