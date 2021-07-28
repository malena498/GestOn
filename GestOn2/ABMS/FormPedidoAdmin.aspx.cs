using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;

namespace GestOn2.ABMS
{
    public partial class FormPedidoAdmin : System.Web.UI.Page
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
                ListarUser();
                llenarGrillaPedidos();
            }
        }

        protected void llenarGrillaPedidos()
        {
            int id = int.Parse(Session["IdUsuario"].ToString());
            GridViewPedidos.DataSource = Sistema.GetInstancia().ListadoPedidos();
            GridViewPedidos.DataBind();
        }

        protected void llenarDatosPedido(int idPedido) {
            Pedido p = Sistema.GetInstancia().BuscarPedido(idPedido);
            if (p != null) { 
                txtDescripcionPedidoA.Text = p.Descripcion;
                txtDireccionPedidoA.Text = p.Direccion;
                int idUser = p.UserId;
                txtIdPedidoA.Text = idPedido.ToString();
                txtPrecioA.Text = p.Precio.ToString();
                if (p.Estado.Equals("Realizado"))
                    ddlEstado.SelectedIndex = 0;
                if (p.Estado.Equals("Pendiente"))
                    ddlEstado.SelectedIndex = 1;
                if (p.Estado.Equals("Cancelado"))
                    ddlEstado.SelectedIndex = 2;
                Usuario u = Sistema.GetInstancia().BuscarUsuario(1);
                txtNombreUsuarioA.Text = u.UserNombre;
            }
        }

        protected void GridViewPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPedidos.EditIndex = e.NewEditIndex;
            llenarGrillaPedidos();
        }

        protected void GridViewPedidos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("IdPedido") as Label).Text);
            btnActualizarPedido.Enabled = true;
            llenarDatosPedido(Id);
        }

        protected void GridViewPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPedidos.EditIndex = -1;
            llenarGrillaPedidos();
        }

        protected void GridViewPedidos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedidos.PageIndex = e.NewPageIndex;
            llenarGrillaPedidos();
        }

        protected void ListPedidoEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            String estado = ListPedidoEstado.SelectedItem.Value;
            if (!estado.Equals("Todos"))
            {
                GridViewPedidos.DataSource = Sistema.GetInstancia().ListadoPedidosEstado(estado);
                GridViewPedidos.DataBind();
            }
            else
                llenarGrillaPedidos();
        }

        protected void ListarUser() {

            List<Usuario> datos = Sistema.GetInstancia().ListadoUsuarios();

            ListPedidoUsuario.DataSource = datos;
            //Definimos el campo que contendrá los valores para el control
            ListPedidoUsuario.DataValueField = "UserId";
            //Definimos el campo que contendrá los textos que se verán en el control
            ListPedidoUsuario.DataTextField = "UserNombre";
            //Enlazamos los valores de los datos con el contenido del Control
            ListPedidoUsuario.DataBind();
        }

        protected void ListPedidoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrillaProductos();
        }

        protected void llenarGrillaProductos()
        {
            int Id = int.Parse(txtIdPedidoA.Text);
            List<ProductoPedidoCantidad> list = Sistema.GetInstancia().ListadoProductosPedido(Id);
            GridViewProductos.DataSource = list;
            GridViewProductos.DataBind();
            DivVisualizarPedidos.Visible = false;
            DivVisualizarProductos.Visible = true;
        }

        protected void lnkProductos_Click(object sender, EventArgs e)
        {
            llenarGrillaProductos();
        }

        protected void btnActualizarPedido_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdPedidoA.Text);
            Pedido p = Sistema.GetInstancia().BuscarPedido(id);
            p.Estado = ddlEstado.SelectedValue;
            if (ddlEstado.SelectedItem.Value == "Cancelado")
            p.Activo = false;
            bool exito = Sistema.GetInstancia().ModificarPedidoAdministrador(p);
            if (exito)
            {
                lblInformativo.Text = "Se modificó con éxito";
                lblInformativo.Visible = true;
            }
            else
            {
                lblInformativo.Text = "No se pudo modifiar ";
                lblInformativo.Visible = true;
            }
        }
    }
}