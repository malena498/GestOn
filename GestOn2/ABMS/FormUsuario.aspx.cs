﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
using System.Drawing;
using System.IO;

namespace GestOn2
{
    public partial class FormUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["NombreBase"] = "GestOn";
                ddlCategoriaUsuario.DataSource = Sistema.GetInstancia().ListadoNiveles();
                ddlCategoriaUsuario.DataTextField = "NombreNivel";
                ddlCategoriaUsuario.DataValueField = "IdNivel";
                ddlCategoriaUsuario.DataBind();
            }

            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool ciValida = ValidarCI(txtCedulaUser.Text);
            if (ciValida)
            {
                try
                {
                    Usuario u = new Usuario();
                    String nameDB = Session["NombreBase"].ToString();
                    u.UserNombre = txtNombreUser.Text;
                    u.UserEmail = txtEmailUser.Text;
                    u.UserCedula = txtCedulaUser.Text;
                    u.UserTelefono = txtTelefonoUser.Text;
                    u.UserContrasenia = txtPassUser.Text;
                    u.IdNivel = int.Parse(ddlCategoriaUsuario.SelectedValue);

                    bool exito = Sistema.GetInstancia().GuardarUsuario(u,  nameDB);
                    if (exito)
                    {
                        lblResultado.Text = "Se guardo con éxito";
                        limpiar();
                    }
                }
                catch (Exception ex)
                {
                    lblResultado.Text = "No se logro guardar";
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtNombreUser.Text == "" || txtEmailUser.Text == "" || txtTelefonoUser.Text == ""
               || txtCedulaUser.Text == "" || txtPassUser.Text == "" )
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos";
                //TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer la modificación.
            else
            {
                lblResultado.Visible = false;
                lblResultado.Text = "";
                Usuario us = null;
                us = Sistema.GetInstancia().BuscarUsuario(int.Parse(txtIdUsuario.Text));
                us.UserNombre= txtNombreUser.Text;
                us.UserEmail= txtEmailUser.Text;
                us.UserTelefono = txtTelefonoUser.Text;
                us.UserCedula = txtCedulaUser.Text;
                us.UserContrasenia = txtPassUser.Text;
                us.IdNivel = int.Parse(ddlCategoriaUsuario.SelectedValue);

                bool exito = Sistema.GetInstancia().ModificarUsuario(us);
                if (exito)
                {
                    lblResultado.Text = "Se modificó con éxito";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    limpiar();
                }
                else
                {
                    lblResultado.Text = "No se guardo (Error)";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdUsuario.Text);
            Usuario us = Sistema.GetInstancia().BuscarUsuario(id);
            bool exito = Sistema.GetInstancia().EliminarUsuario(us);
            if (exito)
            {
                lblResultado.Text = "Se elimino con éxito";
                lblResultado.Visible = true;
                //TimerMensajes.Enabled = true;

                //Elimino campos luego que se modifico con éxito
                limpiar();
            }
            else
            {
                lblResultado.Text = "No se pudo eliminar ";
                lblResultado.Visible = true;
                //TimerMensajes.Enabled = true;
            }
        }

        protected void limpiar()
        {
            txtNombreUser.Text = string.Empty;
            txtEmailUser.Text = string.Empty;
            txtTelefonoUser.Text = string.Empty;
            txtCedulaUser.Text = string.Empty;
            txtPassUser.Text = string.Empty;
            ddlCategoriaUsuario.ClearSelection();
        }

        public bool ValidarCI(String ci)
        {
            try
            {
                //Control inicial sobre la cantidad de números ingresados. 
                if (ci.Length == 8 || ci.Length == 7)
                {
                    int[] _formula = { 2, 9, 8, 7, 6, 3, 4 };
                    int _suma = 0;
                    int _guion = 0;
                    int _aux = 0;
                    int[] _numero = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

                    if (ci.Length == 8)
                    {
                        _numero[0] = Convert.ToInt32(ci[0].ToString());
                        _numero[1] = Convert.ToInt32(ci[1].ToString());
                        _numero[2] = Convert.ToInt32(ci[2].ToString());
                        _numero[3] = Convert.ToInt32(ci[3].ToString());
                        _numero[4] = Convert.ToInt32(ci[4].ToString());
                        _numero[5] = Convert.ToInt32(ci[5].ToString());
                        _numero[6] = Convert.ToInt32(ci[6].ToString());
                        _numero[7] = Convert.ToInt32(ci[7].ToString());
                    }
                    //Para cédulas menores a un millón. 
                    else if (ci.Length == 7)
                    {
                        _numero[0] = 0;
                        _numero[1] = Convert.ToInt32(ci[0].ToString());
                        _numero[2] = Convert.ToInt32(ci[1].ToString());
                        _numero[3] = Convert.ToInt32(ci[2].ToString());
                        _numero[4] = Convert.ToInt32(ci[3].ToString());
                        _numero[5] = Convert.ToInt32(ci[4].ToString());
                        _numero[6] = Convert.ToInt32(ci[5].ToString());
                        _numero[7] = Convert.ToInt32(ci[6].ToString());
                    }
                    _suma = (_numero[0] * _formula[0]) + (_numero[1] * _formula[1]) + (_numero[2] * _formula[2]) + (_numero[3] * _formula[3]) + (_numero[4] * _formula[4]) + (_numero[5] * _formula[5]) + (_numero[6] * _formula[6]);
                    for (int i = 0; i < 10; i++)
                    {
                        _aux = _suma + i;
                        if (_aux % 10 == 0)
                        {
                            _guion = _aux - _suma;
                            i = 10;
                        }
                    }
                    if (_numero[7] == _guion)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
                if (txtIdUsuario.Text != "")
                {
                    int id = Int32.Parse(txtIdUsuario.Text);

                    Usuario u = Sistema.GetInstancia().BuscarUsuario(id);
                    if (u != null)
                    {
                    txtNombreUser.Text = u.UserNombre;
                    txtEmailUser.Text = u.UserEmail;
                    txtCedulaUser.Text = u.UserCedula;
                    txtTelefonoUser.Text = u.UserTelefono;
                    //txtPassUser.Text = u.UserContrasenia;
                        btnModificar.Enabled = true;
                    }
                    else
                    {
                        lblResultado.Text = "El usuario buscado no éxiste en el sistema";
                        lblResultado.Visible = true;
                        //TimerMensajes.Enabled = true;
                    }
                }
                else
                {
                    lblResultado.Text = "Debe completar id del usuario";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;
                }
            
        }
    }
}