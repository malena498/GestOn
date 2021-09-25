﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarConfiguraciones(List<Configuracion> configuraciones)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    foreach (Configuracion c in configuraciones)
                    {
                        c.Activo = true;
                        baseDatos.Configuraciones.Add(c);
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

        public Configuracion BuscarConfiguracion(String nombre)
        {
            try
            {
                using (var baseDatos = new Context())
                { 
                    Configuracion c= baseDatos.Configuraciones.FirstOrDefault(prop => prop.Nombre == nombre && prop.Activo == true);
                    return c;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public bool EliminarConfiguraciones()
        {
            try
            {
                
                using (var baseDatos = new Context())
                {
                    List<Configuracion> c = baseDatos.Configuraciones.Where(ej => ej.Activo == true).ToList();
                    foreach(Configuracion co in c)
                    {
                        baseDatos.Configuraciones.Remove(co);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Configuracion> ListadoConfiguraciones()
        {
            try
            {

                using (var baseDatos = new Context())
                {
                    List<Configuracion> c = baseDatos.Configuraciones.Where(ej => ej.Activo == true).ToList();
                    return c;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
