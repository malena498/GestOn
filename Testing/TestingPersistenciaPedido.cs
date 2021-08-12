using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibliotecaClases;
namespace Testing
{
    [TestClass]
    public class TestingPersistenciaPedido
    {
        /* GUARDO PEDIDO */
        [TestMethod]
        public void GuardarPedidoTest()
        {
            BibliotecaClases.Clases.Pedido pedido= new BibliotecaClases.Clases.Pedido();
            pedido.Activo = true;
            pedido.FechaEntrega = DateTime.Parse("12/08/2021");
            pedido.FechaPedido = DateTime.Now;
            pedido.Direccion = "Uruguay 1898";
            pedido.Descripcion = "Pedido de productos varios";
            pedido.UserId = 3;
            pedido.Precio = 400;
            pedido.Estado =  "Pendiente";
            List<int> lista = new List<int>();
            lista.Add(1);
            lista.Add(3);
            List<String> imagenes = null;
            bool result = false;
            int id = BibliotecaClases.Sistema.GetInstancia().GuardarPedido(pedido, lista, imagenes, lista);
            if (id > 0) { result = true; }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GuardarProductoPedidoCantidadTest()
        {
            List<BibliotecaClases.Clases.ProductoPedidoCantidad> productoPedidoCantidad = new List<BibliotecaClases.Clases.ProductoPedidoCantidad>();
            BibliotecaClases.Clases.ProductoPedidoCantidad  ppc = new BibliotecaClases.Clases.ProductoPedidoCantidad();
            BibliotecaClases.Clases.ProductoPedidoCantidad ppc2 = new BibliotecaClases.Clases.ProductoPedidoCantidad();
            ppc.IdPedido = 4;
            ppc.ProductoId = 1;
            ppc.Cantidad = 8;
            ppc2.IdPedido = 4;
            ppc2.ProductoId = 3;
            ppc2.Cantidad = 6;
            productoPedidoCantidad.Add(ppc);
            productoPedidoCantidad.Add(ppc2);
            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarProductoPedidoCantidad(productoPedidoCantidad);
            Assert.AreEqual(true, result);
        }
        
        /* MODIFICO PRODUCTO */
        [TestMethod]
        public void ModificarPedidoTest()
        {
            BibliotecaClases.Clases.Pedido pedido = new BibliotecaClases.Clases.Pedido();
            pedido.IdPedido = 4;
            pedido.Activo = true;
            pedido.FechaEntrega = DateTime.Parse("12/08/2021");
            pedido.FechaPedido = DateTime.Now;
            pedido.Direccion = "Uruguay 1898";
            pedido.Descripcion = "Pedido de productos varios";
            pedido.UserId = 3;
            pedido.Precio = 400;
            pedido.Estado = "Pendiente";
            List<int> lista = new List<int>();
            lista.Add(1);
            lista.Add(3);
            List<String> imagenes = null;

            bool result = false;
            int id = BibliotecaClases.Sistema.GetInstancia().ModificarPedido(pedido, lista);
            if (id > 0) result = true;
            Assert.AreEqual(true, result);
        }
        public void ModificarPedidoAdministradorTest()
        {
            BibliotecaClases.Clases.Pedido pedido = new BibliotecaClases.Clases.Pedido();
            pedido.IdPedido = 4;
            pedido.Activo = true;
            pedido.FechaEntrega = DateTime.Parse("12/08/2021");
            pedido.FechaPedido = DateTime.Now;
            pedido.Direccion = "Uruguay 1898";
            pedido.Descripcion = "Pedido de productos varios";
            pedido.UserId = 3;
            pedido.Precio = 400;
            pedido.Estado = "Pendiente";

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarPedidoAdministrador(pedido);

            Assert.AreEqual(true, result);
        }
        
        /* BUSCO USUARIOS CON Y SIN FILTROS */
        [TestMethod]
        public void BuscarPedidoTest()
        {
            bool result = false;
            int id = 4;
            BibliotecaClases.Clases.Pedido pedido = BibliotecaClases.Sistema.GetInstancia().BuscarPedido(id);

            if (pedido != null)
                result = true;

            Assert.AreEqual(true, result);

        }
        [TestMethod]
        public void ListadoPedidosTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Pedido> pedidos;

            pedidos = BibliotecaClases.Sistema.GetInstancia().ListadoPedidos();

            if (pedidos != null)
                result = true;

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ListadoPedidosUsuarioTest()
        {
            int idUser = 1;
            bool result = false;
            List<BibliotecaClases.Clases.Pedido> pedidos;

            pedidos = BibliotecaClases.Sistema.GetInstancia().ListadoPedidosUsuario(idUser);

            if (pedidos != null)
                result = true;

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ListadoPedidosEstadoTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Pedido> pedidos;

            pedidos = BibliotecaClases.Sistema.GetInstancia().ListadoPedidosEstado("pendiente");

            if (pedidos != null)
                result = true;

            Assert.AreEqual(true, result);
        }
        
        /* ELIMINACIÓN DE PRODUCTO*/
        [TestMethod]
        public void EliminarPedidoTest()
        {
            int id = 4;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarPedido(id);

            Assert.AreEqual(true, result);
        }
    }
}
