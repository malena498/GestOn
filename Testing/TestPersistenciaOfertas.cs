using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaOfertas
    {
        /*GUARDO OFERTA*/
        [TestMethod]
        public void GuardarOfertaTest()
        {
            BibliotecaClases.Clases.Oferta o = new BibliotecaClases.Clases.Oferta();
            o.IdOferta = 5;
            o.OfertaTitulo = "pack lapiceras";
            o.UserId = 1;
            o.Activo = true;
            o.OfertaFechaDesde = DateTime.Parse("12/02/2021");
            o.OfertaFechaHasta = DateTime.Parse("12/08/2021");
            o.OfertaDescripcion = "Ultima de prueba test";
            o.OfertaPrecio = decimal.Parse("11.3");
            List<String> imagenes = new List<String>();
            String url = "erReport_20210804211541.jpg";
            imagenes = url.Split(char.Parse(",")).ToList();

            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarOferta(o, imagenes);

            Assert.AreEqual(true, result);
        }

        /*MODIFICO OFERTA*/
        [TestMethod]
        public void ModificarOfertaTest()
        {
            BibliotecaClases.Clases.Oferta o = null;
            o = BibliotecaClases.Sistema.GetInstancia().BuscarOferta(5);
            o.OfertaTitulo = "pack lapiceras";
            o.OfertaFechaDesde = DateTime.Parse("12/02/2021");
            o.OfertaFechaHasta = DateTime.Parse("12/08/2021");
            o.OfertaDescripcion = "Quedan pocas unidades, vecinoo vecinaaa";
            o.OfertaPrecio = decimal.Parse("300");
            List<String> imagenes = new List<String>();
            String url = "erReport_20210804211541.jpg";
            imagenes = url.Split(char.Parse(",")).ToList();

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarOferta(o, imagenes);

            Assert.AreEqual(true, result);
        }

        /*BUSQUEDA DE OFERTA*/
        [TestMethod]
        public void BuscarOfertaTest()
        {
            bool result = false;
            int idOferta = 5;

            BibliotecaClases.Clases.Oferta o = BibliotecaClases.Sistema.GetInstancia().BuscarOferta(idOferta);

            if (o != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE OFERTAS */
        [TestMethod]
        public void ListadoOfertasTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Oferta> oferta;
            oferta = BibliotecaClases.Sistema.GetInstancia().ListadoOfertas();

            if (oferta != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE IMAGENES DE LAS OFERTAS */
        [TestMethod]
        public void BuscarImagenesOfertaTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Imagen> imagens;
            int idOferta = 5;

            imagens = BibliotecaClases.Sistema.GetInstancia().BuscarImagenesOferta(idOferta);

            if (imagens != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADO DE OFERTAS CON FILTRO*/
        [TestMethod]
        public void BuscarOfertaFiltrosTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Oferta> ofertas;
            DateTime fechaDesde = Convert.ToDateTime("12/03/2021");
            DateTime fechaHasta = Convert.ToDateTime("12/05/2021");
            String nombre = "pack lapiceras";

            ofertas = BibliotecaClases.Sistema.GetInstancia().BuscarOfertaFiltros(fechaDesde, fechaHasta);

            if (ofertas != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* ELIMINA OFERTAS */
        [TestMethod]
        public void EliminarOfertaTest()
        {
            int id = 4;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarOferta(id);

            Assert.AreEqual(true, result);
        }
    }
}
