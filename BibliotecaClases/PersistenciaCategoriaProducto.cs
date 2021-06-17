using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    public class PersistenciaCategoriaProducto
    {
        public bool GuardarCategoria(CategoriaProducto categoria)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    categoria.Activo = true;
                    baseDatos.Categorias.Add(categoria);
                    if (categoria.IdCategoria != 0)
                    {
                        baseDatos.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarCategoria(CategoriaProducto categoria)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    CategoriaProducto c = baseDatos.Categorias.FirstOrDefault(de => de.IdCategoria == categoria.IdCategoria);
                    if (c != null)
                    {
                        c.Activo = false;

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

        public bool ModificarCategorias(CategoriaProducto categoria)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    CategoriaProducto cat = baseDatos.Categorias.SingleOrDefault(cl => cl.IdCategoria == categoria.IdCategoria);
                    if (cat != null)
                    {
                        cat.IdCategoria = categoria.IdCategoria;
                        cat.NombreCategoria = categoria.NombreCategoria;
                        cat.Activo = categoria.Activo;
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

        public CategoriaProducto BuscarCategorias(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Categorias.FirstOrDefault(prop => prop.IdCategoria == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<CategoriaProducto> ListadoCategorias()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<CategoriaProducto> categorias = baseDatos.Categorias.Where(ej => ej.Activo == true).OrderBy(ej => ej.IdCategoria).ToList();
                        return categorias;
                    }
                    catch
                    {
                        List<CategoriaProducto> categorias = baseDatos.Categorias.Where(ej => ej.Activo == true).OrderBy(ej => ej.NombreCategoria).ToList();
                        return categorias;
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
