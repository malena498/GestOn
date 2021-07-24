using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;


namespace GestOn2.ABMS
{
    public partial class FormPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if (Session["IdUsuario"] != null)
                //{
                //    String idUsuarioLogueado = Session["IdUsuario"].ToString();
                //}
                //else
                //{
                //    Response.Redirect("~/Login.aspx");
                //}
                ListProductos1.DataSource = Sistema.GetInstancia().ListadoProductos();
                ListProductos1.DataTextField = "ProductoNombre";
                ListProductos1.DataValueField = "ProductoId";
                ListProductos1.DataBind();
                txtFechaEntregaPedido.Text = DateTime.Now.ToShortDateString();
                Session["Tabla"] = null;
                Session["IdUsuario"] = 1;
                llenarGrillaPedidos();
            }
        }

        protected void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.
            if (CompleteCampos())
            {
                lblInformativo.Text = "Debe completar todos los campos";
                lblInformativo.Visible = true;
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer el insert.
            else
            {
                lblInformativo.Visible = false;
                lblInformativo.Text = "";
                
                DateTime fchEntrega = DateTime.Parse(txtFechaEntregaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = int.Parse(Session["IdUsuario"].ToString());

                List<int> lstitems = new List<int>();

                Pedido p = new Pedido();
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.UserId = user;

                DataTable dt = Session["Tabla"] as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idProd = int.Parse(dt.Rows[i]["IdProducto"].ToString());
                    lstitems.Add(idProd);
                }
                int id = Sistema.GetInstancia().GuardarPedido(p, lstitems);
                if (id > 0)
                {
                    List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();
                   
                    for (int i = 0; i <dt.Rows.Count; i++) {
                        ProductoPedidoCantidad productoPedidoCantidad = new ProductoPedidoCantidad();

                        productoPedidoCantidad.ProductoId = int.Parse(dt.Rows[i]["IdProducto"].ToString());
                        productoPedidoCantidad.Cantidad =   int.Parse(dt.Rows[i]["cantidad"].ToString());
                        productoPedidoCantidad.IdPedido = id;

                        
                        lista.Add(productoPedidoCantidad);
                    }
                    bool exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(lista);
                    if(exito)
                    {
                        lblInformativo.Text = "Se guardo con éxito";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        Session["Tabla"] = null;

                        //Elimino campos luego que se inserto con éxito
                        VaciarCampos();
                    }
                }
                else
                {
                    lblInformativo.Text = "No se guardo (Error)";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
        }

        protected void btnAgregarTodo_Click(object sender, EventArgs e)
        {
            if(ListProductos1.SelectedItem != null) {

                int idProducto = int.Parse(ListProductos1.SelectedValue);
                int cantidad = int.Parse(txtCantidadProducto.Text);
                Producto p = Sistema.GetInstancia().BuscarProducto(idProducto);
                String nombre = p.ProductoNombre;
                if (Session["Tabla"] != null)
                {
                    DataTable dt = Session["Tabla"] as DataTable;
                    DataRow row; 
                    row = dt.NewRow();
                    row["IdProducto"] = idProducto;
                    row["Nombre"] = nombre;
                    row["Cantidad"] = cantidad;
                    dt.Rows.Add(row);

                    Session["Tabla"] = dt;

                    GridViewProductos.DataSource = dt;
                    GridViewProductos.DataBind();

                    ListProductos1.Items.Remove(ListProductos1.Items.FindByValue(idProducto.ToString()));
                    txtCantidadProducto.Text = string.Empty;

                }
                else
                {
                    DataTable dt = new DataTable("DatosProductos");
                    DataColumn column;
                    DataColumn column1;
                    DataColumn column2;
                    DataRow row;

                    column = new DataColumn();
                    column.ColumnName = "IdProducto";


                    column1 = new DataColumn();
                    column1.ColumnName = "Nombre";

                    column2 = new DataColumn();
                    column2.ColumnName = "Cantidad";

                    dt.Columns.Add(column);
                    dt.Columns.Add(column1);
                    dt.Columns.Add(column2);

                    row = dt.NewRow();
                    row["idProducto"] = idProducto;
                    row["nombre"] = nombre;
                    row["cantidad"] = cantidad;
                    dt.Rows.Add(row);

                    Session["Tabla"] = dt;

                    GridViewProductos.DataSource = dt;
                    GridViewProductos.DataBind();

                }

            }
        }

        public bool CompleteCampos()
        {
            if (RadioBtnSi.Checked == true)
            {
                if (String.IsNullOrEmpty(txtFechaEntregaPedido.Text) ||
                   String.IsNullOrEmpty(txtDireccion.Text) || String.IsNullOrEmpty(txtDescripcion.InnerText ))
                {
                    return true;
                }
                else { return false; }
            }
            else if (RadioBtnNo.Checked == false)
            {
                if (String.IsNullOrEmpty(txtFechaEntregaPedido.Text)||
                    String.IsNullOrEmpty(txtDescripcion.InnerText))
                {
                    return true;
                }
                else { return false; }
            }
            else return false;
        }

        public void VaciarCampos() {
            txtId.Text = string.Empty;
            txtFechaEntregaPedido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDescripcion.InnerText = string.Empty;
            txtCantidadProducto.Text = string.Empty;
        }

        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            TimerMensajes.Enabled = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                Pedido p = Sistema.GetInstancia().BuscarPedido(id);
                if (p != null)
                {
                    if (p.Activo == true)
                    {
                        txtDescripcion.InnerText = p.Descripcion.ToString();
                        txtDireccion.Text = p.Direccion.ToString();
                        if (txtDireccion.Text == "")
                        {
                            txtDireccion.Visible = false;
                            RadioBtnNo.Checked = true;
                            lblDireccion.Visible = false;
                        }
                        else
                        {
                            RadioBtnSi.Checked = true;
                            lblDireccion.Visible = true;
                            txtDireccion.Visible = true;
                        }
                        txtFechaEntregaPedido.Text = p.FechaPedido.ToString();
                    }
                    else
                    {
                        lblInformativo.Text = "El pedido fue dada de baja";
                        txtDescripcion.InnerText = "";
                        txtDireccion.Text = "";
                        RadioBtnNo.Checked = true;
                        txtDireccion.Visible = false;
                        lblDireccion.Visible = false;
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                    }
                }
                else
                {
                    lblInformativo.Text = "El pedido buscado no éxiste en el sistema";
                    txtDescripcion.InnerText = "";
                    txtDireccion.Visible = false;
                    lblDireccion.Visible = false;
                    RadioBtnNo.Checked = true;
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblInformativo.Text = "Debe completar id del pedido";
                RadioBtnNo.Checked = true;
                lblDireccion.Visible = false;
                txtDireccion.Text = "";
                lblInformativo.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        protected void RadioBtnSi_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnSi.Checked) { 
                txtDireccion.Enabled = true;
                txtDireccion.Visible = true;
                lblDireccion.Visible = true;
            }
        }

        protected void RadioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDireccion.Enabled = false;
            txtDireccion.Text = "";
            txtDireccion.Visible = false;
            lblDireccion.Visible = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.
            if (CompleteCampos())
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Debe completar todos los campos";
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer la modificación.
            else
            {
                lblInformativo.Visible = false;
                lblInformativo.Text = "";
                int id = int.Parse(txtId.Text);
                DateTime fchEntrega = DateTime.Parse(txtFechaEntregaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;

                List<int> lstitems = new List<int>();

                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.IdPedido = id;
                p.UserId = user;

                bool exito = Sistema.GetInstancia().ModificarPedido(p, lstitems);
                if (exito)
                {
                    lblInformativo.Text = "Se modificó con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    VaciarCampos();
                }
                else
                {
                    lblInformativo.Text = "No se pudo modifiar ";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
        }

        protected void ListProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
                txtCantidadProducto.Focus();
        }

        protected void txtCantidadProducto_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCantidadProducto.Text))
            {
                btnAgregarTodo.Enabled = true;
            }
            else btnAgregarTodo.Enabled = false;
        }

        protected void ListProductos1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidadProducto.Focus();
        }
        
        protected void llenarGrillaPedidos()
        {
            int id = int.Parse(Session["IdUsuario"].ToString());
            GridViewPedidos.DataSource = Sistema.GetInstancia().ListadoPedidosUsuario(id);
            GridViewPedidos.DataBind();
        }

        protected void GridViewProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProductos.EditIndex = e.NewEditIndex;
            llenarGrillaProductos();
        }

        protected void llenarGrillaProductos()
        {
            DataTable dt = Session["Tabla"] as DataTable;
            GridViewProductos.DataSource = dt;
            GridViewProductos.DataBind();
        }

        protected void GridViewProductos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewProductos.Rows[e.RowIndex];
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
            GridViewProductos.EditIndex = -1;
            llenarGrillaProductos();
        }

        protected void GridViewProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProductos.EditIndex = -1;
            llenarGrillaProductos();
        }

        protected void GridViewProductos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewProductos.Rows[e.RowIndex];
                DataTable dt = Session["Tabla"] as DataTable;
                dt.Rows[e.RowIndex].Delete();
                Session["Tabla"] = dt;
                lblInformativo.Visible = true;
                lblInformativo.Text = "Se elimino con éxito";
                GridViewProductos.EditIndex = -1;
                llenarGrillaProductos();
                
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}