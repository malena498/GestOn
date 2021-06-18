using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;
namespace BibliotecaClases
{
    partial class Sistema
    {

        public bool GuardarProducto(Producto producto)
        {  
            try
            {
                using (var baseDatos = new Context())
                {
                    producto.Activo = true;
                    baseDatos.Productos.Add(producto);
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

        public bool EliminarProducto(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Producto p = baseDatos.Productos.FirstOrDefault(de => de.ProductoId == id);
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

        public bool ModificarProducto(Producto producto)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Producto pr = baseDatos.Productos.SingleOrDefault(cl => cl.ProductoId == producto.ProductoId);
                    if (pr != null)
                    {
                        pr.ProductoId = producto.ProductoId;
                        pr.categoria = producto.categoria;
                        pr.ProductoMarca = producto.ProductoMarca;
                        pr.ProductoNombre = producto.ProductoNombre;
                        pr.ProductoPrecioCompra = producto.ProductoPrecioCompra;
                        pr.ProductoPrecioVenta = producto.ProductoPrecioVenta;
                        pr.Cantidad = producto.Cantidad;
                        pr.Activo = producto.Activo;
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

        public Producto BuscarProducto(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Productos.Include("categoria").FirstOrDefault(prop => prop.ProductoId == id);
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