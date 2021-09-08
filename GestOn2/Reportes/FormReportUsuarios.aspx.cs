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
                if (Session["IdUsuario"] != null)
                {
                    String idUsuarioLogueado = Session["IdUsuario"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
                // La primera vez que carga la pagina, dejo la gráfica cargada con unas fechas random
                txtFecha1.Text = "2019-11-02";
                txtFecha2.Text = "2021-11-02";
                DateTime fch1 = Convert.ToDateTime("2019-11-02");
                DateTime fch2 = Convert.ToDateTime("2021-11-02");
                LlenarGrafica(fch1,fch2);
            }
        }

        private void LlenarGrafica(DateTime fch1, DateTime fch2) {

            int cont = 0;

            String filtro = ddlSeleccionaFiltro.SelectedValue;

            List<Reporte> reportes = null;
            List<ReporteProductosMasVendidos> reporte = null;
            if (filtro == "Productos")
            {
                reportes = Sistema.GetInstancia().ReportCliProducto(fch1, fch2);
            }
            else if (filtro == "Documentos") {
                reportes = Sistema.GetInstancia().ReportCliDocumentos(fch1, fch2);
            }
            else if (filtro == "Ganancia")
            {
                reportes = Sistema.GetInstancia().ReportCliGastos(fch1, fch2);
            }
            else if (filtro == "ProductoVendido")
            {
                reporte = Sistema.GetInstancia().ReporteProductosMasVendidos(fch1, fch2);
            }

            if (reportes != null)
            {
                foreach (var r in reportes)
                {
                    int id = r.USERID;
                    valores[cont] = r.CANTIDAD;
                    nombres[cont] = r.USERNOMBRE;
                    cont++;
                }
            }
            else if (reporte != null)
            {
                foreach (var r in reporte)
                {
                    int id = r.ProductoId;
                    valores[cont] = int.Parse(r.Cantidad.ToString());
                    nombres[cont] = r.ProductoNombre;
                    cont++;
                }
            }
            else {
                lblMensaje.Text = "No hay reporte para mostrar";
                lblMensaje.Visible = true;
            }

            GraficaUsuariosPedidos.Series["Series"].Points.DataBindXY(nombres, valores);
        }


        //Arrays utilizados para la gráfica
        int[] valores = new int[5];
        string[] nombres = new string[5];

        protected void btnGraficar_Click(object sender, EventArgs e)
        {
            DateTime fch1 = Convert.ToDateTime(txtFecha1.Text);
            DateTime fch2 = Convert.ToDateTime(txtFecha2.Text);
            if (txtFecha1.Text == "" || txtFecha1.Text == null || txtFecha2.Text == "" || txtFecha2.Text == null)
            {
                lblMensaje.Text = "Ingrese fechas válidas";
                lblMensaje.Visible = true;
            }
            else if (fch1 > fch2) {
                lblMensaje.Text = "La fecha de inicio no puede ser mayor a la de fin";
                lblMensaje.Visible = true;
            }
            else
            {
                DateTime FechaInicio = Convert.ToDateTime(txtFecha1.Text);
                DateTime FechaFin = Convert.ToDateTime(txtFecha2.Text);
                LlenarGrafica(FechaInicio, FechaFin);
            }
        }
        protected void ddlSeleccionaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fch1 = Convert.ToDateTime(txtFecha1.Text);
            DateTime fch2 = Convert.ToDateTime(txtFecha2.Text);
            if (txtFecha1.Text == "" || txtFecha1.Text == null || txtFecha2.Text == "" || txtFecha2.Text == null)
            {
                lblMensaje.Text = "Ingrese fechas válidas";
                lblMensaje.Visible = true;
            }
            else if (fch1 > fch2)
            {
                lblMensaje.Text = "La fecha de inicio no puede ser mayor a la de fin";
                lblMensaje.Visible = true;
            }
            else
            {
                DateTime FechaInicio = Convert.ToDateTime(txtFecha1.Text);
                DateTime FechaFin = Convert.ToDateTime(txtFecha2.Text);
                LlenarGrafica(FechaInicio, FechaFin);
            }
        }
            

    }
}