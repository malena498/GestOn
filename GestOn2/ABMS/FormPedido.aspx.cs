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
            if (!IsPostBack) {
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
                int identificador = int.Parse(txtId.Text);
                txtFechaPedido.Text = DateTime.Now.ToLongTimeString();
                DateTime fchPedido = DateTime.Parse(txtFechaPedido.Text);
                DateTime fchEntrega = DateTime.Parse(txtFechaPedido.Text);
                string Descripcion = txtDescripcion.InnerText;
                string Direccion = txtDireccion.Text;
                int user = 1;

                List<int> lstitems = new List<int>();

                for (int i = 0; i < ListSeleccionados.Items.Count; i++)
                {
                    int prod = int.Parse(ListSeleccionados.Items[i].Value);
                    lstitems.Add(prod);
                }


                Pedido p = new Pedido();
                p.Activo = true;
                p.Descripcion = Descripcion;
                p.Direccion = Direccion;
                p.FechaEntrega = fchEntrega;
                p.FechaPedido = fchPedido;
                p.IdPedido = identificador;
                p.UserId = user;

                bool exito = Sistema.GetInstancia().GuardarPedido(p, lstitems);
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                Pedido p = Sistema.GetInstancia().BuscarPedido(id);
                if (p != null)
                {
                    if (p.Activo == true)
                    {
                        txtDescripcion.InnerText = p.Descripcion.ToString();
                        txtDireccion.Text = p.Direccion.ToString();
                        txtFechaPedido.Text = p.FechaPedido.ToString();
                        ListSeleccionados.DataSource = p.productos;
                        ListProductos.Items.Remove(ListSeleccionados.Items.ToString());
                        ListSeleccionados.DataBind();
                        RadioBtnSi.Checked = true;
                        lblDireccion.Visible = true;
                        txtDireccion.Visible = true;
                        txtDireccion.Enabled = true;
                    }
                    else
                    {
                        lblInformativo.Text = "El pedido fue dada de baja";
                        txtDescripcion.InnerText = "";
                        txtDireccion.Text = "";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                    }
                }
                else
                {
                    lblInformativo.Text = "El pedido buscado no éxiste en el sistema";
                    txtDescripcion.InnerText = "";
                    txtDireccion.Text = "";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblInformativo.Text = "Debe completar id del pedido";
                lblInformativo.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        protected void RadioBtnSi_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBtnSi.Checked) { 
                txtDireccion.Enabled = true;
                txtDireccion.Visible = true;
                lblDireccion.Visible = true;
            }
        }

        protected void RadioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            txtDireccion.Enabled = false;
            txtDireccion.Text = "";
            txtDireccion.Visible = false;
            lblDireccion.Visible = false;
        }
    }
}