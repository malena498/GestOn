﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Persistencias;

namespace GestOn2.ABMS
{
    public partial class FromProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) Session["NombreBase"] = "GestOn";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.
            if (txtIdProducto.Text == "" || txtCantidad.Text == "" || txtCategoria.Text == ""
                || txtMarca.Text == "" || txtNombreProducto.Text == "" || txtPrecioCompra.Text == ""
                || txtPrecioVenta.Text == "")
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Debe completar todos los campos";
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer el insert.
            else
            {
                lblInformativo.Visible = false;
                lblInformativo.Text = "";
                int id = Int32.Parse(txtIdProducto.Text);
                int cantidad = Int32.Parse(txtCantidad.Text);
                String categoria = txtCategoria.Text;
                String marca = txtMarca.Text;
                String nombre = txtNombreProducto.Text;
                decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
                CalculoPrecioVenta(precioCompra);
                decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                String nameDB = "GestOn";

                Producto p = new Producto();
                p.Activo = true;
                p.Cantidad = cantidad;
                p.ProductoCategoría = categoria;
                p.ProductoId = id;
                p.ProductoMarca = marca;
                p.ProductoNombre = nombre;
                p.ProductoPrecioCompra = precioCompra;
                p.ProductoPrecioVenta = PrecioVenta;

                PersistenciaProducto persistencia = new PersistenciaProducto();
                if (persistencia.GuardarProducto(p, nameDB))
                {
                    lblInformativo.Text = "Se guardo con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
                else {
                    lblInformativo.Text = "No se guardo (Error)" ;
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            
        }
        protected void CalculoPrecioVenta(decimal precioCompra) {
            decimal pventa = precioCompra  + ((precioCompra * 025) / 100);
            string resultado = pventa.ToString();
            String substring = resultado.Substring(0, 5);
            lblprice.Text = substring;
        }
        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            TimerMensajes.Enabled = false;
        }
    }
}