using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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


namespace GestOn2.ABMS
{
    public partial class FormPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    String idUsuarioLogueado = Session["IdUsuario"].ToString();
                    chkPedidoProducto.Checked = true;
                    ListProductos1.DataSource = Sistema.GetInstancia().ListadoProductos();
                    ListProductos1.DataTextField = "ProductoNombre";
                    ListProductos1.DataValueField = "ProductoId";
                    ListProductos1.DataBind();
                    Producto p = Sistema.GetInstancia().BuscarProducto(int.Parse(ListProductos1.SelectedValue));
                    lblPrecioproducto.Text =p.ProductoPrecioVenta.ToString();
                    
                    llenarGrillaPedidos();
                   // Session["Tabla"] = null;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
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
                DateTime ahora = DateTime.Now;
                DateTime fchEntrega;
                if (ahora.Hour >= 20)
                {
                    fchEntrega = DateTime.Today.AddDays(1);
                }
                else
                {
                    fchEntrega = DateTime.Today;
                }
                
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = int.Parse(Session["IdUsuario"].ToString());
                string horaentrega= "";
                
                if (RadioBtnTarde.Checked)
                {
                    horaentrega = "Entre 14 y 16 p.m.";
                }
                else if (RadioBtnNoche.Checked)
                {
                    horaentrega = "Después de las 20 p.m.";
                }
                List<int> lstdocs = new List<int>();
                List<String> imagenes = new List<String>();
                if (!String.IsNullOrEmpty(txtURLs.Text))
                {
                    imagenes = txtURLs.Text.Split(char.Parse(",")).ToList();
                }
                Pedido p = new Pedido();
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.UserId = user;
                p.HoraEntrega = horaentrega;
                if (RadioBtnSi.Checked)
                {
                    p.esEnvio = true;
                }
                if (!String.IsNullOrEmpty(txtPrecioPedido.Text))
                    p.Precio = decimal.Parse(txtPrecioPedido.Text);
                else
                    p.Precio = 0;
                if (chkPedidoImagen.Checked)
                {
                    foreach (String url in imagenes)
                    {
                        Documento doc = new Documento();
                        doc.AColor = false;
                        doc.Descripcion = Descripcion;
                        doc.Direccion = Direccion;
                        doc.EsDobleFaz = false;
                        doc.esEnvio = true;
                        doc.EsImagen = true;
                        doc.EsPractico = false;
                        doc.estado = "Pendiente";
                        doc.FechaIngreso = DateTime.Now;
                        doc.Formato = "";
                        doc.gradoLiceal = 0;
                        doc.NroPractico = 0;
                        doc.NombreDocumento = "Pedido" + DateTime.Now;
                        doc.ruta = url;
                        doc.UserId = user;
                        int idd = Sistema.GetInstancia().GuardarDocumento(doc);
                        if (idd > 0)
                        {
                            lstdocs.Add(idd);
                        }
                    }

                }
                if (chkPedidoProducto.Checked)
                {
                    if (p.Precio <= 50)
                    {
                        lblInformativo.Text = "El precio del pedio debe ser mayor a cero y mayor al costo de envío.";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        return;
                    }
                }
               int id = Sistema.GetInstancia().GuardarPedido(p,  imagenes, lstdocs);
               if (id > 0)
               {
                    bool exito = false;
                    if (chkPedidoProducto.Checked)
                    {
                        List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();
                        foreach (GridViewRow row in GridViewProductosNuevo.Rows)
                        {
                            int idProd = int.Parse((row.FindControl("lblIdProducto") as Label).Text);

                            ProductoPedidoCantidad ppc = new ProductoPedidoCantidad();
                            ppc.ProductoId = idProd;
                            ppc.IdPedido = int.Parse(Session["IdPedido"].ToString());
                            ppc.Cantidad = int.Parse((row.FindControl("lblCantidad") as Label).Text);

                            lista.Add(ppc);
                            p.productosCantidad.Add(ppc);
                        

                        ActualizarStock(int.Parse(Session["IdPedido"].ToString(), int.Parse((row.FindControl("lblCantidad") as Label).Text));

                        }
                        exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(lista);
                    }
                    if (exito) {
                        lblInformativo.Text = "Se guardo con éxito";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        Session["Tabla"] = null;
                        Session["IdPedido"] = id;
                        Usuario u = Sistema.GetInstancia().BuscarUsuario(user);
                        bool ex = Sistema.GetInstancia().GuardarNotificacionPedido(user, u.UserNombre, "NUEVO");
                        llenarGrillaPedidos();
                        divNuevoPedido.Visible = false;
                        btnCerrar.Enabled = false;
                        btnNuevoPedido.Visible = false;
                        //Elimino campos luego que se inserto con éxito
                        VaciarCampos();
                        lblPrecioproducto.Visible = true;
                        divProductos.Visible = true;
                        GridViewProductosNuevo.Visible = false;
                        btnCerrar.Enabled = true;
                    }
                    else
                    {
                        lblInformativo.Text = "No se guardo (Error)";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
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
            GridViewProductosNuevo.Visible = true;
            if (ListProductos1.SelectedItem != null && (!String.IsNullOrEmpty(txtCantidadProducto.Text)))
            {
                int idProducto = int.Parse(ListProductos1.SelectedValue);
                int cantidad = int.Parse(txtCantidadProducto.Text);
                Producto p = Sistema.GetInstancia().BuscarProducto(idProducto);
                if (Session["Tabla"] != null)
                {
                    DataTable table = Session["Tabla"] as DataTable;
                    DataRow row;
                    row = table.NewRow();
                    row["ProductoId"] = idProducto;
                    row["ProductoNombre"] = p.ProductoNombre;
                    row["Cantidad"] = cantidad;
                    table.Rows.Add(row);
                    Session["PrecioAnterior"] = txtPrecioPedido.Text;

                    Session["Tabla"] = table;

                    GridViewProductosNuevo.DataSource = table;
                    GridViewProductosNuevo.DataBind();
                    
                }
                else
                {
                    DataTable table = new DataTable();
                    DataColumn c1 = new DataColumn();
                    c1.ColumnName = "ProductoId";
                    DataColumn c2 = new DataColumn();
                    c2.ColumnName = "ProductoNombre";
                    DataColumn c3 = new DataColumn();
                    c3.ColumnName = "Cantidad";
                    table.Columns.Add(c1);
                    table.Columns.Add(c2);
                    table.Columns.Add(c3);

                    DataRow row = table.NewRow();
                    row["ProductoId"] = idProducto.ToString();
                    row["ProductoNombre"] = p.ProductoNombre;
                    row["Cantidad"] = cantidad.ToString();
                    table.Rows.Add(row);
                    Session["PrecioAnterior"] = txtPrecioPedido.Text;

                    Session["Tabla"] = table;
                    GridViewProductosNuevo.DataSource = table;
                    GridViewProductosNuevo.DataBind();
                }
                txtCantidadProducto.Text = string.Empty;
            }
            else
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Seleccione un producto e ingrese una cantidad";
            }

            
        }

        protected void lnkNuevoPedido_Click(object sender, EventArgs e)
        {
            DivGridPedido.Visible = false;
            DivEncabezado.Visible = false;
            divNuevoPedido.Visible = true;
            divConatiner.Visible = true;
            btnNuevoPedido.Visible = true;
            btnCerrar.Visible = true;
            GridViewProductosNuevo.Visible = false;



        }
        
        public bool CompleteCampos()
        {
            if (RadioBtnSi.Checked == true)
            {
                if (String.IsNullOrEmpty(txtDireccion.Text) || String.IsNullOrEmpty(txtDescripcion.InnerText))
                {
                    return true;
                }
                else { return false; }
            }
            else if (RadioBtnNo.Checked == false)
            {
                if (String.IsNullOrEmpty(txtDescripcion.InnerText))
                {
                    return true;
                }
                else { return false; }
            }
            else return false;
        }

        public void VaciarCampos()
        {
            txtDireccion.Text = string.Empty;
            txtDescripcion.InnerText = string.Empty;
            txtCantidadProducto.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
            txtPrecioPedido.Text = string.Empty;
            lblPrecioproducto.Text = string.Empty;

        }

        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            TimerMensajes.Enabled = false;
        }

