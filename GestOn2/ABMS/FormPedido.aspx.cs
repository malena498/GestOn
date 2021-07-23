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
                int identificador = int.Parse(txtId.Text);
                txtFechaPedido.Text = DateTime.Now.ToLongTimeString();
                DateTime fchPedido = DateTime.Parse(txtFechaPedido.Text);
                DateTime fchEntrega = DateTime.Parse(txtFechaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;//int.Parse(Session["IdUsuario"].ToString());

                List<int> lstitems = new List<int>();

                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.FechaPedido = fchPedido;
                p.IdPedido = identificador;
                p.UserId = user;

                int id = Sistema.GetInstancia().GuardarPedido(p, lstitems);
                if (id > 0)
                {
                    List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();
                    DataTable dt = Session["Tabla"] as DataTable;
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
                //ListSeleccionados.Items.Add(ListProductos1.SelectedItem);
                //ListProductos1.Items.Remove(ListProductos1.SelectedItem);

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
                if (txtId.Text == "" || txtFechaPedido.Text == "" ||
                   txtDireccion.Text == "" || txtDescripcion.InnerText == "")
                {
                    return true;
                }
                else { return false; }
            }
            else if (RadioBtnNo.Checked == false)
            {
                if (txtId.Text == "" || txtFechaPedido.Text == "" ||
                    txtDescripcion.InnerText == "")
                {
                    return true;
                }
                else { return false; }
            }
            else return false;
        }

        public void VaciarCampos() {
            txtId.Text = "";
            txtFechaPedido.Text = "";
            txtDireccion.Text = "";
            txtDescripcion.InnerText = "";
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
                        txtFechaPedido.Text = p.FechaPedido.ToString();
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
                DateTime fchPedido = DateTime.Parse(txtFechaPedido.Text);
                DateTime fchEntrega = DateTime.Parse(txtFechaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;

                List<int> lstitems = new List<int>();

                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.FechaPedido = fchPedido;
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
        //protected void GridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.RowState == DataControlRowState.Edit)
        //        {

        //            //LinkButton btnEditar = FindControl("btnEditar") as LinkButton;
        //            //btnEditar.Visible = false;
        //            //LinkButton btnBorrar = FindControl("btnBorrar") as LinkButton;
        //            //btnBorrar.Visible = false;
        //            //LinkButton btnCancelar = FindControl("btnCancelar") as LinkButton;
        //            //btnCancelar.Visible = true;
        //            //LinkButton btnUpdate = FindControl("btnUpdate") as LinkButton;
        //            //btnUpdate.Visible = true;

        //        }


        //    }
        //}

        //protected void GridViewProductos_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    GridViewProductos.EditIndex = e.NewEditIndex;
        //    llenarGrillaProducto();
        //}


        //protected void llenarGrillaProducto()
        //{
        //    GridViewProductos.DataSource = Sistema.GetInstancia().ListadoProductos();
        //    GridViewProductos.DataBind();
        //}

        //protected void GridViewProductos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        //{
        //    bool exito = false;
        //    try
        //    {
        //        List<int> ProdyCantidad = new List<int>();

        //        foreach (GridViewRow row in GridViewProductos.Rows)
        //        {
        //            if (row.RowType == DataControlRowType.DataRow)
        //            {
        //                CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
        //                if (chkRow.Checked)
        //                {
        //                    int Id = Convert.ToInt32((row.FindControl("lblIdProducto") as Label).Text);
        //                    int cantidad = Convert.ToInt32((row.FindControl("txtCantidad") as TextBox).Text);
        //                    ProdyCantidad.Add(Id);
        //                    ProdyCantidad.Add(cantidad);
        //                }
        //            }

        //            if (ProdyCantidad.Count > 0)
        //            {
        //                exito = true;
        //                llenarGrillaProducto();
        //            }
        //            if (exito) lblInformativo.Text = "quedo";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exito = false;
        //    }
        //}


        //protected void GridViewProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GridViewProductos.EditIndex = -1;
        //    llenarGrillaProducto();
        //}

        //protected void GridViewProductos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    GridViewRow row = GridViewProductos.Rows[e.RowIndex];
        //    int Id = Convert.ToInt32(GridViewProductos.DataKeys[e.RowIndex].Values[0]);
        //    bool exito = Sistema.GetInstancia().EliminarProducto(Id);
        //    if (exito)
        //    {
        //        lblInformativo.Visible = true;
        //        lblInformativo.Text = "Se elimino con éxito";
        //        GridViewProductos.EditIndex = -1;
        //        llenarGrillaProducto();
        //    }
        //}

        //protected void OnPaging(object sender, GridViewPageEventArgs e)
        //{
        //    GridViewProductos.PageIndex = e.NewPageIndex;
        //    this.llenarGrillaProducto();
        //}
    }
}