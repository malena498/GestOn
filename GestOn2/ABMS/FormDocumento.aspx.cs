﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaClases;
using BibliotecaClases.Clases;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;

namespace GestOn2.ABMS
{
    public partial class FormDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Lleno listados cuando la pagina inicia por primera vez.
                GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
                GridViewDocumentos.DataBind();
                ListarDropUsuarios();
            }
        }

        //Ingreso un nuevo documento, dejandolo en estado pendiente para que sea gestionado.
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Documento doc = new Documento();
                string ruta = lblrutaarchivo.Text;
                if (ruta.Equals("Error"))
                {
                    lblMensaje.Text = "Error al cargar el archivo";
                    return;
                }
                else
                {
                    doc.ruta = ruta;
                    doc.Formato = lblTipoDoc.Text;
                    doc.NombreDocumento = txtNombre.Text;
                    doc.Descripcion = txtDescripcion.Text;
                    doc.gradoLiceal = int.Parse(ddlGradoLiceal.SelectedValue);
                    doc.esEnvio = false;
                    doc.EsPractico = false;
                    if (chkEsEnvio.Checked)
                    {
                        doc.Direccion = txtDireccion.Text;
                        doc.esEnvio = true;
                    }
                    if (chkEsPractico.Checked)
                    {
                        doc.NroPractico = int.Parse(txtNroPractico.Text);
                        doc.EsPractico = true;
                    }
                    if (chkDobleFaz.Checked)
                    {
                        doc.EsDobleFaz = true;
                    }
                    else
                    {
                        doc.EsDobleFaz = false;
                    }
                    if (chkColor.Checked)
                    {
                        doc.AColor = true;
                    }
                    else
                    {
                        doc.AColor = false;
                    }
                    doc.UserId = 1;
                    if (!CamposCompletos())
                    {
                        lblMensaje.Text = "Debe completar todos los campos";
                    }
                    else
                    {
                        bool exito = Sistema.GetInstancia().GuardarDocumento(doc);
                        if (exito)
                        {
                            lblMensaje.Text = "";
                            VaciarCampos();
                            llenarGrillaDocumentos();
                        }
                        else
                        {
                            lblMensaje.Text = "No anduvo";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Chequea si el documento va a requerir envío en caso que si, se le permitira ingresar una dirección
        protected void chkEsEnvio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsEnvio.Checked)
            {
                txtDireccion.Visible = true;
            }
            else
            {
                txtDireccion.Text = string.Empty;
                txtDireccion.Visible = false;
            }
        }

        //Chequea si el documento es práctico en caso que si, se le permitira ingresar el número del mismo
        protected void chkEsPractico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPractico.Checked)
            {
                txtNroPractico.Visible = true;
            }
            else
            {
                txtNroPractico.Text = string.Empty;
                txtNroPractico.Visible = false;
            }
        }



        //Una vez el usuario selecciono el archivo, deberá darle al botón subir archivo para que se guarde correctamente
        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string rrr = GuardarArchivo();
            btnUpload.Enabled = true;
        }

        //Aqui se guarda la informacion relacionada al archivo seleccinado
        protected String GuardarArchivo()
        {
            try
            {
                string filePath;
                if (fuDocs.HasFile)
                {
                    HttpPostedFile archivo = fuDocs.PostedFile;
                    string NombreArchivo = Path.GetFileName(fuDocs.PostedFile.FileName);
                    string extencion = Path.GetExtension(NombreArchivo);

                    //Guardo el nombre de la imagen para guardarlo en la propiedad despues
                    var folder = Server.MapPath("~/Documentos/");
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);

                    }
                    filePath = Path.Combine(Server.MapPath("~/Documentos/"), NombreArchivo);
                    archivo.SaveAs(filePath);
                    lblTipoDoc.Text = extencion;
                    lblrutaarchivo.Text = filePath;
                    txtNombre.Text = Path.GetFileNameWithoutExtension(fuDocs.PostedFile.FileName);
                }
                else
                {
                    return "Error";
                }
                return filePath;
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }



        //Aquí se hace lo orrespondiente a la gestión de documentos (delete y update)
        protected void GridViewDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    for (int i = 0; i < GridViewDocumentos.Rows.Count - 1; i++)
                    {
                        bool chkEsPractico1 = Convert.ToBoolean((GridViewDocumentos.Rows[i].Cells[i].FindControl("chkEsPractico1") as CheckBox).Checked);
                        if (chkEsPractico1)
                        {
                            TextBox txtNumeroPracticoDoc = GridViewDocumentos.Rows[i].Cells[i].FindControl("txtNumeroPracticoDoc") as TextBox;
                            txtNumeroPracticoDoc.Visible = true;
                        }
                    }
                }
            }
        }

        protected void GridViewDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDocumentos.EditIndex = e.NewEditIndex;
            llenarGrillaDocumentos();
        }

        protected void GridViewDocumento_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewDocumentos.Rows[e.RowIndex];
            int Id = Convert.ToInt32((row.FindControl("lblIdDocumento") as Label).Text);
            string nombre = (row.FindControl("txtNombreDoc") as TextBox).Text;
            string descripcion = (row.FindControl("txtDescripcionDoc") as TextBox).Text;
            DateTime FechaIngreso = DateTime.Parse((row.FindControl("txtFechaIngresoDoc") as TextBox).Text);
            bool AColor = Convert.ToBoolean((row.FindControl("chkAColorDoc1") as CheckBox).Checked);
            bool DobleFaz = Convert.ToBoolean((row.FindControl("chkDobleFazDoc1") as CheckBox).Checked);
            bool EsPractico = Convert.ToBoolean((row.FindControl("chkEsPractico1") as CheckBox).Checked);
            bool Adomicilio = Convert.ToBoolean((row.FindControl("chkADomicilio1") as CheckBox).Checked);
            string NroPractico = (row.FindControl("txtNumeroPracticoDoc") as TextBox).Text;
            string Dire = (row.FindControl("txtADomicilioDoc") as TextBox).Text;

            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
            Documento doc = null;
            doc = Sistema.GetInstancia().BuscarDocumento(Id);
            doc.NombreDocumento = nombre;
            doc.Descripcion = descripcion;
            doc.FechaIngreso = FechaIngreso;
            doc.AColor = AColor;
            doc.EsDobleFaz = DobleFaz;
            doc.EsPractico = EsPractico;
            doc.esEnvio = Adomicilio;
            if (EsPractico)
                doc.NroPractico = int.Parse(NroPractico);
            if (Adomicilio)
                doc.Direccion = Dire;

            bool exito = Sistema.GetInstancia().ModificarDocumento(doc);
            if (exito)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Modificado con éxito";
                GridViewDocumentos.EditIndex = -1;
                llenarGrillaDocumentos();
            }
        }

        protected void GridViewDocumento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewDocumentos.EditIndex = -1;
            llenarGrillaDocumentos();
        }

        protected void GridViewDocumento_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridViewDocumentos.Rows[e.RowIndex];
            int Id = Convert.ToInt32(GridViewDocumentos.DataKeys[e.RowIndex].Values[0]);
            bool exito = Sistema.GetInstancia().EliminarDocumento(Id);
            if (exito)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Se elimino con éxito";
                GridViewDocumentos.EditIndex = -1;
                llenarGrillaDocumentos();
            }
        }

        //Se cambia de pagina en el listado
        protected void GridViewDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDocumentos.PageIndex = e.NewPageIndex;
            llenarGrillaDocumentos();
        }



        //Esta funcion se llama al inicio del programa, listando todos los documentos activos en el sistema.
        protected void llenarGrillaDocumentos()
        {
            GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
            GridViewDocumentos.DataBind();
        }



        //Sección de filtros para los listados
        //Selecciona el filtro a aplicar al listado
        protected void ddlSeleccionaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("Fecha")) {
                DivFiltroPorDocumento.Visible = false;
                DivFiltroPorPractico.Visible = false;
                DivFiltroPorUsuario.Visible = false;
                DivFiltroPorNroCarpeta.Visible = false;
                DivFiltroPorFecha.Visible = true;
            }
            else if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("NombreDocumento"))
            {
                DivFiltroPorPractico.Visible = false;
                DivFiltroPorUsuario.Visible = false;
                DivFiltroPorFecha.Visible = false;
                DivFiltroPorNroCarpeta.Visible = false;
                DivFiltroPorDocumento.Visible = true;
            }
            else if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("Practico"))
            {
                DivFiltroPorDocumento.Visible = false;
                DivFiltroPorUsuario.Visible = false;
                DivFiltroPorFecha.Visible = false;
                DivFiltroPorNroCarpeta.Visible = false;
                DivFiltroPorPractico.Visible = true;
            }
            else if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("NombreUsuario"))
            {
                DivFiltroPorDocumento.Visible = false;
                DivFiltroPorPractico.Visible = false;
                DivFiltroPorFecha.Visible = false;
                DivFiltroPorNroCarpeta.Visible = false;
                DivFiltroPorUsuario.Visible = true;
            }
            else if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("NumeroCarpeta"))
            {
                DivFiltroPorDocumento.Visible = false;
                DivFiltroPorPractico.Visible = false;
                DivFiltroPorFecha.Visible = false;
                DivFiltroPorUsuario.Visible = false;
                DivFiltroPorNroCarpeta.Visible = true;
            }
        }

        //Filtra la grilla a través del número de carpeta asignado al usuario (Docente)
        protected void btnBuscarNroCarpeta_Click(object sender, EventArgs e)
        {
            int nroCarpeta = Convert.ToInt32(txtNroCarpeta.Text);
            Usuario u = Sistema.GetInstancia().BuscarUsuarioPorCarpeta(nroCarpeta);
            int idUser = u.UserId;
            if (idUser != 0)
            {
                List<Documento> documentos = Sistema.GetInstancia().ListadoDocumentoUser(nroCarpeta);
                if (documentos != null)
                {
                    GridViewDocumentos.DataSource = documentos;
                    GridViewDocumentos.DataBind();
                }
            }
            else {
                lblResultado.Text = "El numero de carpeta indicado no exíste o fue dado de baja";
            }
        }

        //Filtra la grilla a través de nombre de usuario seleccionado
        protected void ListPedidoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUser = Convert.ToInt32(ddlDocumentoNombreUsuario.SelectedItem.Value);
            List<Documento> documentos = Sistema.GetInstancia().ListadoDocumentoUser(idUser);
            if (documentos != null)
            {
                GridViewDocumentos.DataSource = documentos;
                GridViewDocumentos.DataBind();
            }
        }

        //Filtra los documentos por nombre del mismo
        protected void btnBuscarFiltro_Click(object sender, EventArgs e)
        {
            string nombreDoc = txtNombreDocumentoFiltro.Text;
            List<Documento> lista = Sistema.GetInstancia().ListadoDocumentoNombre(nombreDoc);
            GridViewDocumentos.DataSource = lista;
            GridViewDocumentos.DataBind();

        }

        //Filtra los documentos tanto si son práctico como cuándo no lo son
        protected void chkEsPracticoFiltro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPracticoFiltro.Checked)
            {
                bool EsPractico = true;
                List<Documento> lista = Sistema.GetInstancia().ListadoDocumentoPractico(EsPractico);
                GridViewDocumentos.DataSource = lista;
                GridViewDocumentos.DataBind();
            }
            else
            {
                bool EsPractico = false;
                List<Documento> lista = Sistema.GetInstancia().ListadoDocumentoPractico(EsPractico);
                GridViewDocumentos.DataSource = lista;
                GridViewDocumentos.DataBind();
            }
        }

        //Filtra los documentos según las fechas seleccionadas  (Desde y Hasta)
        protected void btnBuscarFecha_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = DateTime.Parse(txtFchDesde.Text);
            DateTime fechaHasta = DateTime.Parse(txtFchHasta.Text);
            List<Documento> documentos = Sistema.GetInstancia().ListadoDocumentosFechas(fechaDesde, fechaHasta);
            if (documentos != null)
            {
                GridViewDocumentos.DataSource = documentos;
                GridViewDocumentos.DataBind();
            }
        }



        //Valida que tipo de archivo selecciono el usuario para subir al sistema.
        private bool EsDoc(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsPDF(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsExcel(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }

        private bool EsImagen(HttpPostedFile archivo)
        {
            return ((archivo != null) && System.Text.RegularExpressions.Regex.IsMatch(archivo.ContentType, "image/\\S+") && (archivo.ContentLength > 0));
        }


        //Muestra Div con formulario para ingresar un nuevo documento
        protected void lnkNuevoProducto_Click(object sender, EventArgs e)
        {
            DivGridDocumentos.Visible = false;
            DivFiltros.Visible = false;
            divNuevaOferta.Visible = true;
        }

        //Muestra Div con el listado de todos los documentos y los filtros a aplicar
        protected void lnkVerDocumentos_Click(object sender, EventArgs e)
        {
            divNuevaOferta.Visible = false;
            DivGridDocumentos.Visible = true;
            DivFiltros.Visible = true;
        }



        //Lista el dropDown del filtro con nombre de los usuarios para su posterior uso
        protected void ListarDropUsuarios()
        {
            List<Usuario> datos = Sistema.GetInstancia().ListadoUsuarios();
            ddlDocumentoNombreUsuario.DataSource = datos;
            ddlDocumentoNombreUsuario.DataValueField = "UserId";
            ddlDocumentoNombreUsuario.DataTextField = "UserNombre";
            ddlDocumentoNombreUsuario.DataBind();
        }



        //Valido que no falten datos a la hora de realizar un Insert o Update en la base de datos
        protected bool CamposCompletos()
        {
            if (chkEsPractico.Checked && String.IsNullOrEmpty(txtNroPractico.Text))
                return false;
            if (chkEsEnvio.Checked && String.IsNullOrEmpty(txtDireccion.Text))
                return false;
            if (String.IsNullOrEmpty(txtDescripcion.Text) || String.IsNullOrEmpty(txtNombre.Text))
                return false;
            else
                return true;
        }



        //Vacia todos los campos del formulario
        protected void VaciarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNroPractico.Text = string.Empty;
            chkColor.Checked = false;
            chkDobleFaz.Checked = false;
            chkEsEnvio.Checked = false;
            chkEsPractico.Checked = false;
            ddlGradoLiceal.SelectedIndex = 0;
        }
    }
}
