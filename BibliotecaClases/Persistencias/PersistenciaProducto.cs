﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;
namespace BibliotecaClases.Persistencias
{
    public class PersistenciaProducto
    {
        public bool GuardarProducto(Producto producto, String NombreBase)
        {
            try
            {
                using (var baseDatos = new Context(NombreBase))
                {
                    producto.Activo = true;
                    baseDatos.Productos.Add(producto);
                    baseDatos.SaveChanges();
                    if (producto.ProductoId!= 0)
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

        public bool EliminarProducto(Producto producto)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Producto p = baseDatos.Productos.FirstOrDefault(de => de.ProductoId == producto.ProductoId);
                    if (p != null)
                    {
                        p.Activo = false;

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

        public bool ModificarOferta(Producto producto)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Producto pr = baseDatos.Productos.FirstOrDefault(cl => cl.ProductoId == producto.ProductoId);
                    if (pr != null)
                    {
                        pr.ProductoId = producto.ProductoId;
                        pr.ProductoCategoría = producto.ProductoCategoría;
                        pr.ProductoMarca = producto.ProductoMarca;
                        pr.ProductoNombre = producto.ProductoNombre;
                        pr.ProductoPrecioCompra = producto.ProductoPrecioCompra;
                        pr.ProductoPrecioVenta = producto.ProductoPrecioVenta;
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

        public Producto BuscarProducto(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Productos.FirstOrDefault(prop => prop.ProductoId
 == id);
                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<Producto> ListadoProductos()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Producto> productos = baseDatos.Productos.Where(ej => ej.Activo == true).OrderBy(ej => ej.ProductoId).ToList();
                        return productos;
                    }
                    catch
                    {
                        List<Producto> productos = baseDatos.Productos.Where(ej => ej.Activo == true).OrderBy(ej => ej.ProductoNombre).ToList();
                        return productos;
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