        protected void RadioBtnSi_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnSi.Checked)
            {
                txtDireccion.Enabled = true;
                txtDireccion.Visible = true;
                DateTime ahora = DateTime.Now;
                if (!(ahora.Hour == 1 && ahora.Minute >= 30 && ahora.Hour > 8))
                { 
                    RadioBtnTarde.Visible = true;
                }
                Label3.Visible = true;
                RadioBtnNoche.Visible = true;
                Configuracion conf = Sistema.GetInstancia().BuscarConfiguracion("CostoEnvio");
                decimal precio;
                if (chkPedidoImagen.Checked){
                    precio = decimal.Parse(conf.Valor);
                }
                else
                {
                    if (!String.IsNullOrEmpty(txtPrecioPedido.Text))
                    {
                        precio = decimal.Parse(txtPrecioPedido.Text) + int.Parse(conf.Valor);

                    }
                    else
                    {
                        precio = int.Parse(conf.Valor);
                    }
                }


                txtPrecioPedido.Text = precio.ToString();
            }
        }

        protected void RadioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDireccion.Enabled = false;
            txtDireccion.Text = "";
            txtDireccion.Visible = false;
            RadioBtnTarde.Visible = false;
            Label3.Visible = false;
            RadioBtnNoche.Visible = false;
        }
    
        protected void ListProductos1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListProductos1.SelectedIndex > -1)
            {
                Producto p = Sistema.GetInstancia().BuscarProducto(int.Parse(ListProductos1.SelectedValue));
                lblPrecioproducto.Text =p.ProductoPrecioVenta.ToString();
                txtCantidadProducto.Focus();
            }
        }

        protected void txtCantidadProducto_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCantidadProducto.Text))
            {
                decimal precio = decimal.Parse(lblPrecioproducto.Text);
                int cant = int.Parse(txtCantidadProducto.Text);
                decimal subtotal = precio * cant;
                txtSubtotal.Text = subtotal.ToString();

                decimal total = decimal.Parse(txtSubtotal.Text);
                if (!String.IsNullOrEmpty(txtPrecioPedido.Text))
                {
                    txtPrecioPedido.Text = (decimal.Parse(txtPrecioPedido.Text) + total).ToString();
                }
                else
                {
                    txtPrecioPedido.Text = total.ToString();

                }
                btnAgregarTodo.Enabled = true;
            }
            else btnAgregarTodo.Enabled = false;
        }

       
        protected void llenarGrillaPedidos()
        {
            int id = int.Parse(Session["IdUsuario"].ToString());
            GridViewPedidos.DataSource = Sistema.GetInstancia().ListadoPedidosUsuario(id);
            GridViewPedidos.DataBind();
        }

      

        protected void llenarGrillaProductos(int id)
        {
            List<ProductoPedidoCantidad> ppc = Sistema.GetInstancia().ListadoProductosPedido(id);
            DataTable dt = new DataTable();
            DataColumn c1 = new DataColumn();
            c1.ColumnName = "ProductoId";
            DataColumn c2 = new DataColumn();
            c2.ColumnName = "ProductoNombre";
            DataColumn c3 = new DataColumn();
            c3.ColumnName = "Cantidad";
            dt.Columns.Add(c1);
            dt.Columns.Add(c2);
            dt.Columns.Add(c3);
            foreach (ProductoPedidoCantidad p in ppc) {
                DataRow row1;
                row1 = dt.NewRow();
                row1["ProductoId"] = p.ProductoId;
                Producto pr = Sistema.GetInstancia().BuscarProducto(p.ProductoId);
                row1["ProductoNombre"] = pr.ProductoNombre;
                row1["Cantidad"] = p.Cantidad;
                dt.Rows.Add(row1);
            }

            GridViewProductosNuevo.DataSource = dt;
            GridViewProductosNuevo.DataBind();
            Session["Tabla"] = dt;
        }


      

        protected void GridViewProductos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridViewProductosNuevo.Rows[e.RowIndex];
            int idPedido = int.Parse(Session["IdPedido"].ToString());
            int idProducto = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
            int cantidad = int.Parse((row.FindControl("txtCantidad") as TextBox).Text);
           

            ProductoPedidoCantidad pc = new ProductoPedidoCantidad();
            pc.IdPedido = idPedido;
            pc.ProductoId = idProducto;
            pc.Cantidad = cantidad;
            bool exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(pc);

            lblInformativo.Visible = true;
            lblInformativo.Text = "";
        }
             
        
        protected void GridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedidos.PageIndex = e.NewPageIndex;
            llenarGrillaPedidos();
        }
       
        protected void GridViewPedidos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            divNuevoPedido.Visible = true;
            GridViewProductosNuevo.Visible = true;
            GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("IdPedido") as Label).Text);
            Pedido p = Sistema.GetInstancia().BuscarPedido(Id);
            txtDescripcion.InnerText = p.Descripcion;
            if (p.esEnvio)
            {
                RadioBtnSi.Checked = true;
                txtDireccion.Visible = true;
                txtDireccion.Text = p.Direccion;
                RadioBtnTarde.Visible = true;
                RadioBtnNoche.Visible = true;
            }
            else
            {
                RadioBtnSi.Checked = false;
                RadioBtnNo.Checked = true;
                txtDireccion.Visible = false;
                RadioBtnTarde.Visible = false;
                RadioBtnNoche.Visible = false;
            }
            List<ProductoPedidoCantidad> ppc = Sistema.GetInstancia().ListadoProductosPedidos(Id);
            txtPrecioPedido.Text = p.Precio.ToString();
            DivGridPedido.Visible = false;
            divProductos.Visible = true;
            btnNuevoPedido.Visible = false;
            btnCerrar.Visible = false;
            GridViewProductosNuevo.Visible = true;
            btnActualizar.Visible = true;
            RadioBtnSi.Visible = true;
            RadioBtnNo.Visible = true;
            divConatiner.Visible = true;
            Session["IdPedido"] = Id;
            foreach (ProductoPedidoCantidad pc in ppc)
            {
                ListProductos1.Items.Remove(ListProductos1.Items.FindByValue(pc.ProductoId.ToString()));
            }
            llenarGrillaProductos(Id);




        }

        protected void GridViewPedidos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPedidos.EditIndex = -1;
            llenarGrillaPedidos();
        }
       
        protected void GridViewProductosNuevo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProductosNuevo.EditIndex = -1;
            llenarGrillaProductosNuevo();
        }
        protected void GridViewPedidos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(Session["IdUsuario"].ToString()));
                GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
                int Id = Convert.ToInt32((row.FindControl("IdPedido") as Label).Text);
                ActualizarStockBorrarPedido(Id);
                bool exito = Sistema.GetInstancia().CancelarPedido(Id);
                lblInformativo.Text = "Se elimino con éxito";
                GridViewPedidos.EditIndex = -1;
                llenarGrillaPedidos();
                bool ex = Sistema.GetInstancia().GuardarNotificacionPedido(u.UserId, u.UserNombre, "CANCELACION");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void GridViewProductosNuevo_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewProductosNuevo.Rows[e.RowIndex];
            int idPedido = int.Parse(Session["IdPedido"].ToString());
            int idProducto = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
            int cantidad = int.Parse((row.FindControl("txtCantidad") as TextBox).Text);

            DataTable dt = Session["Tabla"] as DataTable;
            DataRow row1;
            row1 = dt.NewRow();
            row1["ProductoId"] = idProducto;
            row1["ProductoNombre"] = (row.FindControl("lblNombre") as Label).Text;
            row1["Cantidad"] = cantidad;
            dt.Rows.Add(row1);
            dt.Rows[e.RowIndex].Delete();
            Session["Tabla"] = dt;
            decimal precio = 0;
            decimal precioAnterior = 0;
            ProductoPedidoCantidad ppc = Sistema.GetInstancia().BuscarPedidoProductoCantidadPr(idPedido, idProducto);
            Producto pr = Sistema.GetInstancia().BuscarProducto(idProducto);
            Pedido p = Sistema.GetInstancia().BuscarPedido(idPedido);
            
            precioAnterior = p.Precio;
            decimal subtotal = precioAnterior - (pr.ProductoPrecioVenta * ppc.Cantidad);
            precio = subtotal + (pr.ProductoPrecioVenta * cantidad);
           
            txtPrecioPedido.Text = precio.ToString();
            bool exito = Sistema.GetInstancia().EliminarProductoPedidoCant(ppc.IdPedido,ppc.ProductoId,ppc.Cantidad);
            lblInformativo.Visible = true;
            GridViewProductosNuevo.EditIndex = -1;
            GridViewProductosNuevo.DataSource = dt;
            GridViewProductosNuevo.DataBind();
            
        }
        protected void GridViewProductosNuevo_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewProductosNuevo.Rows[e.RowIndex];
                int idPedido = int.Parse(Session["IdPedido"].ToString());
                int idProducto = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
                int cantidad = int.Parse((row.FindControl("lblCantidad") as Label).Text);
                ProductoPedidoCantidad ppc = Sistema.GetInstancia().BuscarPedidoProductoCantidadPr(idPedido, idProducto);
                Producto pr = Sistema.GetInstancia().BuscarProducto(idProducto);
                Pedido p = Sistema.GetInstancia().BuscarPedido(idPedido);

                decimal precioAnterior = p.Precio;
                decimal subtotal = precioAnterior - (pr.ProductoPrecioVenta * ppc.Cantidad);

                txtPrecioPedido.Text = subtotal.ToString();
                DataTable dt = Session["Tabla"] as DataTable;
                dt.Rows[e.RowIndex].Delete();
                Session["Tabla"] = dt;
                lblInformativo.Visible = true;
                bool exito = Sistema.GetInstancia().EliminarProductoPedidoCant(idPedido,idProducto, cantidad);
                if (exito)
                {
                    lblInformativo.Text = "Se elimino con éxito";
                    GridViewProductosNuevo.EditIndex = -1;
                    GridViewProductosNuevo.DataSource = dt;
                    GridViewProductosNuevo.DataBind();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        protected void GridViewProductosNuevo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProductosNuevo.EditIndex = -1;
            llenarGrillaProductosNuevo();
        }
        protected void llenarGrillaProductosNuevo()
        {
            if (Session["Table"] != null)
            {
                DataTable dt = Session["Table"] as DataTable;
                GridViewProductosNuevo.DataSource = dt;
                GridViewProductosNuevo.DataBind();
            }
        }
        protected void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            bool exito = false;
            int id = int.Parse(Session["IdPedido"].ToString());
            Pedido p = Sistema.GetInstancia().BuscarPedido(id);
            p.Precio = decimal.Parse(txtPrecioPedido.Text);

            
            int idP = Sistema.GetInstancia().ModificarPedido(p);
            if (idP != 0)
            {
                lblInformativo.Text = "Se actualizo con éxito";
                llenarGrillaProductosNuevo();
                divNuevoPedido.Visible = false;
                divProductos.Visible = false;
                llenarGrillaPedidos();
                DivGridPedido.Visible = true;
                btnCerrar.Visible = false;
            }

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            bool exito = false;
            int id= int.Parse(Session["IdPedido"].ToString());
            DateTime ahora = DateTime.Now;
            DateTime fchEntrega;
            if (ahora.Hour >= 20)
            {
                fchEntrega = DateTime.Today.AddDays(1);
            }
            else
            {
                fchEntrega = DateTime.Today;
            }
            string Direccion = txtDireccion.Text;
            string horaentrega = "";
            if (RadioBtnTarde.Checked)
            {
                horaentrega = "Entre 14 y 16 p.m.";
            }
            else if (RadioBtnNoche.Checked)
            {
                horaentrega = "Después de las 20 p.m.";
            }
            Pedido p = Sistema.GetInstancia().BuscarPedido(id);
            p.Precio = decimal.Parse(txtPrecioPedido.Text);
            p.Descripcion = txtDescripcion.InnerText;
            p.Direccion = Direccion;
            p.FechaEntrega = fchEntrega;
            
            List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();
            foreach (GridViewRow row in GridViewProductosNuevo.Rows)
            {
                int idProd = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
              
                ProductoPedidoCantidad ppc = new ProductoPedidoCantidad();
                ppc.ProductoId = idProd;
                ppc.IdPedido = id;
                ppc.Cantidad = int.Parse((row.FindControl("lblCantidad") as Label).Text);

                lista.Add(ppc);
                p.productosCantidad.Add(ppc);
            }
            exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(lista);
            if (exito)
            {
                int idP = Sistema.GetInstancia().ModificarPedido(p);
                if (idP != 0)
                {
                    lblInformativo.Text = "Se actualizo con éxito";
                    Usuario u = Sistema.GetInstancia().BuscarUsuario(p.UserId);
                    bool ex = Sistema.GetInstancia().GuardarNotificacionPedido(p.UserId, u.UserNombre,"MODIFICACION");
                    llenarGrillaProductosNuevo();
                    divNuevoPedido.Visible = false;
                    divProductos.Visible = false;
                    llenarGrillaPedidos();
                    DivGridPedido.Visible = true;
                    btnActualizar.Visible = false;
                    divPedidoImagen.Visible = false;


                }
            }

        }

        protected void rdbPedidoProductos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPedidoProducto.Checked)
            {
                ListProductos1.Visible = true;
                txtCantidadProducto.Visible = true;
                btnAgregarTodo.Visible = true;
                GridViewProductosNuevo.Visible = true;
                divPedidoImagen.Visible = false;
                chkPedidoImagen.Checked = false;
            }
        }

        protected void rdbPedidoImagen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPedidoImagen.Checked)
            {
                ListProductos1.Visible = false;
                txtCantidadProducto.Visible = false;
                btnAgregarTodo.Visible = false;
                GridViewProductosNuevo.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                divProductos.Visible = false;
                divPedidoImagen.Visible = true;
                chkPedidoProducto.Checked = false;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            divNuevoPedido.Visible = false;
            divProductos.Visible = false;
            btnNuevoPedido.Visible = false;
            btnCerrar.Visible = false;
            divConatiner.Visible = false;
            //DivEncabezado.Visible = true;
            DivGridPedido.Visible = true;
        }

        protected void CargarImagenes(List<Imagen> imagenes)
        {
            try
            {
                List<System.Web.UI.WebControls.ListItem> archivos = new List<System.Web.UI.WebControls.ListItem>();
                foreach (Imagen i in imagenes)
                {
                    string pathImg = "~/Imagenes/" + i.ImagenURL;
                    archivos.Add(new System.Web.UI.WebControls.ListItem(i.ImagenId.ToString(), pathImg));
                }

                //Recorro la lista de imagenes cargadas
                String[] filePaths = txtURLs.Text.Split(char.Parse(","));
                foreach (string filePath in filePaths)
                {
                    if (!String.IsNullOrEmpty(filePath))
                    {
                        string pathImg = "~/Imagenes/" + filePath;
                        archivos.Add(new System.Web.UI.WebControls.ListItem(filePath, pathImg));
                    }


                }


                GridView1.DataSource = archivos;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                List<Imagen> imagenes = new List<Imagen>();
                if (fuImagenes.HasFile)
                {
                    HttpPostedFile archivo = fuImagenes.PostedFile;
                    if ((archivo != null) && (archivo.ContentLength > 0))
                    {
                        if (EsImagen(archivo) == false)
                        {
                            lblInformativo.Text = "Debe seleccionar una imagen.";
                            return;
                        }
                    }


                    int iFileSize = archivo.ContentLength;

                    // todo bien subo la imagen
                    string NombreArchivo = Path.GetFileNameWithoutExtension(fuImagenes.PostedFile.FileName);

                    //CREO UNA IMAGEN BitMap
                    System.Drawing.Bitmap imagen = new System.Drawing.Bitmap(archivo.InputStream);

                    //Me fijo la orientacion por si necesito rotarla
                    if (Array.IndexOf(imagen.PropertyIdList, 274) > -1)
                    {
                        var orientation = (int)imagen.GetPropertyItem(274).Value[0];
                        switch (orientation)
                        {
                            case 1:
                                // No rotation required.
                                break;
                            case 2:
                                imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                break;
                            case 3:
                                imagen.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 4:
                                imagen.RotateFlip(RotateFlipType.Rotate180FlipX);
                                break;
                            case 5:
                                imagen.RotateFlip(RotateFlipType.Rotate90FlipX);
                                break;
                            case 6:
                                imagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 7:
                                imagen.RotateFlip(RotateFlipType.Rotate270FlipX);
                                break;
                            case 8:
                                imagen.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        // This EXIF data is now invalid and should be removed.
                        imagen.RemovePropertyItem(274);
                    }

                    //Convierto la imagen en un cuadrado sin cambiar la resolucion
                    imagen = (Bitmap)ImagenPad(imagen);

                    //Creo un nombre de archivo con la fecha
                    string nombrearchivo = string.Format("{0}_{1:yyyyMMddHHmmss}.{2}", NombreArchivo, DateTime.Now, "jpg");
                    //Subo la imagen al servidor
                    imagen.Save(Server.MapPath("~/Imagenes/" + nombrearchivo), System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Guardo el nombre de la imagen para guardarlo en la propiedad despues
                    if (String.IsNullOrEmpty(txtURLs.Text))
                    {
                        txtURLs.Text = nombrearchivo;
                    }
                    else
                    {
                        txtURLs.Text = txtURLs.Text + "," + nombrearchivo;
                    }
                    CargarImagenes(imagenes);
                   
                }
            }
            catch (Exception ex)
            {


            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //0 represents first column
                System.Web.UI.WebControls.Image img = e.Row.Cells[0].Controls[0] as System.Web.UI.WebControls.Image;
                img.Attributes.Add("onclick", "window.open('" + img.ImageUrl.Replace("~", "") + "', '_blank')");
            }
        }

        private bool EsImagen(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        public static System.Drawing.Image ImagenPad(System.Drawing.Image originalImage)
        {
            int largestDimension = Math.Max(originalImage.Height, originalImage.Width);
            Size squareSize = new Size(largestDimension, largestDimension);
            Bitmap squareImage = new Bitmap(squareSize.Width, squareSize.Height);
            using (Graphics graphics = Graphics.FromImage(squareImage))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                graphics.DrawImage(originalImage, (squareSize.Width / 2) - (originalImage.Width / 2), (squareSize.Height / 2) - (originalImage.Height / 2), originalImage.Width, originalImage.Height);
            }
            return squareImage;
        }


        protected decimal RecalcularPrecioPedido(int idPedido, int idProducto, int cantidad)
        {
            Pedido p = Sistema.GetInstancia().BuscarPedido(idPedido);
            Producto pr = Sistema.GetInstancia().BuscarProducto(idProducto);
            decimal preciopr = pr.ProductoPrecioVenta * cantidad;
            decimal precio = p.Precio;
            precio = precio - preciopr;
            
            return precio;
        }

        protected bool ActualizarStock(int idProducto, int cantidad)
        {
            bool exito = false;
            Producto p = Sistema.GetInstancia().BuscarProducto(idProducto);
            p.Cantidad = p.Cantidad - cantidad;

            exito = Sistema.GetInstancia().ModificarProducto(p);
            if (exito)
                return true;
            else
                return false;
        }

        protected bool ActualizarStockBorrarProd(int idProd, int cantidad)
        {
            bool exito = false;
            
                Producto p = Sistema.GetInstancia().BuscarProducto(idProd);
                p.Cantidad = p.Cantidad + cantidad;

                exito = Sistema.GetInstancia().ModificarProducto(p);

            
            if (exito)
                return true;
            else
                return false;
        }

        protected bool ActualizarStockBorrarPedido(int idPedido)
        {
            bool exito = false;

           List<ProductoPedidoCantidad> ppc = Sistema.GetInstancia().ListadoProductosPedidos(idPedido);
            foreach (ProductoPedidoCantidad p in ppc)
            {
                Producto pr = Sistema.GetInstancia().BuscarProducto(p.ProductoId);
                pr.Cantidad = pr.Cantidad + p.Cantidad;
                exito = Sistema.GetInstancia().ModificarProducto(pr);
            }
            if (exito)
                return true;
            else
                return false;
        }

        /* Envío de Email utilizado para notificar el ingreso de un pedido */
        protected void EnviarMailNuevoPedido(String mailEmpresa, String mailDestino, Usuario u)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(mailDestino); //Correo destino?
            correo.Subject = "Se ha ingresado un nuevo pedido."; //Asunto
            correo.Body = "E4l usuario: " +u.UserNombre + " ha realizado un nuevo pedido."; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 25; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa, "05296221mg");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);
        }

        /* Envío de Email utilizado para notificar la modificación de un pedido */
        protected void EnviarMailModificarPedido(String mailEmpresa, String mailDestino, Usuario u, Pedido p)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(mailDestino); //Correo destino?
            correo.Subject = "Se ha modificado un pedido."; //Asunto
            correo.Body = "El usuario: " + u.UserNombre + " ha modificado el pedido" + p.Descripcion; //Mensaje del correo
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

        /* Envío de Email utilizado para notificar la modificación de un pedido */
        protected void EnviarMailCancelarPedido(String mailEmpresa, String mailDestino, Usuario u, Pedido p)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(mailDestino); //Correo destino?
            correo.Subject = "Restablecer contraseña."; //Asunto
            correo.Body = "El usuario: " + u.UserNombre + " ha cancelado el pedido" + p.Descripcion; //Mensaje del correo
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


