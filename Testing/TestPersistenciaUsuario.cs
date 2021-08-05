using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BuscarUsuarioEmailTest()
        {
           
                bool result = false;
                BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioEmail("frederick@frederick");
                if (u != null)
                {
                    result = true;
                }
                Assert.AreEqual(true, result);
            
        }

        [TestMethod]
        public void GuardarUsuarioTest()
        {
            BibliotecaClases.Clases.Usuario u = new BibliotecaClases.Clases.Usuario();
            u.Activo = true;
            u.IdNivel = 1;
            u.UserCedula = "50306210";
            u.UserContrasenia = "Hola";
            u.UserEmail = "alan@alan";
            u.UserId = 1;
            u.UserNombre = "Susana Gomez";
            u.UserTelefono = "098123123";
            bool result = BibliotecaClases.Sistema.GetInstancia().GuardarUsuario(u);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ModificarUsuarioTest()
        {
            BibliotecaClases.Clases.Usuario u = new BibliotecaClases.Clases.Usuario();
            u.Activo = true;
            u.IdNivel = 1;
            u.UserCedula = "50306210";
            u.UserContrasenia = "Pepe";
            u.UserEmail = "alan@alan";
            u.UserId = 2;
            u.UserNombre = "Susana Gomez";
            u.UserTelefono = "098123123";
            bool result = BibliotecaClases.Sistema.GetInstancia().ModificarUsuario(u);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EliminarUsuarioTest()
        {
            int id = 2;
            bool result = BibliotecaClases.Sistema.GetInstancia().EliminarUsuario(id);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioIdTest()
        {

            bool result = false;
            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuario(1);
            if (u != null)
            {
                result = true;
            }
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
            {
                result = true;
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void BuscarUsuarioCedulaTest()
        {
            bool result = false;
            BibliotecaClases.Clases.Usuario u = BibliotecaClases.Sistema.GetInstancia().BuscarUsuarioCedula("50306210");

            if (u != null)
                result = true;

            Assert.AreEqual(true, result);
        }

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
        public void ListadoNivelesRegistroTest()
        {
            bool result = false;
            List<BibliotecaClases.Clases.Nivel> nivel;
            nivel = BibliotecaClases.Sistema.GetInstancia().ListadoNivelesRegistro();

            if (nivel != null)
                result = true;

            Assert.AreEqual(true, result);
        }
    }
}
