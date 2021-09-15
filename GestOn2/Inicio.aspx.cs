using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases.Clases;
using BibliotecaClases;
using System.IO;

namespace GestOn2
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarouselOferta();
        }

        private void CarouselOferta(){

            string[] imgOfertas = new string[3];
            int cont = 0;

            List<Imagen> listIMG = new List<Imagen>();

            listIMG = Sistema.GetInstancia().ListadoImagenesOferta();

            if (listIMG != null)
            {
                foreach (Imagen ima in listIMG)
                {
                    if (cont <= 3)
                    {
                        imgOfertas[cont] = ima.ImagenURL;
                        cont++;
                    }
                    else break;
                }
                string[] filesindirectory = Directory.GetFiles(Server.MapPath("/Imagenes"));
                List<String> images = new List<string>(imgOfertas.Count());

                foreach (string item in imgOfertas)
                {
                    if (item != null)
                        images.Add(String.Format("/Imagenes/{0}", System.IO.Path.GetFileName(item)));
                }

                repetidor.DataSource = images;
                repetidor.DataBind();
            }
        }

        //Itera las fotos del slider 
        protected string iterarFotos(int ImageItem)
        {
            if (ImageItem == 0)
            {
                return "active";
            }
            else {
                return "";
            }
        }
    }
}