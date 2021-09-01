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
                    Session["Tabla"] = null;
                    llenarGrillaPedidos();
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
                DataTable dt = new DataTable();
                if (chkPedidoProducto.Checked)
                {
                    
                }
               int id = Sistema.GetInstancia().GuardarPedido(p,  imagenes, lstdocs);
               if (id > 0)
               {
                        lblInformativo.Text = "Se guardo con éxito";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        Session["IdPedido"] = id;
                        llenarGrillaPedidos();
                        divProductos.Visible = true;
                        divNuevoPedido.Visible = false;
                        btnAgregarProductos.Visible = true;
                        btnCerrar.Enabled = false;
                        btnNuevoPedido.Visible = false;
                        //Elimino campos luego que se inserto con éxito
                        VaciarCampos();
                        


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
            if (ListProductos1.SelectedItem != null && (!String.IsNullOrEmpty(txtCantidadProducto.Text)))
            {
                int idPedido = int.Parse(Session["IdPedido"].ToString());
                int idProducto = int.Parse(ListProductos1.SelectedValue);
                int cantidad = int.Parse(txtCantidadProducto.Text);

                ProductoPedidoCantidad ppc = new ProductoPedidoCantidad();
                Producto p = Sistema.GetInstancia().BuscarProducto(idProducto);
                ppc.ProductoId = idProducto;
                ppc.IdPedido = idPedido;
                ppc.Cantidad = cantidad;
                ActualizarStock(idProducto, cantidad);
                bool exito = Sistema.GetInstancia().GuardarProductoPedidoCantidad(ppc);
                if (exito)
                {
                    txtCantidadProducto.Text = string.Empty;
                    decimal precioProductos = CalcularPrecioProductos(p.ProductoPrecioVenta, cantidad);
                    decimal precio = 0;

                    if (!String.IsNullOrEmpty(txtPrecioPedido.Text))
                    {
                        precio = decimal.Parse(txtPrecioPedido.Text);
                    }
                    precio = precio + precioProductos;
                    txtPrecioPedido.Text = precio.ToString();
                    Session["precioTotal"] = precioProductos;
                    Session["PrecioAnterior"] = txtPrecioPedido.Text;
                    llenarGrillaProductosBase(idPedido);

                }
                else
                {
                    lblInformativo.Visible = true;
                    lblInformativo.Text = "Seleccione un producto e ingrese una cantidad";
                }
                
            }
        }
        protected void lnkNuevoPedido_Click(object sender, EventArgs e)
        {
            DivGridPedido.Visible = false;
            divNuevoPedido.Visible = true;
            divProductos.Visible = false;
            btnNuevoPedido.Visible = true;
            btnCerrar.Visible = true;
            
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
        protected void llenarGrillaProductosBase(int idPedido)
        {
            GridViewProductos.DataSource = Sistema.GetInstancia().ListadoProductosPedidos(idPedido);
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
            lblInformativo.Text = "";
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
                
                int idProducto= int.Parse((row.FindControl("lblIdProducto") as Label).Text);
                int idPedido = int.Parse((row.FindControl("lblIdPedido") as Label).Text);
                int cantidad = int.Parse((row.FindControl("lblCantidad") as Label).Text);

                ActualizarStockBorrarProd(idProducto, cantidad);
                bool exito = Sistema.GetInstancia().EliminarProductoPedidoCant(idPedido, idProducto, cantidad);
                if (exito)
                {
                    lblInformativo.Visible = true;
                    lblInformativo.Text = "Se elimino con éxito";
                    GridViewProductos.EditIndex = -1;
                    llenarGrillaProductosBase(idPedido);
                    decimal precio = RecalcularPrecioPedido(idPedido);
                    txtPrecioPedido.Text = precio.ToString();
                }
            }
            catch (Exception ex)
            {
                return;
            }
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
            GridViewRow row = GridViewPedidos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("IdPedido") as Label).Text);
            Pedido p = Sistema.GetInstancia().BuscarPedido(Id);
            txtPrecioPedido.Text = p.Precio.ToString();
            DivGridPedido.Visible = false;
            divProductos.Visible = true;
            btnNuevoPedido.Visible = false;
            btnCerrar.Visible = false;
            btnActualizar.Visible = true;
            btnAgregarProductos.Visible = false;
            RadioBtnSi.Visible = true;
            RadioBtnNo.Visible = true;
            Session["IdPedido"] = Id;
            llenarGrillaProductosBase(Id);

            
            
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
                int Id = Convert.ToInt32((row.FindControl("IdPedido") as Label).Text);
                ActualizarStockBorrarPedido(Id);
                bool exito = Sistema.GetInstancia().CancelarPedido(Id);
                lblInformativo.Text = "Se elimino con éxito";
                GridViewPedidos.EditIndex = -1;
                llenarGrillaPedidos();

            }
            catch (Exception ex)
            {
                return;
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
                llenarGrillaProductosBase(idP);
                divNuevoPedido.Visible = false;
                divProductos.Visible = false;
                llenarGrillaPedidos();
                DivGridPedido.Visible = true;
                btnAgregarProductos.Visible = false;
                btnCerrar.Visible = false;
            }

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            bool exito = false;
            int id= int.Parse(Session["IdPedido"].ToString());
            Pedido p = Sistema.GetInstancia().BuscarPedido(id);
            p.Precio = decimal.Parse(txtPrecioPedido.Text);
            
            List<ProductoPedidoCantidad> lista = new List<ProductoPedidoCantidad>();
            foreach (GridViewRow row in GridViewProductos.Rows)
            {
                int idProd = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
              
                ProductoPedidoCantidad ppc = new ProductoPedidoCantidad();
                ppc.ProductoId = idProd;
                ppc.IdPedido = int.Parse((row.FindControl("lblIdPedido") as Label).Text);
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
                    llenarGrillaProductosBase(idP);
                    divNuevoPedido.Visible = false;
                    divProductos.Visible = false;
                    llenarGrillaPedidos();
                    DivGridPedido.Visible = true;
                    
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
                GridViewProductos.Visible = true;
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
                GridViewProductos.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                divPedidoImagen.Visible = true;
                chkPedidoProducto.Checked = false;
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            divNuevoPedido.Visible = false;
            divProductos.Visible = false;
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

        protected decimal CalcularPrecioProductos(decimal precio, int cantidad)
        {
            decimal precioTotal = 0;
            precioTotal = precio * cantidad;

           
            return precioTotal;
        }
    

    protected decimal RecalcularPrecioPedido(int idPedido)
    {
            Pedido p = Sistema.GetInstancia().BuscarPedido(idPedido);
            decimal precioa = 0;
            decimal precio = p.Precio;
            foreach (GridViewRow row in GridViewProductos.Rows)
            {

                int idProducto = int.Parse((row.FindControl("lblIdProducto") as Label).Text);
                int cantidad = int.Parse((row.FindControl("lblCantidad") as Label).Text);
                ProductoPedidoCantidad ppc = Sistema.GetInstancia().BuscarPedidoProductoCantidad(idPedido,idProducto, cantidad);
                precioa = precioa + ppc.producto.ProductoPrecioVenta * ppc.Cantidad;
                precio = precio + precioa;
            }
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


