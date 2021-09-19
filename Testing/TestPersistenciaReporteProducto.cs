using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaReporteProducto
    {
        [TestMethod]
        /*REPORTE DE LOS PRODUCTOS MÁS VENDIDOS*/
        public void ReporteTopProductosVendidos()
        {
            bool result = false;

            DateTime fch1 = DateTime.Parse("12/08/2019");
            DateTime fch2 = DateTime.Parse("12/08/2021");

            List<BibliotecaClases.Clases.ReporteProductosMasVendidos> report;

            report = BibliotecaClases.Sistema.GetInstancia().ReporteProductosMasVendidos(fch1, fch2);

            if (report != null)
                result = true;

            Assert.AreEqual(true, result);
        }
    }
}
