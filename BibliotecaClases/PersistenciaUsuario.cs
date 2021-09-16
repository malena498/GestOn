using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public int GuardarUsuario(Usuario usu)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    usu.Activo = true;
                    baseDatos.Usuarios.Add(usu);
                    baseDatos.SaveChanges();
                   
                }
                return usu.UserId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool EliminarUsuario(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Usuario usuario = baseDatos.Usuarios.FirstOrDefault(de => de.UserId == id);
                    if (usuario != null)
                    {
                        usuario.Activo = false;

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
                    Usuario us = baseDatos.Usuarios.FirstOrDefault(cl => cl.UserId == usuario.UserId);
                    if (us != null)
                    {
                        us.UserNombre= usuario.UserNombre;
                        us.UserEmail = usuario.UserEmail;
                        us.UserCedula = usuario.UserCedula;
                        us.UserTelefono = usuario.UserTelefono;
                        us.UserContrasenia = usuario.UserContrasenia;

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

        public Usuario BuscarUsuario(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.Include("nivel").FirstOrDefault(prop => prop.UserId == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Usuario> BuscarUsuarioFiltros(string nombre)
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                using (var baseDatos = new Context())
                {
                    usuarios = baseDatos.Usuarios.SqlQuery("select * from Usuario where Activo = 1 and UserNombre like '%" + nombre + "%'").ToList();
                    return usuarios;
                }
            }
            catch (Exception ex)
            {
                List<Usuario> usuarios = null;
                return usuarios;
            }
        }

        public Usuario BuscarUsuarioEmail(string email)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(prop => prop.UserEmail == email && prop.Activo == true);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Usuario BuscarUsuarioCedula(string cedula)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(prop => prop.UserCedula == cedula && prop.Activo == true);
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
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == true).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                    catch
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == true).OrderBy(ej => ej.UserNombre).ToList();
                        return usuarios;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Usuario> ListadoUsuariosEliminados()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == false).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                    catch
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == false).OrderBy(ej => ej.UserNombre).ToList();
                        return usuarios;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Nivel> ListadoNivelesRegistro()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Nivel> niveles = baseDatos.Niveles.Where(ej => ej.Activo == true && ej.UserAdmin != true).OrderBy(ej => ej.IdNivel).ToList();
                        return niveles;
                    }
                    catch
                    {
                        List<Nivel> niveles = baseDatos.Niveles.Where(ej => ej.Activo == true && ej.UserAdmin != true).OrderBy(ej => ej.NombreNivel).ToList();
                        return niveles;
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
                        List<Nivel> niveles = baseDatos.Niveles.Where(ej => ej.Activo == true).OrderBy(ej => ej.IdNivel).ToList();
                        return niveles;
                    }
                    catch
                    {
                        List<Nivel> niveles = baseDatos.Niveles.Where(ej => ej.Activo == true).OrderBy(ej => ej.NombreNivel).ToList();
                        return niveles;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Usuario> ListadoUsuariosCedula(string cedula)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == true && ej.UserCedula == cedula).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                    catch
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == true && ej.UserCedula == cedula).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Usuario BuscarUsuarioxNombre(string nombre)
        {

            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(prop => prop.UserNombre == nombre);
                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public Usuario BuscarUsuarioPorCarpeta(int nroCarpeta)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Usuarios.FirstOrDefault(prop => prop.NroCarpeta == nroCarpeta);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Usuario> ListadoUsuariosOfertas()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Usuario> usuarios = baseDatos.Usuarios.Include("nivel").Where(ej => ej.Activo == true && ej.ReciveOfertas == true).OrderBy(ej => ej.UserId).ToList();
                        return usuarios;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Nivel BuscarNivel(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Niveles.FirstOrDefault(prop => prop.IdNivel == id && prop.Activo == true);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Curso BuscarCurso(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Cursos.FirstOrDefault(prop => prop.Id == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool GuardarDocente(MateriaCursoDocente d)
        {
            try
            {
                using (var baseDatos = new Context())
                {

                    baseDatos.MateriaCursoDocentes.Add(d);
                    baseDatos.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool GuardarEstudiante(CursoEstudiante e)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    baseDatos.CursoEstudiantes.Add(e);
                    baseDatos.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable ListadoCursos()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Curso> cursos = baseDatos.Cursos.OrderBy(c => c.Grado).ToList();
                        DataTable table = new DataTable();
                        DataColumn c1 = new DataColumn();
                        c1.ColumnName = "IDCURSO";
                        table.Columns.Add(c1);
                        DataColumn c2 = new DataColumn();
                        c2.ColumnName = "CURSO";
                        table.Columns.Add(c2);
                        foreach (Curso c in cursos)
                        {
                            DataRow r = table.NewRow();
                            r["IDCURSO"] = c.Id;
                            r["CURSO"] = c.Grado + "-" + c.Grupo;
                            table.Rows.Add(r);
                        }
                      
                        return table;
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MateriaCursoDocente> ListadoMateriaCursoDocente(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<MateriaCursoDocente> MateriaCursoDocente = baseDatos.MateriaCursoDocentes.Where(ej => ej.idDocente== id).OrderBy(ej => ej.IdCurso).ToList();
                        return MateriaCursoDocente;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MateriaCursoDocente> ListadoMateriaCursoDocentexGrado(int idGrado)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<MateriaCursoDocente> MateriaCursoDocente = baseDatos.MateriaCursoDocentes.Where(ej => ej.IdCurso == idGrado).OrderBy(ej => ej.IdCurso).ToList();
                        return MateriaCursoDocente;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CursoEstudiante BuscarCursoEstudiante(Usuario u)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.CursoEstudiantes.FirstOrDefault(prop => prop.idEstudiante == u.UserId);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
