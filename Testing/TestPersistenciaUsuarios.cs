using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class TestPersistenceUsuario
    {
        /* GUARDO USUARIO */
        [TestMethod]
        public void GuardarUsuarioTest()
        {
            BibliotecaClases.Clases.Usuario u = new BibliotecaClases.Clases.Usuario();
            u.Activo = true;
            u.IdNivel = 1;
            u.UserCedula = "50306210";
            u.UserContrasenia = "Hola";
            u.UserEmail = "alan@alan";
            u.UserId = 3;
            u.UserNombre = "Susana Gomez";
            u.UserTelefono = "098123123";

            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarUsuario(u);

            Assert.AreEqual(true, result);
        }

        /* MODIFICO USUARIO */
        [TestMethod]
        public void ModificarUsuarioTest()
        {
            BibliotecaClases.Clases.Usuario u = null;
            u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuario(3);
            u.Activo = true;
            u.IdNivel = 1;
            u.UserCedula = "50306210";
            u.UserContrasenia = "Pepe";
            u.UserEmail = "alan@alan";
            u.UserNombre = "Susana Gomez";
            u.UserTelefono = "098123123";

            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarUsuario(u);

            Assert.AreEqual(true, result);
        }

        /* BUSCO USUARIOS CON Y SIN FILTROS */
        [TestMethod]
        public void BuscarUsuarioEmailTest()
        {
            bool result = false;
            String email = "frederick@frederick";
            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioEmail(email);

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void BuscarUsuarioIdTest()
        {
            bool result = false;
            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuario(1);

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioCedulaTest()
        {
            bool result = false;
            string cedula = "50306210";

            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioCedula(cedula);

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioXNombreTest()
        {
            bool result = false;
            string nombre = "Susana Gomez";
            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioxNombre(nombre);

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioPorCarpetaTest()
        {
            bool result = false;
            int carpeta = 1;

            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioPorCarpeta(carpeta);

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* LISTADOS DE USUARIOS CON Y SIN FILTROS */
        [TestMethod]
        public void ListadoUsuariosTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Usuario> user;

            user = BibliotecaClases.Sistema.GetInstancia().ListadoUsuarios();

            if (user != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ListadoUsuariosEliminadosTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Usuario> user;
            user = BibliotecaClases.Sistema.GetInstancia().ListadoUsuariosEliminados();

            if (user != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ListadoUsuariosCedulaTest()
        {
            bool result = false;
            String cedula = "50306210";
            List<BibliotecaClases.Clases.Usuario> user;
            user = BibliotecaClases.Sistema.GetInstancia().ListadoUsuariosCedula(cedula);

            if (user != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioFiltroTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Usuario> user;
            string nombre = "al";

            user = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioFiltros(nombre);

            if (user != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* LISTADOS DE NIVELES CON Y SIN FILTROS */

        /* LISTA LOS NIVELES QUE NO SEAN DE TIPO ADMINISTRADOR*/
        [TestMethod]
        public void ListadoNivelesRegistroTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Nivel> nivel;
            nivel = BibliotecaClases.Sistema.GetInstancia().ListadoNivelesRegistro();

            if (nivel != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ListadoNivelesTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Nivel> nivel;
            nivel = BibliotecaClases.Sistema.GetInstancia().ListadoNiveles();

            if (nivel != null)
                result = true;

            Assert.AreEqual(true, result);
        }

        /* ELIMINACIÓN DE USUARIOS*/
        [TestMethod]
        public void EliminarUsuarioTest()
        {
            int id = 3;

            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarUsuario(id);

            Assert.AreEqual(true, result);
        }

    }
}