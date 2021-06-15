﻿using System;
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
            if(!IsPostBack) Session["NombreBase"] = "GestOn";
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
            String nameDB;

            //Encapsulo para guardar
            Pedido p = new Pedido();
            txtFechaPedido.Text = DateTime.Now.ToLongTimeString();
            nameDB = Session.ToString();
            string Descripcion = txtDescripcion.InnerText;

            p.IdPedido = 1;
            p.FechaPedido = DateTime.Parse(txtFechaPedido.Text);
            p.FechaEntrega = DateTime.Parse(txtFechaPedido.Text);
            p.UserId = 1;
            p.Direccion = txtDireccion.ToString();
            p.Descripcion = txtDescripcion.InnerText;
            PersistenciaPedido persistencia = new PersistenciaPedido();
            persistencia.GuardarPedido(p,nameDB);
            lblResultado.Text = "Se guardo con éxito";
        }

        protected void btnAgregarTodo_Click(object sender, EventArgs e)
        {
            while (ListSeleccionados.GetSelectedIndices().Length > 0) {
                ListProductos.Items.Add(ListSeleccionados.SelectedItem);
                ListSeleccionados.Items.Remove(ListSeleccionados.SelectedItem);
            }
        }

        protected void btnQuitarTodo_Click(object sender, EventArgs e)
        {
            while (ListProductos.GetSelectedIndices().Length > 0)
            {
                ListSeleccionados.Items.Add(ListProductos.SelectedItem);
                ListProductos.Items.Remove(ListProductos.SelectedItem);
            }
        }
    }
}