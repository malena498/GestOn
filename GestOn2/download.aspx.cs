using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
namespace GestOn2
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["IdDoc"].ToString());
            Documento d = Sistema.GetInstancia().BuscarDocumento(id);
            string filename = d.NombreDocumento +"."+ d.Formato;

            if (filename != "")
            {
                Response.Clear();

                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", filename));
                Response.ContentType = "application/octet-stream";

                Response.WriteFile(Server.MapPath(Path.Combine("~/Documentos", filename)));

                Response.End();
            }
           
        }

       
    }

    
}