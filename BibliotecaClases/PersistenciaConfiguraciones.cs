
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaClases.Clases;

namespace BibliotecaClases
{
    partial class Sistema
    {
        public bool GuardarConfiguracion(String nombre, String valor)
        {
            try
            {
                using (var baseDatos = new Context())
                {
                    Configuracion conf = new Configuracion();
                    conf.Nombre = nombre;
                    conf.Valor = valor;
                    baseDatos.Configuraciones.Add(conf);
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
