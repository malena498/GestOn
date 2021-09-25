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
using System.Drawing.Printing;
using System.Diagnostics;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace GestOn2.ABMS
{
    public partial class FormOferta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFchDesde.Text = "2021-10-02";
                txtFchHasta.Text = "2021-11-02";
                txtFechaDesde.Text = "2019-11-02";
                txtFechaHasta.Text = "2021-11-02";
                /* AL INICIAR POR PRIMERA VEZ LA PAGINA, CARGA EL LISTADO DE OFERTAS ACTIVAS*/
                GridViewOferta.DataSource = Sistema.GetInstancia().ListadoOfertas();
                GridViewOferta.DataBind();
                btnGuardar.Enabled = false;
            }

        }

        /* Guarda una nueva oferta*/
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fch1 = DateTime.Parse(txtFechaDesde.Text);
                DateTime fch2 = DateTime.Parse(txtFechaHasta.Text);
                DateTime fchHoy = DateTime.Today;

                if (CamposCompletos())
                {
                    if (fch1 < fchHoy)
                    {
                        DivMensajeFormulario.Visible = true;
                        lblResultado.Text = "Fecha inicio no puede ser menor a la de hoy";
                    }
                    else {
                        if (fch1 <= fch2) {
                            List<String> imagenes = new List<String>();
                            if (!String.IsNullOrEmpty(txtURLs.Text))
                            {
                                imagenes = txtURLs.Text.Split(char.Parse(",")).ToList();
                                string user = Session["IdUsuario"].ToString();
                                Oferta o = new Oferta();
                                o.OfertaTitulo = txtTituloOferta.Text;
                                o.OfertaFechaDesde = DateTime.Parse(txtFechaDesde.Text);
                                o.OfertaFechaHasta = DateTime.Parse(txtFechaHasta.Text);
                                o.OfertaDescripcion = txtDescripcionOferta.Text;
                                o.OfertaPrecio = decimal.Parse(txtPrecio.Text);
                                o.UserId = int.Parse(user);
                                bool exito = Sistema.GetInstancia().GuardarOferta(o, imagenes);
                                if (exito)
                                {
                                    
                                    List<Usuario> u = Sistema.GetInstancia().ListadoUsuariosOfertas();
                                    Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                                   
                                    bool mail = EnviarMail(c.Valor, u, imagenes, o);
                                    if (mail)
                                    {
                                        lblResultado.Visible = false;
                                        lblResultado.Text = "EXITO";
                                    }
                                    else
                                    {
                                        lblResultado.Visible = false;
                                        lblResultado.Text = "ERROR";
                                    }
                                    DivMensajeFormulario.Visible = true;
                                    lblResultado.Text = "Oferta ingresada con éxito";
                                    llenarGrilla();
                                    limpiar();
                                }
                            }
                            else
                            {
                                DivMensajeFormulario.Visible = true;
                                lblResultado.Text = "Debe seleccionar una imágen";
                            }
                        }
                        else
                        {
                            DivMensajeFormulario.Visible = true;
                            lblResultado.Text = "Fecha inválida corrobore";
                        }
                    }
                }
                else
                {
                    DivMensajeFormulario.Visible = true;
                    lblResultado.Text = "Debe completar todos los campos";
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /* Busca ofertas a través de filtros, trayendo todas las que se encuentren activas*/
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtFchDesde.Text.Equals(""))
                txtFechaDesde.Text = null;
            if (txtFchHasta.Text.Equals(""))
                txtFchHasta.Text = null;
            DateTime fechaDesde = DateTime.Parse(txtFchDesde.Text);
            DateTime fechaHasta = DateTime.Parse(txtFchHasta.Text);
            string titulo;
            if (String.IsNullOrEmpty(txtTituloOferta.Text))
                titulo = "";
            else
                titulo = txtTituloOferta.Text;
            List<Oferta> ofertas = Sistema.GetInstancia().BuscarOfertaFiltros(fechaDesde, fechaHasta, titulo);
            if (ofertas != null)
            {
                GridViewOferta.DataSource = ofertas;
                GridViewOferta.DataBind();
            }
            else
            {
                lblResultadoBusqueda.Text = "La oferta buscada no éxiste en el sistema";
                lblResultado.Visible = true;
            }
        }

        /* Aca va todo lo referido a la grilla, en la cuál se pueden observar las ofertas asi como modificar u eliminar la que se desee*/
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //0 represents first column
                System.Web.UI.WebControls.Image img = e.Row.Cells[0].Controls[0] as System.Web.UI.WebControls.Image;
                img.Attributes.Add("onclick", "window.open('" + img.ImageUrl.Replace("~", "") + "', '_blank')");
            }
        }

        protected void GridViewOferta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
       (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

            
            }
        }

        protected void GridViewOferta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewOferta.EditIndex = e.NewEditIndex;
            llenarGrilla();
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

            if (Id == 0 || titulo.Equals("") || fechadesde.Equals("") || fechahasta.Equals("") 
                || descripcion.Equals("") || precio == 0)
            {
                lblResultadoGrilla.Visible = true;
                lblResultadoGrilla.Text = "Debe completar todos los campos";
            }
            else {
                DateTime fchhoy = DateTime.Today;
                if (fechadesde < fchhoy)
                {
                    lblResultadoGrilla.Visible = true;
                    lblResultadoGrilla.Text = "Fecha inicio no puede ser menor a la de hoy";
                }
                else
                {
                    if (fechadesde > fechahasta)
                    {
                        lblResultadoGrilla.Visible = true;
                        lblResultadoGrilla.Text = "Fecha inválida corrobore";
                    }
                    else
                    {
                        Oferta of = null;
                        of = Sistema.GetInstancia().BuscarOferta(Id);
                        of.OfertaTitulo = titulo;
                        of.OfertaDescripcion = descripcion;
                        of.OfertaFechaDesde = fechadesde;
                        of.OfertaFechaHasta = fechahasta;
                        of.OfertaPrecio = precio;
                        of.Activo = activo;
                        List<String> imagenes = new List<string>();

                        bool exito = Sistema.GetInstancia().ModificarOferta(of, imagenes);
                        if (exito)
                        {
                            lblResultadoGrilla.Visible = true;
                            lblResultadoGrilla.Text = "Oferta modificada con éxito";
                            GridViewOferta.EditIndex = -1;
                            llenarGrilla();
                        }
                    }
                }
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
            bool exito = Sistema.GetInstancia().EliminarOferta(Id);
            if (exito)
            {
                lblResultadoGrilla.Visible = true;
                lblResultadoGrilla.Text = "Se elimino con éxito";
                GridViewOferta.EditIndex = -1;
                llenarGrilla();
            }
        }

        protected void GridViewOferta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewOferta.PageIndex = e.NewPageIndex;
            llenarGrilla();
        }

        /* Permite cambiar de página en la grilla, siempre que existan registros para mostrar en la siguiente pagina*/
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridViewOferta.PageIndex = e.NewPageIndex;
            this.llenarGrilla();
        }

        /* LLena la grilla con todas las ofertas activas*/
        protected void llenarGrilla()
        {
            GridViewOferta.DataSource = Sistema.GetInstancia().ListadoOfertas();
            GridViewOferta.DataBind();
        }

        /*Limpia todos los campos del formulario dónde se agregan las ofertas*/
        protected void limpiar()
        {
            txtIdOferta.Text = string.Empty;
            txtTituloOferta.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            txtDescripcionOferta.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            btnGuardar.Enabled = false;
        }

        /* Válida que todos los campos del formulario estén completos, retornando true en caso que lo estén*/
        protected bool CamposCompletos()
        {
            if (String.IsNullOrEmpty(txtTituloOferta.Text) ||
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


        /* SE HACE LA GESTIÓN DE LAS IMAGENES TANTO SUBIRLA COMO MOSTRARLA EN EL FORMULARIO*/

        /* Carga la imagen que el usuario seleccionó para la oferta.*/
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

        /* Luego que se selecciona la imagen acá se confirma y se carga para posteriormente ingresarla en dicha oferta.*/
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

        /* FIN DE LA GESTIÓN DE IMAGENES ****************************************************/

        /* Muestra u oculta el campo del filtro que el usuario desee para filtrar la grilla*/
        protected void ddlSeleccionaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("Fechas"))
            {
                DivFiltroXTitulo.Visible = false;
                DivFiltroXFechas.Visible = true;
            }
            else
            {
                DivFiltroXFechas.Visible = false;
                DivFiltroXTitulo.Visible = true;
            }
        }

        /* Muestra el formulario para ingresar una nueva oferta, ocultando la grilla y los filtros de la misma*/
        protected void lnkNuevaOferta_Click(object sender, EventArgs e)
        {
            div1.Visible = false;
            DivFiltros.Visible = false;
            DivNewOferta.Visible = true;
        }

        /* Muestra la grilla y los filtros a aplicarle, ocultando el formulario de ingreso de nueva oferta*/
        protected void btnVerListado_Click(object sender, EventArgs e)
        {
            DivNewOferta.Visible = false;
            DivFiltros.Visible = true;
            div1.Visible = true;
        }

        /* Envío de Email utilizado para notificar el ingreso de una oferta. */
        protected bool EnviarMail(String mailEmpresa, List<Usuario> usuariosDestino, List<string> archivos, Oferta o)
        {
            try
            {
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("Contraseñamail");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);
                if (usuariosDestino != null)
                {
                    //agregado de archivo
                    foreach (Usuario u in usuariosDestino)
                    {//Correo de salida
                        mail.To.Add(u.UserEmail); //Correo destino?
                    }
                }
                mail.IsBodyHtml = true;
                mail.Subject = "¡La oferta " + o.OfertaTitulo + " está disponible!"; //Asunto
                if (archivos != null)
                {
                    //agregado de archivo
                    foreach (string archivo in archivos)
                    {
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        if (System.IO.File.Exists(@archivo))
                            mail.Attachments.Add(new Attachment(@archivo));
                    }
                }
                mail.Body = "Oferta valida desde el día " + o.OfertaFechaDesde.ToShortDateString() + " hasta el día " + o.OfertaFechaHasta.ToShortDateString() + ". <br> Por más información y más ofertas ingrese a: https://bertinatpapeleria.com/ <br> Bertinat Papeleria."; //Mensaje del correomas ofertas en BertinatPapelería
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 25; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa, c.Valor);//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
    }
}