using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
namespace GestOn2
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////AccesoIP.SRProyectos.ProyectoServiceClient client = new AccesoIP.SRProyectos.ProyectoServiceClient();

            //string nombrearchivo;
            //Byte[] archivo;
            //int idDoc = int.Parse(Session["DOC"].ToString());
            //// GUID = Session["GUID"].ToString();
            //Documento doc = Sistema.GetInstancia().BuscarDocumento(idDoc);
            //System.IO.Stream iStream = null;
            //archivo = client.DescargarDocumento(GUID, 1);
            //nombrearchivo = client.NombreDocumento(GUID, 1);

            //try
            //{
            //    long dataToRead;

            //    // Longitud del archivo: 
            //    int length;

            //    byte[] buffer = new Byte[1000];
            //    iStream = new System.IO.MemoryStream(archivo);
            //    //for (int i = 0; i < archivo.Length; i++)
            //    //    iStream.WriteByte(archivo[0]);

            //    HttpContext.Current.Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + nombrearchivo);
            //    dataToRead = iStream.Length;

            //    Response.Clear();

            //    while (dataToRead > 0)
            //    { // Comprobar que el cliente está conectado. 
            //        if (HttpContext.Current.Response.IsClientConnected)
            //        {
            //            // Read the data in buffer. 
            //            length = iStream.Read(buffer, 0, 1000);


            //            // Escribir los datos en la secuencia de salida actual. 
            //            HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

            //            // Vaciar los datos en la salida HTML. 
            //            HttpContext.Current.Response.Flush();

            //            buffer = new Byte[1000]; dataToRead = dataToRead - length;
            //        }
            //        else
            //        { //impedir un bucle infinito si el usuario se desconecta 
            //            dataToRead = -1;
            //        }
            //    }
            //    if (iStream != null)
            //    { //Cerrar el archivo. 
            //        iStream.Close();
            //        iStream.Dispose();
            //        HttpContext.Current.Response.Flush();
            //        HttpContext.Current.Response.Close();
            //        HttpContext.Current.Response.End();
            //        System.Web.HttpContext.Current.Response.Close();
            //    }
            //}

            //catch (Exception ex)
            //{ // Capturar el error, si lo hay. 
            //    Response.Write("Error : " + ex.Message);
            //}
            //finally
            //{

            //}
        
        }

        private void DescargarDoc() {

            Documento d = Sistema.GetInstancia().BuscarDocumento(27);
            string filename = d.NombreDocumento + d.Formato;

            if (filename != "")
            {
                string path = Server.MapPath(filename);
                string ruta = d.ruta;
                System.IO.FileInfo file = new System.IO.FileInfo(ruta);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Write("This file does not exist.");
                }
            }
        }

        protected void bntDescargar_Click(object sender, EventArgs e)
        {
            DescargarDoc();
        }
    }
}