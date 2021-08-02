
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
                        Configuracion conf = new Configuracion();
                        conf.Nombre = c.Nombre;
                        conf.Valor = c.Valor;
                        baseDatos.Configuraciones.Add(conf);
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
                    return baseDatos.Configuraciones.FirstOrDefault(prop => prop.Nombre == nombre);
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
