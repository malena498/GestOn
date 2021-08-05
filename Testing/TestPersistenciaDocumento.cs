using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaDocumento
    {
        /*GUARDO DOCUMENTO*/
        [TestMethod]
        public void GuardarDocumentoTest()
        {
            BibliotecaClases.Clases.Documento d = new BibliotecaClases.Clases.Documento();
            d.IdDocumento = 1;
            d.AColor = true;
            d.Activo = true;
            d.Descripcion = "Lo quiero en una hoja A4";
            d.Direccion = "";
            d.EsDobleFaz = false;
            d.esEnvio = false;
            d.EsImagen = false;
            d.EsPractico = true;
            d.NroPractico = 2;
            d.estado = "Pendiente";
            d.FechaIngreso = DateTime.Parse("12/03/2021");
            d.Formato = "txt";
            d.gradoLiceal = 4;
            d.NombreDocumento = "Practico Matematicas Financieras";
            d.ruta = "C:,Users\alanb,Source,Repos,alena498,GestOn,GestOn2,Imagenes";
            d.UserId = 1;

            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarDocumento(d);

            Assert.AreEqual(true, result);
        }

        /*MODIFICO DOCUMENTO*/
        [TestMethod]
        public void ModificarDocumentoTest()
        {
            BibliotecaClases.Clases.Documento d = null;
            d = BibliotecaClases.Sistema.GetInstancia().BuscarDocumento(1);
            d.NombreDocumento = "Matematicas de ort";

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarDocumento(d);

            Assert.AreEqual(true, result);
        }

        /*BUSQUEDA DE DOCUMENTO*/
        [TestMethod]
        public void BuscarDocumentoTest()
        {
            bool result = false;
            int idDocumento = 1;

            BibliotecaClases.Clases.Documento d = BibliotecaClases.Sistema.GetInstancia().BuscarDocumento(idDocumento);

            if (d != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE DOCUMENTOS SIN FILTROS */
        [TestMethod]
        public void ListadoDocumentosTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Documento> documentos;
            documentos = BibliotecaClases.Sistema.GetInstancia().ListadoDocumentos();

            if (documentos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE DOCUMENTOS FILTRADO POR NOMBRE */
        [TestMethod]
        public void ListadoDocumentoNombreTest()
        {
            bool result = false;
            String nombreDoc = "Matematicas de ort";
            List<BibliotecaClases.Clases.Documento> documentos;
            documentos = BibliotecaClases.Sistema.GetInstancia().ListadoDocumentoNombre(nombreDoc);

            if (documentos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE DOCUMENTO FILTRADO POR SI ES PRACTICO O NO */
        [TestMethod]
        public void ListadoDocumentoPracticoTest()
        {
            bool result = false;
            bool esPractico = true;
            List<BibliotecaClases.Clases.Documento> documentos;
            documentos = BibliotecaClases.Sistema.GetInstancia().ListadoDocumentoPractico(esPractico);

            if (documentos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE DOCUMENTOS POR FECHA */
        [TestMethod]
        public void ListadoDocumentosFechasTest()
        {
            bool result = false;
            DateTime fechaDesde = Convert.ToDateTime("10/03/2021");
            DateTime fechaHasta = Convert.ToDateTime("12/05/2021");
            List<BibliotecaClases.Clases.Documento> documentos;
            documentos = BibliotecaClases.Sistema.GetInstancia().ListadoDocumentosFechas(fechaDesde, fechaHasta);

            if (documentos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*LISTADOS DE DOCUMENTOS FILTRADO POR ID DE USUARIO */
        [TestMethod]
        public void ListadoDocumentoUserTest()
        {
            bool result = false;
            int idUser = 1;
            List<BibliotecaClases.Clases.Documento> documentos;
            documentos = BibliotecaClases.Sistema.GetInstancia().ListadoDocumentoUser(idUser);

            if (documentos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /*ELIMINACION DE DOCUMENTOS */
        [TestMethod]
        public void EliminarDocumentoTest()
        {
            int id = 1;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarDocumento(id);

            Assert.AreEqual(true, result);
        }
    }
}
