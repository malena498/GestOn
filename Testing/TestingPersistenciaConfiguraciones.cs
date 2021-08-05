using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestingPersistenciaConfiguraciones
    {
        /* GUARDO PEDIDO */
        [TestMethod]
        public void GuardarConfiguracionesTest()
        {
            List<BibliotecaClases.Clases.Configuracion> confs = new List<BibliotecaClases.Clases.Configuracion>();

            BibliotecaClases.Clases.Configuracion conf = new BibliotecaClases.Clases.Configuracion();
            conf.Activo = true;
            conf.Valor = "50";
            conf.Nombre = "CostoEnvio";

            confs.Add(conf);
            
            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarConfiguraciones(confs);
            
            Assert.AreEqual(true, result);
        }

        
        /* BUSCO USUARIOS CON Y SIN FILTROS */
        [TestMethod]
        public void BuscarConfiguracionTest()
        {
            bool result = false;
            BibliotecaClases.Clases.Configuracion conf = BibliotecaClases.Sistema.GetInstancia().BuscarConfiguracion("CostoEnvio");

            if (conf != null)
                result = true;

            Assert.AreEqual(true, result);

        }
        
    }
}
