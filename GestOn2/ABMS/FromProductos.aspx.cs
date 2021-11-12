using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.UI.HtmlControls;
using BibliotecaClases.Clases;
using System.Drawing;
using System.IO;

namespace GestOn2.ABMS
{
    public partial class FromProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
                if (!Page.IsPostBack)
                {
                    ListarProductos();
                    ListarCategorias();
                    ListarMarcas();
                    ListarUnidades();
                    ListarOrden();
                }
            }
        }

        public void ListarProductos() {
            GridViewProductos.DataSource = Sistema.GetInstancia().ListadoProductos();
            GridViewProductos.DataBind();
        }

        //Listo categorias de productos en el ListBox
        public void ListarCategorias() {
            List<CategoriaProducto> lista = Sistema.GetInstancia().ListadoCategorias();
            lstCategorias.Items.Clear();
            lstCategorias.DataSource = lista;
            lstCategorias.DataTextField = "NombreCategoria";
            lstCategorias.DataValueField = "IdCategoria";
            lstCategorias.DataBind();
            ddlCategoriaFiltro.Items.Clear();
            ddlCategoriaFiltro.DataSource = lista;
            ddlCategoriaFiltro.DataTextField = "NombreCategoria";
            ddlCategoriaFiltro.DataValueField = "IdCategoria";
            ddlCategoriaFiltro.DataBind();
        }
        public void ListarMarcas()
        {
            List<Marca> lista = Sistema.GetInstancia().ListadoMarcas(); 
            ddlMarcaProductoFiltro.Items.Clear();
            ddlMarcaProductoFiltro.DataSource = lista;
            ddlMarcaProductoFiltro.DataTextField = "NombreMarca";
            ddlMarcaProductoFiltro.DataValueField = "IdMarca";
            ddlMarcaProductoFiltro.DataBind();
            ddlMarca.Items.Clear();
            ddlMarca.DataSource = lista;
            ddlMarca.DataTextField = "NombreMarca";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();
        }
        protected void ListarOrden()
        {
            List<String> lista = new List<string>();
            lista.Add("A-Z");
            lista.Add("Z-A");
            lista.Add("Fecha Carga");
            lista.Add("Codigo");
            ddlOrden.Items.Clear();
            ddlOrden.DataSource =lista;
            ddlOrden.DataBind();


        }
        protected void ListarUnidades()
        {
            List<String> lista = new List<string>();
            lista.Add("CM");
            lista.Add("UNIDADES");
            lista.Add("M");
            lista.Add("MM");
            ddlUnidadMedida.Items.Clear();
            ddlUnidadMedida.DataSource = lista;
            ddlUnidadMedida.DataBind();


        }
        //Guardo el producto 
        protected void btnGuardar_Click(object sender, EventArgs e)
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
                int cantidad = Int32.Parse(txtCantidad.Text);
                int marca = int.Parse(ddlMarcaProductoFiltro.SelectedValue);
                String nombre = txtNombreProducto.Text;
                decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
                int categoria = int.Parse(lstCategorias.SelectedValue);
                decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                CategoriaProducto cat = Sistema.GetInstancia().BuscarCategorias(categoria);
                Producto p = new Producto();
                p.Cantidad = cantidad;
                p.IdMarca = marca;
                p.ProductoNombre = nombre;
                p.ProductoPrecioCompra = precioCompra;
                p.ProductoPrecioVenta = PrecioVenta;

                bool exito = Sistema.GetInstancia().GuardarProducto(p,categoria);
                if (exito)
                {
                    lblInformativo.Text = "Se guardo con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se inserto con éxito
                    VaciarCampos();

                }
                else {
                    lblInformativo.Text = "No se guardo (Error)" ;
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            ListarProductos();
        }

        //Busco producto a  través de la ID
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
        }

        //Timer usado para mostrar o ocultar mensajes
        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            lblCategoriasMsj.Visible = false;
            TimerMensajes.Enabled = false;
        }

        //Valida si falta poner algun dato (retorna true en caso que falte alguno)
        public bool CompleteCampos() {
            if (String.IsNullOrEmpty(txtCantidad.Text) ||
                
                String.IsNullOrEmpty(txtNombreProducto.Text) ||
                String.IsNullOrEmpty(txtPrecioCompra.Text) ||
                String.IsNullOrEmpty(txtPrecioVenta.Text))
                return true;
            else return false;
        }

        //Vacia los campos de la pantalla excepto el id y los mensajes
        protected void VaciarCampos() {
            txtCantidad.Text = string.Empty;
            ddlMarca.ClearSelection();
            txtNombreProducto.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
        }

        //Muestro el panel de edicion de categoria de productos
        protected void linkNewCategoria_Click(object sender, EventArgs e)
        {
            pnlNuevaCat.Visible = true;
        }

        //Busco categoria de productos a través de la id
        protected void btnBuscarCat_Click(object sender, EventArgs e)
        {
            if (txtIdCat.Text != "")
            {
                int id = int.Parse(txtIdCat.Text);
                CategoriaProducto cat = Sistema.GetInstancia().BuscarCategorias(id);
                if (cat != null)
                {
                    if (cat.Activo == true)
                    {
                        txtNomCat.Text = cat.NombreCategoria.ToString();
                    }
                    else
                    {
                        lblCategoriasMsj.Text = "La categoria fue dada de baja";
                        txtNomCat.Text = "";
                        lblCategoriasMsj.Visible = true;
                        TimerMensajes.Enabled = true;
                    }
                }
                else
                {
                    lblCategoriasMsj.Text = "La categoría buscada no éxiste en el sistema";
                    txtNomCat.Text = "";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblCategoriasMsj.Text = "Debe completar id de la categoría";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        //Guardo categoria de producto
        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.

            if (txtNomCat.Text == "")
            {
                lblCategoriasMsj.Text = "Debe completar todos los campos";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer el insert.
            else
            {
                lblCategoriasMsj.Visible = false;
                lblCategoriasMsj.Text = "";
                
                String nombre = txtNomCat.Text;

                CategoriaProducto cat = new CategoriaProducto();
                cat.NombreCategoria = nombre;
                

                bool existe = Sistema.GetInstancia().GuardarCategoria(cat);
                if (existe)
                {
                    lblCategoriasMsj.Text = "Se guardo con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                    ListarCategorias();

                    //Elimino campos luego que se inserto con éxito
                    txtIdCat.Text = "";
                    txtNomCat.Text = "";
                }
                else
                {
                    lblCategoriasMsj.Text = "No se guardo (Error)";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
        }

        //Elimino la categoria del producto a traves del id 
        protected void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (txtIdCat.Text != "")
            {
                int id = Int32.Parse(txtIdCat.Text);
                bool exito = Sistema.GetInstancia().EliminarCategoria(id);
                if (exito)
                {
                    lblCategoriasMsj.Text = "Se elimino con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                    ListarCategorias();

                    //Elimino campos luego que se modifico con éxito
                    txtIdCat.Text = "";
                    txtNomCat.Text = "";
                }
                else
                {
                    lblCategoriasMsj.Text = "No se pudo eliminar ";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblCategoriasMsj.Text = "Complete id de la categoria a eliminar";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        protected void btnClosePanel_Click(object sender, EventArgs e)
        {
            pnlNuevaCat.Visible = false;
            txtIdCat.Text = "";
            txtNomCat.Text = "";
        }

        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            ListarProductos();
        }

        protected void GridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList ddlCategoria = (DropDownList)e.Row.FindControl("ddlCategoria");
                    ddlCategoria.DataSource = Sistema.GetInstancia().ListadoCategorias();
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();
            }
        }

        protected void GridViewProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProductos.EditIndex = e.NewEditIndex;
            ListarProductos();
        }

        
        protected void GridViewProductos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewProductos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdProducto") as Label).Text);
            string nombre = (row.FindControl("txtNombre") as TextBox).Text;
            //string marca = (row.FindControl("txtMarca") as TextBox).Text;
            string cantidad = (row.FindControl("txtCantidad") as TextBox).Text;
            decimal preciocompra = decimal.Parse((row.FindControl("txtPrecioCompra") as TextBox).Text);
            decimal precioventa = decimal.Parse((row.FindControl("txtoPrecioVenta") as TextBox).Text);
            int IdCategoria = Convert.ToInt32((row.FindControl("ddlCategoria") as DropDownList).SelectedValue);


            lblInformativo.Visible = false;
            lblInformativo.Text = string.Empty;
                Producto p = null;
                p = Sistema.GetInstancia().BuscarProducto(Id);
                p.ProductoNombre = nombre;
               // p.IdMarca = marca;
                p.ProductoPrecioCompra = preciocompra;
                p.ProductoPrecioVenta = precioventa;
                p.IdCategoria = IdCategoria;

                bool exito = Sistema.GetInstancia().ModificarProducto(p);
                if (exito)
                {
                    lblInformativo.Visible = true;
                    lblInformativo.Text = "Se modificó con éxito";
                   GridViewProductos.EditIndex = -1;
                    ListarProductos();
                }
        }

        protected void GridViewProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProductos.EditIndex = -1;
            ListarProductos();
        }

        protected void GridViewProductos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewProductos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdProducto") as Label).Text);
            bool exito = Sistema.GetInstancia().EliminarProducto(Id);
            if (exito)
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Se elimino con éxito";
                GridViewProductos.EditIndex = -1;
                ListarProductos();
            }
        }

        protected void btnBuscarXMarca_Click(object sender, EventArgs e)
        {
            //string marca = txtMarcaProductoFiltro.Text;
            //List<Producto> productos = Sistema.GetInstancia().ListadoProductoMarca(marca);
            //if (productos != null)
            //{
            //    GridViewProductos.DataSource = productos;
            //    GridViewProductos.DataBind();
            //}
        }

        protected void btnBuscarXCategoria_Click(object sender, EventArgs e)
        {

            //string categoria = txtCategoriaFiltro.Text;
            //CategoriaProducto idCat = Sistema.GetInstancia().BuscarIdCategoria(categoria);
            //if (idCat != null)
            //{
            //    int cat = idCat.IdCategoria;
            //    List<Producto> productos = Sistema.GetInstancia().ListadoProductoCategoria(cat);
            //    if (productos != null)
            //    {
            //        GridViewProductos.DataSource = productos;
            //        GridViewProductos.DataBind();
            //    }
            //}
            //else {
            //    List<Producto> productos = null;
            //    GridViewProductos.DataSource = productos;
            //    GridViewProductos.DataBind();
            //}
        }

        protected void RadioBtnMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnMarca.Checked)
            {
                DivFiltroXCategoria.Visible = false;
                DivFiltroXMarca.Visible = true;
            }
            else
            {
                DivFiltroXMarca.Visible = false;
                DivFiltroXCategoria.Visible = true;
            }
            
        }
        protected void RadioBtnCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnCategoria.Checked)
            {
                DivFiltroXCategoria.Visible = true ;
                DivFiltroXMarca.Visible = false;
            }
            else
            {
                DivFiltroXMarca.Visible = true;
                DivFiltroXCategoria.Visible = false ;
            }
        }
        protected void RadioBtnAmbos_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnAmbos.Checked)
            {
                DivFiltroXCategoria.Visible = true;
                DivFiltroXMarca.Visible = true;
            }
            else if (RadioBtnMarca.Checked)
            {
                DivFiltroXCategoria.Visible = false;
                DivFiltroXMarca.Visible = true;
            }
            else if (RadioBtnCategoria.Checked)
            {
                DivFiltroXCategoria.Visible = true;
                DivFiltroXMarca.Visible = false;
            }
            

        }
        protected void ddlSeleccionaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        { }
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
                    btnGuardar.Enabled = true;
                }
            }
            catch (Exception ex)
            {


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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //0 represents first column
                System.Web.UI.WebControls.Image img = e.Row.Cells[0].Controls[0] as System.Web.UI.WebControls.Image;
                img.Attributes.Add("onclick", "window.open('" + img.ImageUrl.Replace("~", "") + "', '_blank')");
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            DivFiltros.Visible = false;
            GVProductos.Visible = false;
            DivNuevoProducto.Visible = true;
        }

        protected void btnListadoProductos_Click(object sender, EventArgs e)
        {
            DivNuevoProducto.Visible = false;
            DivFiltros.Visible = true;
            GVProductos.Visible = true;
        }
    }
}