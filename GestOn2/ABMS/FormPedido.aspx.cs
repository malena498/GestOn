using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BibliotecaClases;

namespace GestOn2.ABMS
{
    public partial class FormPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Obtengo la fecha del sistema, ya que la uso para cargarla en la fecha que se hace el pedido
            txtFechaPedido.Text = DateTime.Now.ToString();
        }

        protected void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            //Creo las variables donde voy a guardar los valores
            ;
            string Direccion;
            DateTime FechaPedido;
            int IdPedido;
            int idUser = 1;
            int Activo = 1;

            //Encapsulo para guardar
            Pedido p = new Pedido();
            txtFechaPedido.Text = DateTime.Now.ToLongTimeString();

            /*HtmlTextArea Description = (HtmlTextArea)FindControl("txtDescripcion");
            string Descripcion = Description.Value;*/
            string Descripcion = txtDescripcion.InnerText;
            lblDireccion.Text = txtDireccion.Text;
            lblDescripcion.Text = Descripcion;
            lblId.Text = idUser.ToString();
            lblFecha.Text = txtFechaPedido.Text;

        }
    }
}