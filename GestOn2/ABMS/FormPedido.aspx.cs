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
            if(!IsPostBack) {
                ListarProductos();
            }
            //Obtengo la fecha del sistema, ya que la uso para cargarla en la fecha que se hace el pedido
            txtFechaPedido.Text = DateTime.Now.ToString();
        }

        public void ListarProductos()
        {
            ListProductos.Items.Clear();
            ListProductos.DataSource = Sistema.GetInstancia().ListadoProductos();
            ListProductos.DataTextField = "ProductoNombre";
            ListProductos.DataValueField = "ProductoId";
            ListProductos.DataBind();
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
                int identificador = Int32.Parse(txtId.Text);
                txtFechaPedido.Text = DateTime.Now.ToLongTimeString();
                DateTime fchPedido = DateTime.Parse(txtFechaPedido.Text);
                DateTime fchEntrega = DateTime.Parse(txtFechaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;
                /*List<Producto> lstitems = new List<Producto>();

                for (int i = 0; i < ListSeleccionados.Items.Count; i++)
                {
                    if (ListSeleccionados.Items[i].Selected)
                    {
                        //Producto prod = ListSeleccionados.Items[i].Value;
                        lstitems.Add(prod);
                    }
                }*/


                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.FechaPedido = fchPedido;
                p.IdPedido = identificador;
               // p.productos = lstitems;
                p.UserId = user;

                bool exito = Sistema.GetInstancia().GuardarPedido(p);
                if (exito)
                {
                    lblInformativo.Text = "Se guardo con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se inserto con éxito
                    VaciarCampos();
                    ListarProductos();

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
            while (ListProductos.SelectedItem != null) {
                ListSeleccionados.Items.Add(ListProductos.SelectedItem);
                ListProductos.Items.Remove(ListProductos.SelectedItem);
            }
        }

        protected void btnQuitarTodo_Click(object sender, EventArgs e)
        {
            while (ListSeleccionados.SelectedItem != null)
            {
                ListProductos.Items.Add(ListSeleccionados.SelectedItem);
                ListSeleccionados.Items.Remove(ListSeleccionados.SelectedItem);
            }
        }

        public bool CompleteCampos()
        {
            if (txtId.Text == "" || txtFechaPedido.Text == "" ||
                txtDireccion.Text == "" || txtDescripcion.InnerText == "")
                return true;
            else return false;
        }

        public void VaciarCampos() {
            txtId.Text = "";
            txtFechaPedido.Text = "";
            txtDireccion.Text = "";
            txtDescripcion.InnerText = "";
            ListSeleccionados.Items.Clear();
        }

        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            TimerMensajes.Enabled = false;
        }
    }
}