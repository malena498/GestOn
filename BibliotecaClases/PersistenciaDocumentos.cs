﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarDocumento(Documento documento)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    documento.FechaIngreso = DateTime.Today;
                    documento.Activo = true;
                    baseDatos.Documentos.Add(documento);
                    baseDatos.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarDocumento(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Documento d = baseDatos.Documentos.FirstOrDefault(de => de.IdDocumento == id);
                    if (d != null)
                    {
                        d.Activo = false;
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

        public Documento BuscarDocumento(int id)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    return baseDatos.Documentos.FirstOrDefault(prop => prop.IdDocumento == id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public bool ModificarDocumento(Documento documento)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Documento d = baseDatos.Documentos.SingleOrDefault(cl => cl.IdDocumento == documento.IdDocumento);
                    if (d != null)
                    {
                        d.IdDocumento = documento.IdDocumento;
                        d.AColor = documento.AColor;
                        d.Activo = true;
                        d.Descripcion = documento.Descripcion;
                        d.Direccion = documento.Direccion;
                        d.EsDobleFaz = documento.EsDobleFaz;
                        d.esEnvio = documento.esEnvio;
                        d.EsPractico = documento.EsPractico;
                        d.FechaIngreso = documento.FechaIngreso;
                        d.Formato = documento.Formato;
                        d.gradoLiceal = documento.gradoLiceal;
                        d.NombreDocumento = documento.NombreDocumento;
                        d.NroPractico = documento.NroPractico;
                        d.ruta = documento.ruta;
                        d.UserId = documento.UserId;
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
    }
}