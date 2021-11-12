using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestingPersistenciaProductos
    {
        /* GUARDO PRODUCTO */
        [TestMethod]
        public void GuardarProductoTest()
        {
            BibliotecaClases.Clases.Producto producto = new BibliotecaClases.Clases.Producto();
            producto.Activo = true;
            producto.ProductoNombre = "Lapiz mecanico";
            producto.ProductoPrecioVenta = 50;
            producto.ProductoPrecioCompra = 30;
            producto.IdCategoria =1;
            producto.IdMarca = 1;
            producto.Cantidad = 50;

            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarProducto(producto, 1);

            Assert.AreEqual(true, result);
        }

        /* MODIFICO PRODUCTO */
        [TestMethod]
        public void ModificarProductoTest()
        {
            BibliotecaClases.Clases.Producto producto = new BibliotecaClases.Clases.Producto();
            producto.CodigoProducto = 1;
            producto.Activo = true;
            producto.ProductoNombre = "Lapiz mecanico";
            producto.ProductoPrecioVenta = 80;
            producto.ProductoPrecioCompra = 30;
            producto.IdCategoria = 1;
            producto.IdMarca = 1;
            producto.Cantidad = 50;

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarProducto(producto);

            Assert.AreEqual(true, result);
        }

        /* BUSCO USUARIOS CON Y SIN FILTROS */
        [TestMethod]
        public void BuscarProductoTest()
        {
            bool result = false;
            int id = 3;
            BibliotecaClases.Clases.Producto producto = BibliotecaClases.Sistema.GetInstancia().BuscarProducto(id);

            if (producto != null)
                result = true;

            Assert.AreEqual(true, result);

        }
        [TestMethod]
        public void ListadoProductoTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Producto> productos;

            productos = BibliotecaClases.Sistema.GetInstancia().ListadoProductos();

            if (productos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* ELIMINACIÓN DE PRODUCTO*/
        [TestMethod]
        public void EliminarProductoTest()
        {
            int id = 3;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarProducto(id);

            Assert.AreEqual(true, result);
        }
    }
}
