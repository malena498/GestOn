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


                    ListProductos1.DataSource = Sistema.GetInstancia().ListadoProductos();
                    ListProductos1.DataTextField = "ProductoNombre";
                    ListProductos1.DataValueField = "ProductoId";
                    ListProductos1.DataBind();
                    Session["Tabla"] = null;
                    llenarGrillaPedidos();
                    divPedidoImagen.Visible = false;
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
                DateTime fchEntrega = DateTime.Today;
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = int.Parse(Session["IdUsuario"].ToString());

                List<int> lstitems = new List<int>();
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
                p.Precio = decimal.Parse(txtPrecioPedido.Text);
                DataTable dt = new DataTable();
                if (rdbPedidoProductos.Checked) { 
                   dt = Session["Tabla"] as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int idProd = int.Parse(dt.Rows[i]["IdProducto"].ToString());
                        lstitems.Add(idProd);
                    }
                }
                int id = Sistema.GetInstancia().GuardarPedido(p, lstitems, imagenes);
                if (id > 0)
                {
                    List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProductoPedidoCantidad productoPedidoCantidad = new ProductoPedidoCantidad();

                        productoPedidoCantidad.ProductoId = int.Parse(dt.Rows[i]["IdProducto"].ToString());
                        productoPedidoCantidad.Cantidad = int.Parse(dt.Rows[i]["cantidad"].ToString());
                        productoPedidoCantidad.IdPedido = id;


                        lista.Add(productoPedidoCantidad);
                    }
                    bool exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(lista);
                    if (exito)
                    {
                        lblInformativo.Text = "Se guardo con éxito";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        Session["Tabla"] = null;

                        llenarGrillaPedidos();
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
            if (ListProductos1.SelectedItem != null)
            {

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

                decimal precioProductos = CalcularPrecioProductos(p.ProductoPrecioVenta, cantidad);
                decimal precio = 0;
                if (Session["precioTotal"] != null) {
                    precio = decimal.Parse(Session["precioTotal"].ToString());
                }
                precio = precio + precioProductos;
                txtPrecioPedido.Text = precio.ToString();
                Session["precioTotal"] = precioProductos;
            }
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
                Configuracion conf = Sistema.GetInstancia().BuscarConfiguracion("CostoEnvio");
                decimal  precio = decimal.Parse(txtPrecioPedido.Text) + int.Parse(conf.Valor);

                txtPrecioPedido.Text = precio.ToString();
            }
        }

        protected void RadioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDireccion.Enabled = false;
            txtDireccion.Text = "";
            txtDireccion.Visible = false;
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
                DateTime fchEntrega = DateTime.Today;
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;

                List<int> lstitems = new List<int>();

                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
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

        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedidos.PageIndex = e.NewPageIndex;
            llenarGrillaPedidos();
        }

        protected void rdbPedidoProductos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPedidoProductos.Checked)
            {
                ListProductos1.Visible = true;
                txtCantidadProducto.Visible = true;
                btnAgregarTodo.Visible = true;
                GridViewProductos.Visible = true;
                divPedidoImagen.Visible = false;


            }
        }
        protected void rdbPedidoImagen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPedidoImagen.Checked)
            {
                ListProductos1.Visible = false;
                txtCantidadProducto.Visible = false;
                btnAgregarTodo.Visible = false;
                GridViewProductos.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                divPedidoImagen.Visible = true;
            }
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

        protected decimal CalcularPrecioProductos(decimal precio, int cantidad)
        {
            decimal precioTotal = 0;
            precioTotal = precio * cantidad;

           
            return precioTotal;
        }
    }

    
}

/* protected void btnBuscar_Click(object sender, EventArgs e)
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
 }*/
