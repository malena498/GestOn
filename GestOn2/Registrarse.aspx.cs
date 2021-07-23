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
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategoriaUsuario.DataSource = Sistema.GetInstancia().ListadoNiveles();
                ddlCategoriaUsuario.DataTextField = "NombreNivel";
                ddlCategoriaUsuario.DataValueField = "IdNivel";
                ddlCategoriaUsuario.DataBind();
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            if (confirmar()) { 
            bool ciValida = ValidarCI(txtDocumento.Text);
                if (ciValida)
                {
                    try
                    {
                        bool exito = false;
                        if (confirmar())
                        {
                            if (txtConfirmarContrasenia.Text.Equals(txtContrasenia.Text))
                            {
                                string contraseña = Encriptar(txtContrasenia.Text);
                                Usuario u = new Usuario();
                                u.UserNombre = txtNombre.Text;
                                u.UserEmail = txtEmail.Text;
                                u.UserCedula = txtDocumento.Text;
                                u.UserTelefono = txtTelefono.Text;
                                u.UserContrasenia = contraseña;
                                u.IdNivel = int.Parse(ddlCategoriaUsuario.SelectedValue);
                                exito = Sistema.GetInstancia().GuardarUsuario(u);
                            }
                            else
                            {
                                lblResultado.Visible = true;
                                lblResultado.Text = "Las contraseñas no coinciden";
                            }
                            if (exito)
                            {
                                lblResultado.Visible = true;
                                lblResultado.Text = "Registrado con éxito";
                                limpiar();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = "Error al registrarse";
                    }
                }
                else
                {
                    lblResultado.Visible = true;
                    lblResultado.Text = "La cédula ingresada no es válida";
                }
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos";
            }
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

        protected void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtContrasenia.Text= string.Empty;
            txtConfirmarContrasenia.Text = string.Empty;
            ddlCategoriaUsuario.ClearSelection();
        }

        protected bool confirmar()
        {
            bool res = false;
            if (String.IsNullOrEmpty(txtNombre.Text) ||
                String.IsNullOrEmpty(txtEmail.Text) ||
                String.IsNullOrEmpty(txtTelefono.Text) ||
                String.IsNullOrEmpty(txtContrasenia.Text) ||
                String.IsNullOrEmpty(txtConfirmarContrasenia.Text))
            {

                res = false;
            }
            else { res = true; }
            return res;
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
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