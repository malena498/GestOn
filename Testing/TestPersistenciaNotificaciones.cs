using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaNotificaciones
    {
       
        [TestMethod]
        public void GuardarNotificacionesDocumentoTest()
        {
            bool result = false;
            int idDocumento = 5;
            int idUsuario = 11; 
            string NombreUsuario = "Lucia Rodriguez";
            string AccionUsuario = "NUEVO";
            bool ex = BibliotecaClases.Sistema.GetInstancia().GuardarNotificacionDocumento(idUsuario, NombreUsuario, AccionUsuario, idDocumento);
            if (ex)
                result = true;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GuardarNotificacionesPedidoTest()
        {
            bool result = false;
            int idUsuario = 9;
            string NombreUsuario = "Pedro Perez";
            string AccionUsuario = "NUEVO";

            bool ex = BibliotecaClases.Sistema.GetInstancia().GuardarNotificacionPedido(idUsuario, NombreUsuario, AccionUsuario);
            if (ex)
                result = true;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ListadoNotificacionesTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Notificaciones> notis;

            notis = BibliotecaClases.Sistema.GetInstancia().UltimasNotificaciones();

            if (notis != null)
                result = true;

            Assert.AreEqual(true, result);
        }

    }
}
