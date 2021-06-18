﻿using System;
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
            public bool GuardarOferta(Oferta oferta, List<String> imagenes, String NombreBase)
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
                            foreach (String url in imagenes)
                            {
                                Imagen img = new Imagen();
                                img.ImagenURL = url;
                                img.IdOferta = oferta.IdOferta;
                                baseDatos.Imagenes.Add(img);

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



            public bool ModificarOferta(Oferta oferta/*, List<String> listaImagenes*/)
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
                            //foreach (String url in listaImagenes)
                            //{
                            //    Imagen img = new Imagen();
                            //    img.ImagenURL = url;
                            //    img.IdOferta = oferta.IdOferta;
                            //    baseDatos.Imagenes.Add(img);
                            //}

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
                        return baseDatos.Ofertas.FirstOrDefault(prop => prop.IdOferta == id);
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
    }
}
