using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;

namespace GestOn2.Reportes
{
    public partial class FormReportUsuarios : System.Web.UI.Page
    {
 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                // La primera vez que carga la pagina, dejo la gráfica cargada con unas fechas random
                DateTime fch1 = Convert.ToDateTime("11/02/2019");
                DateTime fch2 = Convert.ToDateTime("11/02/2022");
                LlenarGrafica(fch1,fch2);
            }
        }

        private void LlenarGrafica(DateTime fch1, DateTime fch2) {

            int cont = 0;

            List<Reporte> reportes = Sistema.GetInstancia().ReportBestClients(fch1, fch2);

            foreach (var r in reportes)
            {
                int id = r.IdUser;
                valores[cont] = r.CantPedidos;
                nombres[cont] = r.NombreUser;
                cont++;
            }

            GraficaUsuariosPedidos.Series["Series"].Points.DataBindXY(nombres, valores);
        }


        //Arrays utilizados para la gráfica
        int[] valores = new int[5];
        string[] nombres = new string[5];

        protected void btnGraficar_Click(object sender, EventArgs e)
        {
            if (txtFecha1.Text == "" || txtFecha1.Text == null || txtFecha2.Text == "" || txtFecha2.Text == null)
            {
                lblMensaje.Text = "Ingrese fechas válidas";
                lblMensaje.Visible = true;
            }
            else {
                DateTime FechaInicio = Convert.ToDateTime(txtFecha1.Text);
                DateTime FechaFin = Convert.ToDateTime(txtFecha2.Text);
                LlenarGrafica(FechaInicio, FechaFin);
            }
        }
    }
}