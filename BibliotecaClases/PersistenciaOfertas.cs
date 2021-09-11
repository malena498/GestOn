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

namespace BibliotecaClases { 

    partial class Sistema
    {
            public bool GuardarOferta(Oferta oferta, List<String> imagenes)
            {
                try
                {
                    using (var baseDatos = new Context())
                    {
                        oferta.Activo = true;
                        baseDatos.Ofertas.Add(oferta);
                        baseDatos.SaveChanges();
                        if (oferta.IdOferta != 0)
                        {
                            if (imagenes != null)
                            {
                                foreach (String url in imagenes)
                                {
                                    Imagen img = new Imagen();
                                    img.ImagenURL = url;
                                    img.Activo = true;
                                    img.IdOferta = oferta.IdOferta;
                                    baseDatos.Imagenes.Add(img);
                                }
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

            public bool EliminarOferta(int id)
            {
                try
                {
                    using (var baseDatos = new Context())
                    {
                        Oferta o = baseDatos.Ofertas.FirstOrDefault(de => de.IdOferta == id);
                        if (o != null)
                        {
                            o.Activo = false;

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

            public bool ModificarOferta(Oferta oferta, List<String> listaImagenes)
            {
                try
                {
                    using (var baseDatos = new Context())
                    {
                        Oferta of = baseDatos.Ofertas.FirstOrDefault(cl => cl.IdOferta == oferta.IdOferta);
                        if (of != null)
                        {
                            of.OfertaDescripcion = oferta.OfertaDescripcion;
                            of.OfertaFechaDesde = oferta.OfertaFechaDesde;
                            of.OfertaFechaHasta = oferta.OfertaFechaHasta;
                            of.OfertaPrecio = oferta.OfertaPrecio;
                            of.OfertaTitulo = oferta.OfertaTitulo;
                            if (listaImagenes != null) {
                                foreach (String url in listaImagenes)
                                {
                                    Imagen img = new Imagen();
                                    img.ImagenURL = url;
                                    img.IdOferta = oferta.IdOferta;
                                    baseDatos.Imagenes.Add(img);
                                }
                            }
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

            public Oferta BuscarOferta(int id)
            {

                try
                {
                    using (var baseDatos = new Context())
                    {
                        return baseDatos.Ofertas.Include("Imagenes").FirstOrDefault(prop => prop.IdOferta == id);
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }


            }

            public List<Oferta> ListadoOfertas()
            {
                try
                {
                    using (var baseDatos = new Context())
                    {
                        try
                        {
                            List<Oferta> ofertas = baseDatos.Ofertas.Include("Imagenes").Where(ej => ej.Activo == true).OrderBy(ej => ej.IdOferta).ToList();
                            return ofertas;
                        }
                        catch
                        {
                            List<Oferta> ofertas = baseDatos.Ofertas.Include("Imagenes").Where(ej => ej.Activo == true).OrderBy(ej => ej.OfertaTitulo).ToList();
                            return ofertas;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public List<Imagen> BuscarImagenesOferta(int idOferta)
            {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Imagenes.Where(i => i.IdOferta == idOferta).ToList();
                }
            }
            catch { return null; }
            }

            public List<Oferta> BuscarOfertaFiltros(DateTime fechaDesde, DateTime fechaHasta, string titulo)
        {
            try
            {
                List<Oferta> ofertas = new List<Oferta>();
                using (var baseDatos = new Context())
                {

                    if (fechaDesde != null && fechaHasta != null)/*Convert(DATE, OfertaFechaDesde)*/
                    {
                        ofertas = baseDatos.Ofertas.SqlQuery("select * from Oferta where Convert(DATE,OfertaFechaDesde) >= '" + fechaDesde+ "' and Convert(DATE,OfertaFechaDesde) <= '" + fechaHasta +"'").ToList();
                        //ofertas = baseDatos.Ofertas.Include("Imagenes").Where(ej => ej.OfertaFechaDesde == fechaDesde && ej.OfertaFechaHasta == fechaHasta).OrderBy(ej => ej.OfertaTitulo).ToList();
                    }
                    else if (!String.IsNullOrEmpty(titulo))
                    {
                        ofertas = baseDatos.Ofertas.SqlQuery("select * from Oferta where OfertaTitulo like '%"+titulo+" %'").ToList();
                    }
                    else if ((fechaDesde != null && fechaHasta != null) && (!String.IsNullOrWhiteSpace(titulo)))
                    {
                        ofertas = baseDatos.Ofertas.SqlQuery("select * from Oferta where (Convert(DATE,OfertaFechaDesde) >= '" + fechaDesde + "' and Convert(DATE,OfertaFechaDesde) <= '" + fechaHasta + "'" + ") and OfertaTitulo like '%" + titulo + " %'").ToList();

                    }
                    return ofertas;

                }
            }
            catch (Exception ex)
            {
                List<Oferta> ofertas = null;
                return ofertas;
            }
        }

        public List<Imagen> ListadoImagenesOferta()
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    try
                    {
                        List<Imagen> img = baseDatos.Imagenes.Where(ej => ej.Activo == true).OrderBy(ej => ej.ImagenId).ToList();
                        return img;
                    }
                    catch
                    {
                        List<Imagen> img = baseDatos.Imagenes.Where(ej => ej.Activo == true).OrderBy(ej => ej.ImagenId).ToList();
                        return img;
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

