using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
using System.Drawing;
using System.IO;
using System.Data;

namespace GestOn2
{
    public partial class FormUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    llenarGrilla();
                    ddlCategoriaUsuario.DataSource = Sistema.GetInstancia().ListadoNiveles();
                    ddlCategoriaUsuario.DataTextField = "NombreNivel";
                    ddlCategoriaUsuario.DataValueField = "IdNivel";
                    ddlCategoriaUsuario.DataBind();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        /* REGISTRA UN NUEVO USUARIO EN EL SISTEMA, PERMITIENDO ASIGNARLE NÚMERO DE CARPETA Y TIPO DE USUARIO ADMINISTRADOR*/
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CamposIncompletos())
                {
                    Usuario us = Sistema.GetInstancia().BuscarUsuarioEmail(txtEmailUser.Text);
                    if (us != null)
                    {
                        divMensaje.Visible = true;
                        lblResultado.Text = "Ya existe un usuario ingresado con este E-mail.";
                    }
                    else
                    {
                        Usuario user = Sistema.GetInstancia().BuscarUsuarioCedula(txtCedulaUser.Text);
                        if (user != null)
                        {
                            divMensaje.Visible = true;
                            lblResultado.Text = "La cédula ingresada ya existe en el sistema.";
                        }
                        else
                        {
                            bool ciValida = ValidarCI(txtCedulaUser.Text);
                            if (ciValida)
                            {
                                if (txtPassUser.Text.Equals(txtPassUser2.Text))
                                {
                                    string contraseña = Encriptar(txtPassUser.Text);
                                    Usuario u = new Usuario();
                                    u.UserNombre = txtNombreUser.Text;
                                    u.NroCarpeta = Convert.ToInt32(txtNroCarpeta.Text);
                                    u.UserEmail = txtEmailUser.Text;
                                    u.UserCedula = txtCedulaUser.Text;
                                    u.UserTelefono = txtTelefonoUser.Text;
                                    u.UserContrasenia = contraseña;
                                    u.IdNivel = int.Parse(ddlCategoriaUsuario.SelectedValue);

                                    bool exito = Sistema.GetInstancia().GuardarUsuario(u);
                                    if (exito)
                                    {
                                        divMensaje.Visible = true;
                                        lblResultado.Text = "Registrado con éxito";
                                        limpiar();
                                    }
                                }
                                else
                                {
                                    divMensaje.Visible = true;
                                    lblResultado.Text = "Las contraseñas no coinciden";
                                }
                            }
                            else
                            {
                                divMensaje.Visible = true;
                                lblResultado.Text = "Cédula inválida";
                            }
                        }
                    }
                }
                else {
                    divMensaje.Visible = true;
                    lblResultado.Text = "Debe completar todos los campos";
                } 
            }
            catch (Exception ex)
            {
                divMensaje.Visible = true;
                lblResultado.Text = "Error al registrarse";
            }
        }

        /* BUSCA LISTADO DE USUARIOS FILTRANDOLO POR NOMBRE*/
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtNomUsuario.Text;
            if (nombre != "")
            {
                List<Usuario> users = Sistema.GetInstancia().BuscarUsuarioFiltros(nombre);
                if (users != null)
                {

                    GridViewUsuarios.DataSource = users;
                    GridViewUsuarios.DataBind();
                }
                else
                {
                    lblResultadoGrilla.Text = "El usuario buscado no éxiste en el sistema";
                    lblResultadoGrilla.Visible = true;
                }
            }

        }

        /* PERMITE CAMBIAR DE PAGINA EN LA GRILLA DE USUARIOS EN CASO QUE SE EXCEDAN LOS 5 REGISTROS DE LA PRIMER HOJA*/
        protected void GridViewUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsuarios.PageIndex = e.NewPageIndex;
            llenarGrilla();
        }

        /*VERIFICA SI LOS DATOS DEL FORMULARIO ESTÁN INCOMPLETOS, RETORNANDO TRUE EN CASO QUE LO ESTÉN*/
        protected bool CamposIncompletos()
        {
            if (String.IsNullOrEmpty(txtIdUsuario.Text) || String.IsNullOrEmpty(txtNombreUser.Text) ||
                String.IsNullOrEmpty(txtEmailUser.Text) || String.IsNullOrEmpty(txtCedulaUser.Text) ||
                String.IsNullOrEmpty(txtTelefonoUser.Text) || String.IsNullOrEmpty(txtPassUser.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* GESTIÓN DE USUARIOS EN LA GRILLA - ELIMINA - EDITA*/
        protected void GridViewUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    
                    DropDownList ddlNivel = (DropDownList)e.Row.FindControl("ddlNivel");
                    ddlNivel.DataSource = Sistema.GetInstancia().ListadoNiveles();
                    ddlNivel.DataTextField = "NombreNivel";
                    ddlNivel.DataValueField = "IdNivel";
                    ddlNivel.DataBind();

                }
            }
        }

        protected void GridViewUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsuarios.EditIndex = e.NewEditIndex;
            llenarGrilla();
        }

        protected void GridViewUsuarios_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewUsuarios.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdUsuario") as Label).Text);
            string nombre = (row.FindControl("txtNombre") as TextBox).Text;
            string email = (row.FindControl("txtEmail") as TextBox).Text;
            string cedula = (row.FindControl("txtDocumento") as TextBox).Text;
            string telefono = (row.FindControl("txtTeléfono") as TextBox).Text;
            bool activo = Convert.ToBoolean((row.FindControl("chkActivo1") as CheckBox).Checked);
            int IdNivel = Convert.ToInt32((row.FindControl("ddlNivel") as DropDownList).SelectedValue);

            if (Id == 0 || nombre == "" || email == "" || cedula == "" || telefono == "" || IdNivel == 0)
            {
                lblResultadoGrilla.Visible = true;
                lblResultadoGrilla.Text = "Debe completar todos los campos";
            }
            else
            { 
                bool ciValida = ValidarCI(cedula);
                if (ciValida)
                {
                    if (!ExisteCedula(cedula,Id))
                    {
                        if (!ExisteEmail(email,Id))
                        {
                            lblResultado.Visible = false;
                            lblResultado.Text = string.Empty;
                            Usuario us = null;
                            us = Sistema.GetInstancia().BuscarUsuario(Id);
                            us.UserNombre = nombre;
                            us.UserEmail = email;
                            us.UserTelefono = telefono;
                            us.UserCedula = cedula;
                            us.Activo = activo;
                            us.IdNivel = IdNivel;

                            bool exito = Sistema.GetInstancia().ModificarUsuario(us);
                            if (exito)
                            {
                                lblResultadoGrilla.Visible = true;
                                lblResultadoGrilla.Text = "Modificado con éxito";
                                GridViewUsuarios.EditIndex = -1;
                                llenarGrilla();
                            }
                        }
                        else
                        {
                            lblResultadoGrilla.Visible = true;
                            lblResultadoGrilla.Text = "Ya existe un usuario con ese Email";
                        }
                    }
                    else
                    {
                        lblResultadoGrilla.Visible = true;
                        lblResultadoGrilla.Text = "Ya existe un usuario con esa cédula";
                    }
                }
                else
                {
                    lblResultadoGrilla.Visible = true;
                    lblResultadoGrilla.Text = "Cédula inválida";
                }
            }
        }

        /*Válido si exíste un usuario con el Email pasado, retornando true en caso que ya exista ese Email en el sistema.*/
        private bool ExisteEmail(string Email, int id) {

            bool resultado = false;

            Usuario u = Sistema.GetInstancia().BuscarUsuarioEmail(Email);

            if (u != null && u.UserId != id)
                resultado = true;

            return resultado;
        }

        /*Válido si exíste un usuario con esa cédula pasada, retornando true en caso que ya exista esa CI en el sistema.*/
        private bool ExisteCedula(string Cedula, int id)
        {

            bool resultado = false;

            Usuario u = Sistema.GetInstancia().BuscarUsuarioCedula(Cedula);

            if (u != null && u.UserId != id)
                resultado = true;

            return resultado;
        }

        protected void GridViewUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsuarios.EditIndex = -1;
            llenarGrilla();
        }

        protected void GridViewUsuarios_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewUsuarios.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdUsuario") as Label).Text);
            bool exito = Sistema.GetInstancia().EliminarUsuario(Id);
            if (exito)
            {
                lblResultadoGrilla.Visible = true;
                lblResultadoGrilla.Text = "Se eliminó  con éxito";
                GridViewUsuarios.EditIndex = -1;
                llenarGrilla();
            }
            else
            {
                lblResultadoGrilla.Visible = true;
                lblResultadoGrilla.Text = "No se pudo eliminar";
            }
        }

        /*LLENA LA GRILLA GRIDVIEWUSUARIOS*/
        protected void llenarGrilla()
        {
            GridViewUsuarios.DataSource = Sistema.GetInstancia().ListadoUsuarios(); 
            GridViewUsuarios.DataBind();
        }

        /* VACIA TODOS LOS CAMPOS DEL FORMULARIO CON LOS DATOS DEL USUARIO A REGISTRAR*/
        protected void limpiar()
        {
            txtNombreUser.Text = string.Empty;
            txtEmailUser.Text = string.Empty;
            txtTelefonoUser.Text = string.Empty;
            txtCedulaUser.Text = string.Empty;
            txtPassUser.Text = string.Empty;
            ddlCategoriaUsuario.ClearSelection();
        }

        /*VÁLIDA QUE LA CÉDULA DEL USUARIO QUE SE DESEA INGRESAR SEA VÁLIDA EN URUGUAY*/
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

        /*MUESTRA EN GRILLA TODOS LOS USUARIOS DADOS DE BAJA*/
        protected void chkEliminados_CheckedChanged(object sender, EventArgs e)
        {
            /*if (chkEliminados.Checked)
            {
                GridViewUsuarios.DataSource = Sistema.GetInstancia().ListadoUsuariosEliminados();
                GridViewUsuarios.DataBind();
            }
            else
            {
                llenarGrilla();
            }*/
        }

        /*LISTA TODOS LOS USUARIOS FILTRANDOLOS POR CÉDULA DE IDENTIDAD*/
        protected void btnBuscarCedula_Click(object sender, EventArgs e)
        {
            string cedula = txtCedulaFiltro.Text;
            if (cedula != "")
            {
                List<Usuario> users = Sistema.GetInstancia().ListadoUsuariosCedula(cedula);
                if (users != null)
                {

                    GridViewUsuarios.DataSource = users;
                    GridViewUsuarios.DataBind();
                }
                else
                {
                    lblResultadoGrilla.Text = "El usuario buscado no éxiste en el sistema";
                    lblResultadoGrilla.Visible = true;
                }
            }
        }

        /* MUESTRA EL FORMULARIO PARA INGRESAR UN NUEVO USUARIO AL SISTEMA Y OCULTA EL LISTADO DE USUARIOS*/
        protected void lnkNuevoUsuario_Click(object sender, EventArgs e)
        {
            DivGridUsuario.Visible = false;
            DivFiltros.Visible = false;
            divNuevoUsuario.Visible = true;
        }

        /* MUESTRA EL LISTADO DE USUARIOS Y OCULTA EL FORMULARIO PARA INGRESAR UN NUEVO USUARIO*/
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            divNuevoUsuario.Visible = false;
            DivGridUsuario.Visible = true;
            DivFiltros.Visible = true;
            limpiar();
        }

        // ENCRIPTA LA CONTRASEÑA PASADA COMO PARÁMETRO PARA HACER POSTERIORMENTE VALIDACIONES O GUARDARLA EN BD
        private static string Encriptar(string password)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        protected void ddlSeleccionaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("Nombre"))
            {
                DivFiltroXCedula.Visible = false;
                DivFiltroXNombre.Visible = true;
            }
            else
            {
                DivFiltroXNombre.Visible = false;
                DivFiltroXCedula.Visible = true;
            }
        }
    }
}
