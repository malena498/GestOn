﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.UI.HtmlControls;
using BibliotecaClases.Clases;

namespace GestOn2.ABMS
{
    public partial class FromProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
                if (!Page.IsPostBack)
                {
                    ListarProductos();
                    ListarCategorias();
                }
            }
        }

        public void ListarProductos() {
            GridViewProductos.DataSource = Sistema.GetInstancia().ListadoProductos();
            GridViewProductos.DataBind();
        }

        //Listo categorias de productos en el ListBox
        public void ListarCategorias() {
            lstCategorias.Items.Clear();    
            lstCategorias.DataSource = Sistema.GetInstancia().ListadoCategorias();
            lstCategorias.DataTextField = "NombreCategoria";
            lstCategorias.DataValueField = "IdCategoria";
            lstCategorias.DataBind();  
        }

        //Guardo el producto 
        protected void btnGuardar_Click(object sender, EventArgs e)
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
                int cantidad = Int32.Parse(txtCantidad.Text);
                String marca = txtMarca.Text;
                String nombre = txtNombreProducto.Text;
                decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
                int categoria = int.Parse(lstCategorias.SelectedValue);
                decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                CategoriaProducto cat = Sistema.GetInstancia().BuscarCategorias(categoria);
                Producto p = new Producto();
                p.Cantidad = cantidad;
                p.ProductoMarca = marca;
                p.ProductoNombre = nombre;
                p.ProductoPrecioCompra = precioCompra;
                p.ProductoPrecioVenta = PrecioVenta;

                bool exito = Sistema.GetInstancia().GuardarProducto(p,categoria);
                if (exito)
                {
                    lblInformativo.Text = "Se guardo con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se inserto con éxito
                    VaciarCampos();

                }
                else {
                    lblInformativo.Text = "No se guardo (Error)" ;
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            ListarProductos();
        }

        ////Modifico el producto a traves de la ID
        //protected void btnModificar_Click(object sender, EventArgs e)
        //{
        //    //Validaciones que las caja de texto no estén vacias.
        //    if (CompleteCampos())
        //    {
        //        lblInformativo.Visible = true;
        //        lblInformativo.Text = "Debe completar todos los campos";
        //        TimerMensajes.Enabled = true;
        //    }
        //    //Si esta todo correcto, procedo a hacer la modificación.
        //    else
        //    {
        //        lblInformativo.Visible = false;
        //        lblInformativo.Text = "";

        //        int cantidad = Int32.Parse(txtCantidad.Text);
        //        int categoria = int.Parse(lstCategorias.SelectedValue);
        //        String marca = txtMarca.Text;
        //        String nombre = txtNombreProducto.Text;
        //        decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
        //        //CalculoPrecioVenta(precioCompra);
        //        decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
        //        Producto p = new Producto();
        //        p.Activo = true;
        //        p.Cantidad = cantidad;
        //        p.IdCategoria = categoria;
        //        p.ProductoId = id;
        //        p.ProductoMarca = marca;
        //        p.ProductoNombre = nombre;
        //        p.ProductoPrecioCompra = precioCompra;
        //        p.ProductoPrecioVenta = PrecioVenta;

        //        bool exito = Sistema.GetInstancia().ModificarProducto(p);
        //        if (exito)
        //        {
        //            ListarProductos();
        //            lblInformativo.Text = "Se modificó con éxito";
        //            lblInformativo.Visible = true;
        //            TimerMensajes.Enabled = true;

        //            //Elimino campos luego que se modifico con éxito
        //            VaciarCampos();
        //        }
        //        else
        //        {
        //            lblInformativo.Text = "No se pudo modificó ";
        //            lblInformativo.Visible = true;
        //            TimerMensajes.Enabled = true;
        //        }
        //    }
        //}

        //Busco producto a  través de la ID
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //if (txtIdProducto.Text != "")
            //{
            //    int id = int.Parse(txtIdProducto.Text);
            //    Producto p = Sistema.GetInstancia().BuscarProducto(id);
            //    if (p != null)
            //    {
            //        if (p.Activo == true)
            //        {
            //            ListarCategorias();
            //            txtCantidad.Text = p.Cantidad.ToString();
            //            txtMarca.Text = p.ProductoMarca.ToString();
            //            txtNombreProducto.Text = p.ProductoNombre.ToString();
            //            txtPrecioCompra.Text = p.ProductoPrecioCompra.ToString();
            //            txtPrecioVenta.Text = p.ProductoPrecioVenta.ToString();
            //            lstCategorias.Items.FindByValue(p.IdCategoria.ToString()).Selected = true;
            //            btnModificar.Enabled = true;
            //            btnEliminar.Enabled = true;
            //        }
            //        else
            //        {
            //            lblInformativo.Text = "El producto fue dado de baja";
            //            lblInformativo.Visible = true;
            //            TimerMensajes.Enabled = true;
            //            VaciarCampos();
            //        }
            //    }
            //    else
            //    {
            //        lblInformativo.Text = "El producto buscado no éxiste en el sistema";
            //        lblInformativo.Visible = true;
            //        TimerMensajes.Enabled = true;
            //        VaciarCampos();
            //    }
            //}
            //else
            //{
            //    lblInformativo.Text = "Debe completar id del producto";
            //    lblInformativo.Visible = true;
            //    TimerMensajes.Enabled = true;
            //}
        }

        ////Elimino el producto a través de la ID
        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    if (txtIdProducto.Text != "")
        //    {
        //        int id = int.Parse(txtIdProducto.Text);
        //        bool exito = Sistema.GetInstancia().EliminarProducto(id);
        //        if (exito)
        //        {
        //            lblInformativo.Text = "Se elimino con éxito";
        //            lblInformativo.Visible = true;
        //            TimerMensajes.Enabled = true;

        //            //Elimino campos luego que se modifico con éxito
        //            VaciarCampos();
        //            ListarProductos();
        //        }
        //        else
        //        {
        //            lblInformativo.Text = "No se pudo eliminar ";
        //            lblInformativo.Visible = true;
        //            TimerMensajes.Enabled = true;
        //        }
        //    }
        //    else {
        //        lblInformativo.Text = "Complete id del poducto que desea eliminar";
        //        lblInformativo.Visible = true;
        //        TimerMensajes.Enabled = true;
        //    }
            
        //}

        //Timer usado para mostrar o ocultar mensajes
        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            lblCategoriasMsj.Visible = false;
            TimerMensajes.Enabled = false;
        }

        //Valida si falta poner algun dato (retorna true en caso que falte alguno)
        public bool CompleteCampos() {
            if (String.IsNullOrEmpty(txtCantidad.Text) ||
                String.IsNullOrEmpty(txtMarca.Text )||
                String.IsNullOrEmpty(txtNombreProducto.Text) ||
                String.IsNullOrEmpty(txtPrecioCompra.Text) ||
                String.IsNullOrEmpty(txtPrecioVenta.Text))
                return true;
            else return false;
        }

        //Vacia los campos de la pantalla excepto el id y los mensajes
        protected void VaciarCampos() {
            txtCantidad.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtNombreProducto.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
        }

        //Calcula el precio de venta a traves de un porcentaje dado y el precio de compra
        /*protected void CalculoPrecioVenta(decimal precioCompra)
        {
            decimal pventa = precioCompra + ((precioCompra * 025) / 100);
            string resultado = pventa.ToString();
            String substring = resultado.Substring(0, 5);
            lblprice.Text = substring;
        }*/

        //Muestro el panel de edicion de categoria de productos
        protected void linkNewCategoria_Click(object sender, EventArgs e)
        {
            pnlNuevaCat.Visible = true;
        }

        //Busco categoria de productos a través de la id
        protected void btnBuscarCat_Click(object sender, EventArgs e)
        {
            if (txtIdCat.Text != "")
            {
                int id = int.Parse(txtIdCat.Text);
                CategoriaProducto cat = Sistema.GetInstancia().BuscarCategorias(id);
                if (cat != null)
                {
                    if (cat.Activo == true)
                    {
                        txtNomCat.Text = cat.NombreCategoria.ToString();
                    }
                    else
                    {
                        lblCategoriasMsj.Text = "La categoria fue dada de baja";
                        txtNomCat.Text = "";
                        lblCategoriasMsj.Visible = true;
                        TimerMensajes.Enabled = true;
                    }
                }
                else
                {
                    lblCategoriasMsj.Text = "La categoría buscada no éxiste en el sistema";
                    txtNomCat.Text = "";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblCategoriasMsj.Text = "Debe completar id de la categoría";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        //Guardo categoria de producto
        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.

            if (txtNomCat.Text == "")
            {
                lblCategoriasMsj.Text = "Debe completar todos los campos";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer el insert.
            else
            {
                lblCategoriasMsj.Visible = false;
                lblCategoriasMsj.Text = "";
                
                String nombre = txtNomCat.Text;

                CategoriaProducto cat = new CategoriaProducto();
                cat.NombreCategoria = nombre;
                

                bool existe = Sistema.GetInstancia().GuardarCategoria(cat);
                if (existe)
                {
                    lblCategoriasMsj.Text = "Se guardo con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                    ListarCategorias();

                    //Elimino campos luego que se inserto con éxito
                    txtIdCat.Text = "";
                    txtNomCat.Text = "";
                }
                else
                {
                    lblCategoriasMsj.Text = "No se guardo (Error)";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
        }

        //Elimino la categoria del producto a traves del id 
        protected void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (txtIdCat.Text != "")
            {
                int id = Int32.Parse(txtIdCat.Text);
                bool exito = Sistema.GetInstancia().EliminarCategoria(id);
                if (exito)
                {
                    lblCategoriasMsj.Text = "Se elimino con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                    ListarCategorias();

                    //Elimino campos luego que se modifico con éxito
                    txtIdCat.Text = "";
                    txtNomCat.Text = "";
                }
                else
                {
                    lblCategoriasMsj.Text = "No se pudo eliminar ";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else
            {
                lblCategoriasMsj.Text = "Complete id de la categoria a eliminar";
                lblCategoriasMsj.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        protected void btnClosePanel_Click(object sender, EventArgs e)
        {
            pnlNuevaCat.Visible = false;
            txtIdCat.Text = "";
            txtNomCat.Text = "";
        }

        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            ListarProductos();
        }

        protected void GridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit)
                {

                    DropDownList ddlCategoria = (DropDownList)e.Row.FindControl("ddlCategoria");
                    ddlCategoria.DataSource = Sistema.GetInstancia().ListadoCategorias();
                    ddlCategoria.DataTextField = "NombreCategoria";
                    ddlCategoria.DataValueField = "IdCategoria";
                    ddlCategoria.DataBind();

                }
            }
        }

        protected void GridViewProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProductos.EditIndex = e.NewEditIndex;
            ListarProductos();
        }

        
        protected void GridViewProductos_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewProductos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdProducto") as Label).Text);
            string nombre = (row.FindControl("txtNombre") as TextBox).Text;
            string marca = (row.FindControl("txtMarca") as TextBox).Text;
            string cantidad = (row.FindControl("txtCantidad") as TextBox).Text;
            decimal preciocompra = decimal.Parse((row.FindControl("txtPrecioCompra") as TextBox).Text);
            decimal precioventa = decimal.Parse((row.FindControl("txtoPrecioVenta") as TextBox).Text);
            int IdCategoria = Convert.ToInt32((row.FindControl("ddlCategoria") as DropDownList).SelectedValue);


            lblInformativo.Visible = false;
            lblInformativo.Text = string.Empty;
                Producto p = null;
                p = Sistema.GetInstancia().BuscarProducto(Id);
                p.ProductoNombre = nombre;
                p.ProductoMarca = marca;
                p.ProductoPrecioCompra = preciocompra;
                p.ProductoPrecioVenta = precioventa;
                p.IdCategoria = IdCategoria;

                bool exito = Sistema.GetInstancia().ModificarProducto(p);
                if (exito)
                {
                    lblInformativo.Visible = true;
                    lblInformativo.Text = "Se modificó con éxito";
                    GridViewProductos.EditIndex = -1;
                    ListarProductos();
                }
        }

        protected void GridViewProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProductos.EditIndex = -1;
            ListarProductos();
        }

        protected void GridViewProductos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewProductos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdProducto") as Label).Text);
            bool exito = Sistema.GetInstancia().EliminarProducto(Id);
            if (exito)
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Se elimino con éxito";
                GridViewProductos.EditIndex = -1;
                ListarProductos();
            }
        }
    }
}