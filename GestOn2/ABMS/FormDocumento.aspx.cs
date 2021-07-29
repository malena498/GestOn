using System;
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
                GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
                GridViewDocumentos.DataBind();
            }
        }

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
                    doc.gradoLiceal = ddlGradoLiceal.SelectedValue;
                    doc.esEnvio = false;
                    doc.EsPractico = false;
                    if (chkEsEnvio.Checked)
                    {
                        doc.Direccion = txtDireccion.Text;
                        doc.esEnvio = true;
                    }
                    if (chkEsPractico.Checked)
                    {
                        doc.NroPractico = txtNroPractico.Text;
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

        protected void VaciarCampos() {
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
                    lblTipoDoc.Text =  extencion;
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

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string rrr = GuardarArchivo();
            btnUpload.Enabled = true;
        }

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

        protected void llenarGrillaDocumentos()
        {
            GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
            GridViewDocumentos.DataBind();
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
                doc.NroPractico = NroPractico;
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

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridViewDocumentos.PageIndex = e.NewPageIndex;
            this.llenarGrillaDocumentos();
        }

        protected void GridViewDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDocumentos.PageIndex = e.NewPageIndex;
            llenarGrillaDocumentos();
        }

        protected void btnBuscarFiltro_Click(object sender, EventArgs e)
        {
            string nombreP = txtNombreProductoFiltro.Text;
            List<Documento> lista = Sistema.GetInstancia().ListadoDocumentoNombre(nombreP);
            GridViewDocumentos.DataSource = lista;
            GridViewDocumentos.DataBind();

        }

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

        protected void lnkNuevoProducto_Click(object sender, EventArgs e)
        {
            DivGridDocumentos.Visible = false;
            DivFiltros.Visible = false;
            divNuevaOferta.Visible = true;
        }

        protected void lnkVerDocumentos_Click(object sender, EventArgs e)
        {
            divNuevaOferta.Visible = false;
            DivGridDocumentos.Visible = true;
            DivFiltros.Visible = true;
        }
    }
}
