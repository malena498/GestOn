using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestingProducto
    {
        [TestMethod]
        public void ValidarBuscar()
        {

            bool result = false;
            BibliotecaClases.Clases.Prod u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioEmail("malenag245@gmail.com");
            if (u != null)
            {
                result = true;
            }
            Assert.AreEqual(true, result);

        }
    }
}
/*namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           
                bool result = false;
                BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioEmail("malenag245@gmail.com");
                if (u != null)
                {
                    result = true;
                }
                Assert.AreEqual(true, result);
            
        }
    }
}
 */
