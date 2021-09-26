using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;
namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarProducto(Producto producto, int cate)
        {  
            try
            {
                using (var baseDatos = new Context())
                {
                    int cat = cate;
                    producto.Activo = true;
                    producto.IdCategoria = cat;
                    baseDatos.Productos.Add(producto);
                    baseDatos.SaveChanges();
                    
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
                    Producto pr = baseDatos.Productos.Include("Categoria").SingleOrDefault(cl => cl.ProductoId == producto.ProductoId);
                    if (pr != null)
                    {
                        pr.ProductoId = producto.ProductoId;
                        pr.IdCategoria = producto.IdCategoria;
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
                    return baseDatos.Productos.Include("Categoria").FirstOrDefault(prop => prop.ProductoId == id);
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
                        List<Producto> productos = baseDatos.Productos.Include("Categoria").Where(ej => ej.Activo == true).OrderBy(ej => ej.ProductoId).ToList();
                        return productos;
                    }
                    catch
                    {
                        List<Producto> productos = baseDatos.Productos.Include("Categoria").Where(ej => ej.Activo == true).OrderBy(ej => ej.ProductoNombre).ToList();
                        return productos;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductoPedidoCantidad> ListadoProductosPedido(int idPedido)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    List<ProductoPedidoCantidad> objeto = new List<ProductoPedidoCantidad>();
                    try
                    {
                         objeto = baseDatos.CanttidadProductos.Include("producto").Where(ej => ej.IdPedido == idPedido).OrderBy(ej => ej.IdPedido).ToList();


                    }
                    catch (Exception ex)
                    {
                    }
                    return objeto;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Producto> ListadoProductoMarca(string marca)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (var baseDatos = new Context())
                {
                    productos = baseDatos.Productos.Include("Categoria").Where(ej => ej.Activo == true && ej.ProductoMarca.Contains(marca)).OrderBy(ej => ej.ProductoId).ToList();
                    return productos;

                }
            }
            catch (Exception ex)
            {
                List<Producto> productos = null;
                return productos;
            }
        }

        public List<Producto> ListadoProductoCategoria(int categoria)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (var baseDatos = new Context())
                {
                    productos = baseDatos.Productos.Include("Categoria").Where(ej => ej.Activo == true && ej.IdCategoria == categoria).OrderBy(ej => ej.ProductoId).ToList();
                    return productos;

                }
            }
            catch (Exception ex)
            {
                List<Producto> productos = null;
                return productos;
            }
        }

    }
}