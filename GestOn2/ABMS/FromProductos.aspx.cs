using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Sql;
using BibliotecaClases.Persistencias;
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
                    ListarCategorias();
                }
            }
        }

        public void ListarCategorias() {
            PersistenciaCategoriaProducto cp = new PersistenciaCategoriaProducto();
            List<CategoriaProducto> categoriaProductos = cp.ListadoCategorias();
            lstCategorias.Items.Clear();
            foreach (CategoriaProducto cat in categoriaProductos)
            {
                String Nombre = cat.NombreCategoria.ToString();
                lstCategorias.DataSource = Nombre;
            }
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
                int id = Int32.Parse(txtIdProducto.Text);
                int cantidad = Int32.Parse(txtCantidad.Text);
                String marca = txtMarca.Text;
                String nombre = txtNombreProducto.Text;
                decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
                //CalculoPrecioVenta(precioCompra);
                decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);

                Producto p = new Producto();
                p.Cantidad = cantidad;
                //p.ProductoCategoría = categoria;
                p.ProductoId = id;
                p.ProductoMarca = marca;
                p.ProductoNombre = nombre;
                p.ProductoPrecioCompra = precioCompra;
                p.ProductoPrecioVenta = PrecioVenta;

                PersistenciaProducto persistencia = new PersistenciaProducto();
                if (persistencia.GuardarProducto(p))
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
            
        }

        //Modifico el producto a traves de la ID
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.
            if (CompleteCampos())
            {
                lblInformativo.Visible = true;
                lblInformativo.Text = "Debe completar todos los campos";
                TimerMensajes.Enabled = true;
            }
            //Si esta todo correcto, procedo a hacer la modificación.
            else
            {
                lblInformativo.Visible = false;
                lblInformativo.Text = "";
                int id = Int32.Parse(txtIdProducto.Text);
                int cantidad = Int32.Parse(txtCantidad.Text);
                //String categoria = txtCategoria.Text;
                String marca = txtMarca.Text;
                String nombre = txtNombreProducto.Text;
                decimal precioCompra = decimal.Parse(txtPrecioCompra.Text);
                //CalculoPrecioVenta(precioCompra);
                decimal PrecioVenta = decimal.Parse(txtPrecioVenta.Text);

                Producto p = new Producto();
                p.Activo = true;
                p.Cantidad = cantidad;
               // p.ProductoCategoría = categoria;
                p.ProductoId = id;
                p.ProductoMarca = marca;
                p.ProductoNombre = nombre;
                p.ProductoPrecioCompra = precioCompra;
                p.ProductoPrecioVenta = PrecioVenta;

                PersistenciaProducto persistencia = new PersistenciaProducto();
                if (persistencia.ModificarProducto(p))
                {
                    lblInformativo.Text = "Se modificó con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    VaciarCampos();
                }
                else
                {
                    lblInformativo.Text = "No se pudo modificó ";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
        }

        //Busco producto a  través de la ID
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtIdProducto.Text != "")
            {
                int id = Int32.Parse(txtIdProducto.Text);
                PersistenciaProducto persistencia = new PersistenciaProducto();
                Producto p = persistencia.BuscarProducto(id);
                if (p != null)
                {
                    if (p.Activo == true)
                    {
                        txtCantidad.Text = p.Cantidad.ToString();
                        txtMarca.Text = p.ProductoMarca.ToString();
                        txtNombreProducto.Text = p.ProductoNombre.ToString();
                        txtPrecioCompra.Text = p.ProductoPrecioCompra.ToString();
                        txtPrecioVenta.Text = p.ProductoPrecioVenta.ToString();
                        btnModificar.Enabled = true;
                        btnEliminar.Enabled = true;
                    }
                    else
                    {
                        lblInformativo.Text = "El producto fue dado de baja";
                        lblInformativo.Visible = true;
                        TimerMensajes.Enabled = true;
                        VaciarCampos();
                    }
                }
                else
                {
                    lblInformativo.Text = "El producto buscado no éxiste en el sistema";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                    VaciarCampos();
                }
            }
            else {
                lblInformativo.Text = "Debe completar id del producto";
                lblInformativo.Visible = true;
                TimerMensajes.Enabled = true;
            }
        }

        //Elimino el producto a través de la ID
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtIdProducto.Text != "")
            {
                int id = Int32.Parse(txtIdProducto.Text);
                Producto p = new Producto();
                p.ProductoId = id;
                PersistenciaProducto persistencia = new PersistenciaProducto();
                if (persistencia.EliminarProducto(p))
                {
                    lblInformativo.Text = "Se elimino con éxito";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;

                    //Elimino campos luego que se modifico con éxito
                    VaciarCampos();
                }
                else
                {
                    lblInformativo.Text = "No se pudo eliminar ";
                    lblInformativo.Visible = true;
                    TimerMensajes.Enabled = true;
                }
            }
            else {
                lblInformativo.Text = "Complete id del poducto que desea eliminar";
                lblInformativo.Visible = true;
                TimerMensajes.Enabled = true;
            }
            
        }

        //Timer usado para mostrar o ocultar mensajes
        protected void TimerMensajes_Tick(object sender, EventArgs e)
        {
            lblInformativo.Visible = false;
            lblCategoriasMsj.Visible = false;
            TimerMensajes.Enabled = false;
        }

        //Valida si falta poner algun dato (retorna true en caso que falte alguno)
        public bool CompleteCampos() {
            if (txtIdProducto.Text == "" || txtCantidad.Text == "" || 
                txtMarca.Text == "" || txtNombreProducto.Text == "" || 
                txtPrecioCompra.Text == "" || txtPrecioVenta.Text == "")
                return true;
            else return false;
        }

        //Vacia los campos de la pantalla excepto el id y los mensajes
        protected void VaciarCampos() {
            txtCantidad.Text = "";
            txtMarca.Text = "";
            txtNombreProducto.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
        }

        //Calcula el precio de venta a traves de un porcentaje dado y el precio de compra
        protected void CalculoPrecioVenta(decimal precioCompra)
        {
            decimal pventa = precioCompra + ((precioCompra * 025) / 100);
            string resultado = pventa.ToString();
            String substring = resultado.Substring(0, 5);
            lblprice.Text = substring;
        }

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
                int id = Int32.Parse(txtIdCat.Text);
                PersistenciaCategoriaProducto persistencia = new PersistenciaCategoriaProducto();
                CategoriaProducto cat = persistencia.BuscarCategorias(id);
                if (cat != null)
                {
                    if (cat.Activo == true)
                    {
                        txtNomCat.Text = cat.NombreCategoria.ToString();
                    }
                    else
                    {
                        lblCategoriasMsj.Text = "La categoria fue dada de baja";
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
            if (txtIdCat.Text == "" || txtNomCat.Text == "")
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
                int id = Int32.Parse(txtIdCat.Text);
                String nombre = txtNomCat.Text;

                CategoriaProducto cat = new CategoriaProducto();
                cat.NombreCategoria = nombre;
                cat.IdCategoria = id;

                PersistenciaCategoriaProducto persistencia = new PersistenciaCategoriaProducto();
                if (persistencia.GuardarCategoria(cat))
                {
                    lblCategoriasMsj.Text = "Se guardo con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;

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
                CategoriaProducto cat = new CategoriaProducto();
                cat.IdCategoria = id;
                PersistenciaCategoriaProducto persistencia = new PersistenciaCategoriaProducto();
                if (persistencia.EliminarCategoria(cat))
                {
                    lblCategoriasMsj.Text = "Se elimino con éxito";
                    lblCategoriasMsj.Visible = true;
                    TimerMensajes.Enabled = true;

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
    }
}