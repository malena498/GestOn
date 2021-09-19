using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaReporte
    {
        [TestMethod]
        /*REPORTE DE USUARIOS QUE MAS PEDIDOS REALIZAN*/
        public void ReporteClienteMasCompras()
        {
            bool result = false;

            DateTime fch1 = DateTime.Parse("12/08/2019");
            DateTime fch2 = DateTime.Parse("12/08/2021");

            List<BibliotecaClases.Clases.Reporte> report;

            report = BibliotecaClases.Sistema.GetInstancia().ReportCliProducto(fch1,fch2);

            if (report != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        /*REPORTE DE USUARIOS QUE MAS DOCUMENTOS SUBEN A LA WEB*/
        public void ReporteClienteMasDocumentos()
        {
            bool result = false;

            DateTime fch1 = DateTime.Parse("12/08/2019");
            DateTime fch2 = DateTime.Parse("12/08/2021");

            List<BibliotecaClases.Clases.Reporte> report;

            report = BibliotecaClases.Sistema.GetInstancia().ReportCliDocumentos(fch1, fch2);

            if (report != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        /*REPORTE DE USUARIOS QUE MAS DINERO EN PEDIDOS HAN GASTADO*/
        public void ReporteClienteMasGasto()
        {
            bool result = false;

            DateTime fch1 = DateTime.Parse("12/08/2019");
            DateTime fch2 = DateTime.Parse("12/08/2021");

            List<BibliotecaClases.Clases.Reporte> report;

            report = BibliotecaClases.Sistema.GetInstancia().ReportCliGastos(fch1, fch2);

            if (report != null)
                result = true;

            Assert.AreEqual(true, result);
        }
    }
}
