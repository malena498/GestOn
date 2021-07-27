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

        protected void GridViewPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPedidos.EditIndex = e.NewEditIndex;
            llenarGrillaPedidos();
        }

        protected void GridViewPedidos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
            DataTable dt = Session["Tabla"] as DataTable;
            DataRow row1;
            row1 = dt.NewRow();
            row1["IdProducto"] = (row.FindControl("lblIdProducto") as Label).Text;
            row1["Nombre"] = (row.FindControl("txtNombre") as TextBox).Text;
            row1["Cantidad"] = (row.FindControl("txtCantidad") as TextBox).Text;
            dt.Rows.Add(row1);
            dt.Rows[e.RowIndex].Delete();
            Session["Tabla"] = dt;

            lblInformativo.Visible = true;
            lblInformativo.Text = "Se modificó con éxito";
            GridViewPedidos.EditIndex = -1;
        }

        protected void GridViewPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPedidos.EditIndex = -1;
            llenarGrillaPedidos();
        }

        protected void GridViewPedidos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
                DataTable dt = Session["Tabla"] as DataTable;
                dt.Rows[e.RowIndex].Delete();
                Session["Tabla"] = dt;
                lblInformativo.Visible = true;
                lblInformativo.Text = "Se elimino con éxito";
                GridViewPedidos.EditIndex = -1;
                llenarGrillaPedidos();
            }
            catch (Exception ex)
            {
                return;
            }
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
            int usuario = Convert.ToInt32(ListPedidoUsuario.SelectedItem.Value);
            List<Pedido> lista = Sistema.GetInstancia().ListadoPedidosUsuario(usuario);
            GridViewPedidos.DataSource = lista;
            GridViewPedidos.DataBind();
        }

        protected void lnkProductos_Click(object sender, EventArgs e)
        {
            lblModalTitle.Text = "Valido que el model se muestre con éxito";
            lblModalBody.Text = "Cuerpo del modal";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }
    }
}