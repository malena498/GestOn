using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
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
