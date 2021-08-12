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

        public int GuardarPedido(Pedido pedido, List<String> imagenes, List<int> listadocs)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                  
                    pedido.FechaPedido = DateTime.Now;
                    pedido.Activo = true;
                    pedido.Estado = "Pendiente";
                    baseDatos.Pedidos.Add(pedido);
                    baseDatos.SaveChanges();
                    if (pedido.IdPedido != 0)
                    {
                        if (imagenes != null)
                        {
                            foreach (String url in imagenes)
                            {
                                ImagenPedido img = new ImagenPedido();
                                img.ImagenPedidoURL = url;
                                img.IdPedido = pedido.IdPedido;
                                baseDatos.ImagenesPedidos.Add(img);
                            }
                        }
                        if (listadocs != null)
                        {
                            foreach (int id in listadocs)
                            {
                                PedidoDocumento pd = new PedidoDocumento();
                                pd.idDocumento = id;
                                pd.idPedido = pedido.IdPedido;
                                baseDatos.PedidoDocumentos.Add(pd);
                            }
                        }

                    }
                    baseDatos.SaveChanges();

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
                    foreach (ProductoPedidoCantidad p in productoPedidoCantidad)//Find(,,);
                    {
                        ProductoPedidoCantidad ppc = baseDatos.CanttidadProductos.Include("producto").SingleOrDefault(cl => cl.IdPedido == p.IdPedido && cl.ProductoId == p.ProductoId && cl.Cantidad == p.Cantidad); 
                        if (ppc == null)
                        {
                            baseDatos.CanttidadProductos.Add(p);
                        }
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

        public bool GuardarProductoPedidoCantidad(ProductoPedidoCantidad productoPedidoCantidad)
        {
            try
            {
                using (var baseDatos = new Context())
                {

                    if (productoPedidoCantidad != null)
                    {
                        Producto p = baseDatos.Productos.Include("Categoria").SingleOrDefault(cl => cl.ProductoId == productoPedidoCantidad.ProductoId);
                        productoPedidoCantidad.producto = p;
                        baseDatos.CanttidadProductos.Add(productoPedidoCantidad);
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
                    Pedido p = baseDatos.Pedidos.Include("productosCantidad").FirstOrDefault(de => de.IdPedido == id);
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

        public bool CancelarPedido(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Pedido p = baseDatos.Pedidos.Include("productosCantidad").FirstOrDefault(de => de.IdPedido == id);
                    if (p != null)
                    {
                        if (!p.Estado.Equals("Entregado"))
                        {
                            p.Estado = "Cancelado";
                            p.Activo = false;
                            baseDatos.SaveChanges();

                            return true;
                        }
                        else
                        {
                            return false;
                        }
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

        public int ModificarPedido(Pedido pedido)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                                     
                    pedido.Activo = true;
                    Pedido pe = baseDatos.Pedidos.SingleOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                    if (pe != null)
                    {
                        pe.IdPedido = pedido.IdPedido;
                        pe.Activo = pedido.Activo;
                        pe.Descripcion = pedido.Descripcion;
                        pe.Direccion = pedido.Direccion;
                        pe.FechaEntrega = pedido.FechaEntrega;
                        pe.FechaPedido = DateTime.Now;
                        //pe.productosCantidad = pedido.productosCantidad;
                        pe.UserId = pedido.UserId;
                        pe.Precio = pedido.Precio;
                        baseDatos.SaveChanges();
                        return pe.IdPedido;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool ModificarPedidoAdministrador(Pedido pedido)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Pedido pe = baseDatos.Pedidos.Include("productosCantidad").SingleOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                    if (pe != null)
                    {
                        pe.IdPedido = pedido.IdPedido;
                        pe.Activo = pedido.Activo;
                        pe.Descripcion = pedido.Descripcion;
                        pe.Direccion = pedido.Direccion;
                        pe.FechaEntrega = pedido.FechaEntrega;
                        pe.FechaPedido = DateTime.Now;
                        pe.productosCantidad = pedido.productosCantidad;
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
                Pedido p = new Pedido();
                using (var baseDatos = new Context())//SingleOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                {
                    p = baseDatos.Pedidos.Include("productosCantidad").Include("usuario").SingleOrDefault(prop => prop.IdPedido == id);
                }
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
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
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true).Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                    catch
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true).Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Pedido> ListadoPedidosUsuario(int idUser)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true && ej.UserId == idUser && ej.Estado != "Cancelado").Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                    catch
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true && ej.UserId == idUser).Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Pedido> ListadoPedidosEstado(string Estado)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true && ej.Estado == Estado).Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                    catch
                    {
                        List<Pedido> pedidos = baseDatos.Pedidos.Include("productosCantidad").Where(ej => ej.Activo == true && ej.Estado == Estado).Distinct().OrderByDescending(ej => ej.FechaPedido).ToList();
                        return pedidos;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ProductoPedidoCantidad> ListadoProductosPedidos(int id)
        {
            try
            {
                List<ProductoPedidoCantidad> productos = new List<ProductoPedidoCantidad>();
                using (var baseDatos = new Context())
                {
                    try
                    {
                        
                        productos = baseDatos.CanttidadProductos.Include("producto").Where(prop =>  prop.IdPedido == id).ToList(); 
                        return productos;
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

        public bool EliminarProductoPedidoCant(int idPedido, int idProducto, int cant)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        ProductoPedidoCantidad p = baseDatos.CanttidadProductos.Include("producto").SingleOrDefault(cl => cl.IdPedido == idPedido && cl.ProductoId == idProducto && cl.Cantidad == cant);
                        if (p != null)
                        {
                            baseDatos.CanttidadProductos.Remove(p);
                            
                        }
                        baseDatos.SaveChanges();
                        return true;
                    }
                    catch
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

        public ProductoPedidoCantidad BuscarPedidoProductoCantidad(int idPedido, int IdProducto, int cantidad)
        {
            try
            {
                ProductoPedidoCantidad p = new ProductoPedidoCantidad();
                using (var baseDatos = new Context())//SingleOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                {
                    p = baseDatos.CanttidadProductos.Include("producto").SingleOrDefault(cl => cl.IdPedido == idPedido && cl.ProductoId == IdProducto && cl.Cantidad == cantidad);
                }
                if (p != null)
                {
                    return p;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
    
}