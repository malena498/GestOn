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
    public partial class FormConfiguraciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String nombre = "";
                if (CostoEnvio.AccessKey.Equals("1"))
                    nombre = "CostoEnvio";

                String valor = txtCostoPedido.Text;
                bool exito = Sistema.GetInstancia().GuardarConfiguracion(nombre, valor);
            }
            catch (Exception ex) { }
        }
    }
}