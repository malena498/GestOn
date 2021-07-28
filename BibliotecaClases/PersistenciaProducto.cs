using System;
using System.Collections.Generic;
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
                    return baseDatos.Productos.FirstOrDefault(prop => prop.ProductoId == id);
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
                        /*SqlParameter parameter = new SqlParameter("@IdPedido", idPedido);
                        objeto = baseDatos.Database.SqlQuery<Object>("SP_Producto_Pedido @IdPedido", parameter).ToList();
                         * SqlParameter categoryParam = new SqlParameter("@IdPedido", idPedido);
                        List<Producto> productos = baseDatos.Database.SqlQuery<Producto>("exec SP_Producto_Pedido @IdPedido", categoryParam).ToList();
                        return productos;*/
                    }
                    return objeto;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}