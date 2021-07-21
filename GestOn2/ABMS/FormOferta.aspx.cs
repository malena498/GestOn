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

namespace GestOn2.ABMS
{
    public partial class FormOferta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewOferta.DataSource = Sistema.GetInstancia().ListadoOfertas();
                GridViewOferta.DataBind();

                divNuevaOferta.Visible = false;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CamposCompletos())
                {
                    Oferta o = new Oferta();
                    String nameDB = "GestOn";
                    o.OfertaTitulo = txtTituloOferta.Text;
                    o.OfertaFechaDesde = DateTime.Parse(txtFechaDesde.Text);
                    o.OfertaFechaHasta = DateTime.Parse(txtFechaHasta.Text);
                    o.OfertaDescripcion = txtDescripcionOferta.Text;
                    o.OfertaPrecio = decimal.Parse(txtPrecio.Text);
                    List<String> imagenes = new List<String>();
                    if (!String.IsNullOrEmpty(txtURLs.Text))
                    {
                        imagenes = txtURLs.Text.Split(char.Parse(",")).ToList();
                    }
                    bool exito = Sistema.GetInstancia().GuardarOferta(o, imagenes, nameDB);
                    if (exito)
                    {
                        lblResultado.Text = "Se guardo con éxito";
                        limpiar();
                    }
                }
                else
                {
                    lblResultado.Text = "Complete los campos";
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "No se logro guardar";
            }
        }

        protected void btnEiminar_Click(object sender, EventArgs e)
        {
            if (txtIdOferta.Text != "")
            {
                int id = Int32.Parse(txtIdOferta.Text);
                bool exito = Sistema.GetInstancia().EliminarOferta(id);
                if (exito)
                {
                    lblResultado.Text = "Se elimino con éxito";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    limpiar();
                }
                else
                {
                    lblResultado.Text = "No se pudo eliminar ";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblResultado.Text = "Complete id de la oferta que desea eliminar";
                lblResultado.Visible = true;
                //TimerMensajes.Enabled = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            if (CamposCompletos())
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos";
                //TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer la modificación.
            else
            {
                lblResultado.Visible = false;
                lblResultado.Text = "";
                Oferta of = null;
                of = Sistema.GetInstancia().BuscarOferta(int.Parse(txtIdOferta.Text));
                of.OfertaTitulo = txtTituloOferta.Text;
                of.OfertaDescripcion = txtDescripcionOferta.Text;
                of.OfertaFechaDesde = DateTime.Parse(txtFechaDesde.Text);
                of.OfertaFechaHasta = DateTime.Parse(txtFechaHasta.Text);
                of.OfertaPrecio = decimal.Parse(txtPrecio.Text);
                List<String> imagenes = new List<String>();
                if (!String.IsNullOrEmpty(txtURLs.Text))
                {
                    imagenes = txtURLs.Text.Split(char.Parse(",")).ToList();
                }
                bool exito = Sistema.GetInstancia().ModificarOferta(of, imagenes);
                if (exito)
                {
                    lblResultado.Text = "Se modificó con éxito";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    limpiar();
                }
                else
                {
                    lblResultado.Text = "No se guardo (Error)";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = DateTime.Parse(txtFchDesde.Text);
            DateTime fechaHasta = DateTime.Parse(txtFchHasta.Text);
            string titulo = txtTituloOferta.Text;
            List<Oferta> ofertas = Sistema.GetInstancia().BuscarOfertaFiltros(fechaDesde, fechaHasta, titulo);
            if (ofertas != null)
            {

                GridViewOferta.DataSource = ofertas;
                GridViewOferta.DataBind();
            }
            
                else
                {
                    lblResultado.Text = "La oferta buscada no éxiste en el sistema";
                    lblResultado.Visible = true;
                    //TimerMensajes.Enabled = true;
                }
           
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            divNuevaOferta.Visible = true;
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

                List<Imagen> imagenes= new List<Imagen>();
                if (fuImagenes.HasFile)
                {
                    HttpPostedFile archivo = fuImagenes.PostedFile;
                    if ((archivo != null) && (archivo.ContentLength > 0))
                    {
                        if (EsImagen(archivo) == false)
                        {
                            lblResultado.Text = "Debe seleccionar una imagen.";
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

        protected void GridViewOferta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    

                    GridViewRow row = GridViewOferta.Rows[e.Row.RowIndex];
                    LinkButton btnEditar = row.FindControl("btnEditar") as LinkButton;
                    btnEditar.Visible = false;
                    LinkButton btnBorrar = row.FindControl("btnBorrar") as LinkButton;
                    btnBorrar.Visible = false;
                    LinkButton btnCancelar = row.FindControl("btnCancelar") as LinkButton;
                    btnCancelar.Visible = true;
                    LinkButton btnUpdate = row.FindControl("btnUpdate") as LinkButton;
                    btnUpdate.Visible = true;
                }


            }
        }

        protected void GridViewOferta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewOferta.EditIndex = e.NewEditIndex;
            llenarGrilla();
        }


        protected void llenarGrilla()
        {
            GridViewOferta.DataSource = Sistema.GetInstancia().ListadoOfertas();
            GridViewOferta.DataBind();
        }

        protected void GridViewOferta_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewOferta.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdOferta") as Label).Text);
            string titulo = (row.FindControl("txtTitulo") as TextBox).Text;
            DateTime fechadesde = DateTime.Parse((row.FindControl("txtFechaDesde") as TextBox).Text);
            DateTime fechahasta = DateTime.Parse((row.FindControl("txtFechaHasta") as TextBox).Text);
            string descripcion = (row.FindControl("txtDescripcion") as TextBox).Text;
            bool activo = Convert.ToBoolean((row.FindControl("chkActivo1") as CheckBox).Checked);
            decimal precio = decimal.Parse((row.FindControl("txtPrecio") as TextBox).Text);
           
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
            Oferta of = null;
            of = Sistema.GetInstancia().BuscarOferta(Id);
            of.OfertaTitulo = titulo;
            of.OfertaDescripcion = txtDescripcionOferta.Text;
            of.OfertaFechaDesde = fechadesde;
            of.OfertaFechaHasta = fechahasta;
            of.OfertaPrecio = precio;
            of.Activo = activo;
            List<String> imagenes = new List<string>(); 

            bool exito = Sistema.GetInstancia().ModificarOferta(of, imagenes);
            if (exito)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Se modificó con éxito";
                GridViewOferta.EditIndex = -1;
                llenarGrilla();
            }
            

            
        }

        protected void GridViewOferta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewOferta.EditIndex = -1;
            llenarGrilla();
        }

        protected void GridViewOferta_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewOferta.Rows[e.RowIndex];
            int Id = Convert.ToInt32(GridViewOferta.DataKeys[e.RowIndex].Values[0]);
            bool exito = Sistema.GetInstancia().EliminarUsuario(Id);
            if (exito)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Se elimino con éxito";
                GridViewOferta.EditIndex = -1;
                llenarGrilla();
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridViewOferta.PageIndex = e.NewPageIndex;
            this.llenarGrilla();
        }

        protected void limpiar()
        {
            txtIdOferta.Text = string.Empty;
            txtTituloOferta.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            txtDescripcionOferta.Text = string.Empty;
            txtPrecio.Text = string.Empty;
        }

       

        protected bool CamposCompletos()
        {
            if (String.IsNullOrEmpty(txtIdOferta.Text) || String.IsNullOrEmpty(txtTituloOferta.Text) ||
                String.IsNullOrEmpty(txtFechaHasta.Text) || String.IsNullOrEmpty(txtFechaDesde.Text) ||
                String.IsNullOrEmpty(txtDescripcionOferta.Text) || String.IsNullOrEmpty(txtPrecio.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}