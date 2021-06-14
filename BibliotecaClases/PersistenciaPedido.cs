using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;
namespace BibliotecaClases
{
    public class PersistenciaPedido
    {
        public bool GuardarPedido(Pedido pedido, String NombreBase)
        {
            try
            {
                using (var baseDatos = new Context(NombreBase))
                {
                    pedido.Activo = true;
                    baseDatos.Pedidos.Add(pedido);
                    baseDatos.SaveChanges();
                    if (pedido.IdPedido != 0)
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

        public bool EliminarPedido(Pedido pedido)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Pedido p = baseDatos.Pedidos.FirstOrDefault(de => de.IdPedido == pedido.IdPedido);
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

        public bool ModificarPedido(Pedido pedido)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Pedido pe = baseDatos.Pedidos.FirstOrDefault(cl => cl.IdPedido == pedido.IdPedido);
                    if (pe != null)
                    {
                        pe.IdPedido = pedido.IdPedido;
                        pe.FechaPedido = pedido.FechaPedido;
                        pe.Direccion = pedido.Direccion;
                        pe.Descripcion = pedido.Descripcion;
                        pe.Activo = pedido.Activo;
                        pe.FechaEntrega = pedido.FechaEntrega;
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
            catch (Exception e)
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
                    return baseDatos.Pedidos.FirstOrDefault(prop => prop.IdPedido == id);
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
                        List<Pedido> pedidos = baseDatos.Pedidos.Where(ej => ej.Activo == true).OrderBy(ej => ej.UserId).ToList();
                        return pedidos;
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