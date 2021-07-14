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
using System.Diagnostics;

namespace GestOn2.ABMS
{
    public partial class FormDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Documento doc = new Documento();
                doc.IdDocumento = 1;
                doc.NombreDocumento = txtNombre.Text;
                doc.Formato = txtFormato.Text;
                doc.Descripcion = txtDescripcion.Text;
                doc.Tipo = txtTipo.Text;
                doc.gradoLiceal = txtGradoLiceal.Text;
                doc.esEnvio = false;
                doc.EsPractico = false;
                if (chkEsEnvio.Checked)
                {
                    doc.esEnvio = true;
                }
                if (chkEsPractico.Checked)
                {
                    doc.EsPractico = true;
                }
                if (chkDobleFaz.Checked)
                {
                    doc.EsDobleFaz = true;
                }
                else
                {
                    doc.EsDobleFaz = false;

                }
                if (chkColor.Checked)
                {
                    doc.AColor = true;
                }
                else
                {
                    doc.AColor = false;
                }
                String ruta = GuardarArchivo();
                if (ruta.Equals("Error"))
                {
                    //aca va el mensajito
                    return; 
                }
                else
                {
                    doc.ruta = ruta;
                }
                doc.UserId = 1;
                bool exito = Sistema.GetInstancia().GuardarDocumento(doc);
                if (exito)
                    txtDescripcion.Text = "";
                else txtDescripcion.Text = "No anduvo petes";

            }
            catch (Exception ex)
            {

            }
        }

//        protected String GuardarArchivo()
//        {
//            

//                return filePath;
//            }
//                else
//                {
//                Aca va el mensajito
//                    return "Error";
//            }

//            return filePath;
//        }
//            catch(Exception ex)
//            {
//                return "Error";

//            }
//}

//private void SendToPrinter()
//{
//    Documento doc = new Documento();
//    ProcessStartInfo info = new ProcessStartInfo();
//    info.Verb = "print";                          // Seleccionar el programa para imprimir PDF por defecto
//    info.FileName = @"C:\Firmador\1.pdf";         // Ruta hacia el fichero que quieres imprimir
//    info.CreateNoWindow = true;                   // Hacerlo sin mostrar ventana
//    info.WindowStyle = ProcessWindowStyle.Hidden; // Y de forma oculta

//    Process p = new Process();
//    p.StartInfo = info;
//    p.Start();  // Lanza el proceso

//    p.WaitForInputIdle();
//    System.Threading.Thread.Sleep(3000);          // Espera 3 segundos
//    if (false == p.CloseMainWindow())
//        p.Kill();                                  // Y cierra el programa de imprimir PDF's
//}

private bool EsDoc(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsPDF(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsExcel(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsImagen(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }

        protected void chkEsEnvio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsEnvio.Checked)
                txtDireccion.Visible = true;
            else
            {
                txtDireccion.Text = string.Empty;
                txtDireccion.Visible = false;
            }
        }

        protected void chkEsPractico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPractico.Checked)
                txtNroPractico.Visible = true;
            else
            {
                txtNroPractico.Text = string.Empty;
                txtNroPractico.Visible = false;
            }
        }

        protected String GuardarArchivo()
        {
            try
            {
                string filePath;
                if (fuDocs.HasFile)
                {
                    HttpPostedFile archivo = fuDocs.PostedFile;
                    string NombreArchivo = Path.GetFileName(fuDocs.PostedFile.FileName);
                    string extencion = Path.GetExtension(NombreArchivo);

                    //Guardo el nombre de la imagen para guardarlo en la propiedad despues
                    var folder = Server.MapPath("~/Documentos/");
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);

                    }
                    filePath = Path.Combine(Server.MapPath("~/Documentos/")
                        , NombreArchivo + extencion);
                    archivo.SaveAs(filePath);

                    txtNombre.Text = Path.GetFileNameWithoutExtension(fuDocs.PostedFile.FileName);
                }
                else
                {
                    return "Error";
                }
                return filePath;
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }

    }
}


        
    
