﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;

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
            List<Configuracion> configuraciones = new List<Configuracion>();
            Configuracion conf1 = new Configuracion();
            conf1.Nombre = "EmailEmpresa";
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

            bool exito = Sistema.GetInstancia().GuardarConfiguraciones(configuraciones);
            if (exito)
            {
                string script = @"<script type='text/javascript'>
                            alerta(Configuraciónes guardadas con éxito.);
                        </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            else
            {
                string script = @"<script type='text/javascript'>
                            alerta(No se logro guardar.);
                        </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }

        }

        protected void CargarConfiguraciones()
        {
            Configuracion conf1 = Sistema.GetInstancia().BuscarConfiguracion("EmailEmpresa");
            txtEmail.Text = conf1.Valor;

            Configuracion conf2 = Sistema.GetInstancia().BuscarConfiguracion("TelefonoEmpresa");
            txtTelefono.Text = conf2.Valor;

            Configuracion conf3 = Sistema.GetInstancia().BuscarConfiguracion("LinkIG");
            txtLinkIG.Text = conf3.Valor;

            Configuracion conf4 = Sistema.GetInstancia().BuscarConfiguracion("LinkFB");
            txtLinkFB.Text = conf4.Valor;

            Configuracion conf5 = Sistema.GetInstancia().BuscarConfiguracion("CostoEnvio");
            txtCostoEnvio.Text = conf5.Valor;
        }
    }
}