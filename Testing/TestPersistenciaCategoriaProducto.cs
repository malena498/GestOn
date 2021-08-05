using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenciaCategoriaProducto
    {
        /* GUARDA UNA NUEVA CATEGORIAPRODUCTO */
        [TestMethod]
        public void GuardarCategoriaTest()
        {
            BibliotecaClases.Clases.CategoriaProducto c = new BibliotecaClases.Clases.CategoriaProducto();
            c.Activo = true;
            c.IdCategoria = 1;
            c.NombreCategoria = "Regaleria";

            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarCategoria(c);

            Assert.AreEqual(true, result);
        }

        /* MODIFICA CATEGORIAPRODUCTO A TRAVÉS DEL ID DE LA MISMA */
        [TestMethod]
        public void ModificarCategoriasTest()
        {
            int idCat = 1;
            BibliotecaClases.Clases.CategoriaProducto c = null;
            c = BibliotecaClases.Sistema.GetInstancia().BuscarCategorias(idCat);
            c.NombreCategoria = "Utiles";

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarCategorias(c);

            Assert.AreEqual(true, result);
        }

        /* BUSCA DE CATEGORIA POR ID DE CATEGORÍA */
        [TestMethod]
        public void BuscarCategoriasTest()
        {
            bool result = false;
            int idCat = 1;

            BibliotecaClases.Clases.CategoriaProducto c = BibliotecaClases.Sistema.GetInstancia().BuscarCategorias(idCat);

            if (c != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* BUSCA DE CATEGORIA FILTRADA POR NOMBRE */
        [TestMethod]
        public void BuscarIdCategoriaTest()
        {
            bool result = false;
            string nombreCat = "Utiles";

            BibliotecaClases.Clases.CategoriaProducto c = BibliotecaClases.Sistema.GetInstancia().BuscarIdCategoria(nombreCat);

            if (c != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* LISTADO DE CATEGORIA DE PRODUCTOS */
        [TestMethod]
        public void ListadoCategoriasTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.CategoriaProducto> categoriaProductos;
            categoriaProductos = BibliotecaClases.Sistema.GetInstancia().ListadoCategorias();

            if (categoriaProductos != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* ELIMINA CATEGORIA DE PRODUCTOS */
        [TestMethod]
        public void EliminarCategoriaTest()
        {
            int idCat = 1;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarCategoria(idCat);

            Assert.AreEqual(true, result);
        }

    }
}
