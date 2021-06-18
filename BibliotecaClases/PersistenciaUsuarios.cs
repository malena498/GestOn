using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using BibliotecaClases.Clases;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;


namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarUsuario(Usuario usuario, String NombreBase)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    usuario.Activo = true;
                    baseDatos.Usuarios.Add(usuario);
                    baseDatos.SaveChanges();
                    

                    
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool EliminarUsuario(Usuario usuario)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Usuario u = baseDatos.Usuarios.FirstOrDefault(de => de.UserId == usuario.UserId);
                    if (u != null)
                    {
                        u.Activo = false;

                        baseDatos.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        public bool ModificarUsuario(Usuario usuario)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Usuario us = baseDatos.Usuarios.FirstOrDefault(cl => cl.UserId== usuario.UserId);
                    if (us != null)
                    {
                        us.UserNombre = usuario.UserNombre;
                        us.UserTelefono = usuario.UserTelefono;
                        us.UserEmail = usuario.UserEmail;
                        us.UserCedula = usuario.UserCedula;
                        us.IdNivel = usuario.IdNivel;
                        baseDatos.SaveChanges();
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Usuario BuscarUsuarioEmail(String email)
        {

            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(res => res.UserEmail== email);
                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public Usuario BuscarUsuario(int id)
        {

            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(res => res.UserId == id);
                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<Usuario> ListadoUsuarios()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Where(ej => ej.Activo == true).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                    catch
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Where(ej => ej.Activo == true).OrderBy(ej => ej.UserNombre).ToList();
                        return usuarios;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Nivel> ListadoNiveles()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Nivel> nivel = baseDatos.Niveles.Where(ej => ej.Activo == true && ej.UserAdmin == false).OrderBy(ej => ej.IdNivel).ToList();
                        return nivel;
                    }
                    catch
                    {
                        List<Nivel> niveles = baseDatos.Niveles.Where(ej => ej.Activo == true && ej.UserAdmin == false).OrderBy(ej => ej.NombreNivel).ToList();
                        return niveles;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
            
            

