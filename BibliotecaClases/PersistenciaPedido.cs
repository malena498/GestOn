using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {

        public int GuardarPedido(Pedido pedido, List<int> lista)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (var baseDatos = new Context())
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        int idprod = lista[i];
                        Producto pe = baseDatos.Productos.FirstOrDefault(p => p.ProductoId == idprod);
                        productos.Add(pe);
                    }
                    pedido.productos = productos;
                    pedido.Activo = true;
                    baseDatos.Pedidos.Add(pedido);
                    if (pedido.IdPedido != 0)
                    {
                        baseDatos.SaveChanges();
                    }
                }
                return pedido.IdPedido;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool GuardarProductoPedidoCantidad(List<ProductoPedidoCantidad> productoPedidoCantidad)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                using (var baseDatos = new Context())
                {
                    foreach (ProductoPedidoCantidad p in productoPedidoCantidad)
                    {
                        baseDatos.CanttidadProductos.Add(p);
                    }
                    baseDatos.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarPedido(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Pedido p = baseDatos.Pedidos.FirstOrDefault(de => de.IdPedido == id);
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

        public bool ModificarPedido(Pedido pedido, List<int> lista)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    pedido.productos = null;
                    List<Producto> productos = new List<Producto>();
                    for (int i = 0; i < lista.Count; i++)
                    {
                        int idprod = lista[i];
                        Producto pr = baseDatos.Productos.FirstOrDefault(p => p.ProductoId == idprod);
                        productos.Add(pr);
                    }
                    pedido.productos = productos;
                    pedido.Activo = true;
                    baseDatos.Pedidos.Add(pedido);
                    Pedido pe = baseDatos.Pedidos.Include("productos").SingleOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                    if (pe != null)
                    {
                        pe.IdPedido = pedido.IdPedido;
                        pe.Activo = pedido.Activo;
                        pe.Descripcion = pedido.Descripcion;
                        pe.Direccion = pedido.Direccion;
                        pe.FechaEntrega = pedido.FechaEntrega;
                        pe.FechaPedido = pedido.FechaPedido;
                        pe.productos = pedido.productos;
                        pe.UserId = pedido.UserId;
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

        public Pedido BuscarPedido(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Pedidos.Include("productos").FirstOrDefault(prop => prop.IdPedido == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<Pedido> ListadoPedidos()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Where(ej => ej.Activo == true).OrderBy(ej => ej.IdPedido).ToList();
                        return pedidos;
                    }
                    catch
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Where(ej => ej.Activo == true).OrderBy(ej => ej.IdPedido).ToList();
                        return pedidos;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public bool eliminar()
        //{
        //    try
        //    {
        //        using (Context baseDatos = new Context())
        //        {
        //            var query = (from p in baseDatos.ProductoPedido
        //                         where p.Id == id
        //                         select p).Single();

        //            baseDatos.(query);
        //            baseDatos.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
    
}