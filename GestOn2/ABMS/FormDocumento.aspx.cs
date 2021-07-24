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
            /*if (!IsPostBack)
            {
                GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
                GridViewDocumentos.DataBind();
            }*/
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Validaciones que las caja de texto no estén vacias.
            if (String.IsNullOrEmpty(txtIdDocumento.Text))
            {
                lblMensaje.Text = "Debe completar todos los campos";
            }
            //Si esta todo correcto, procedo a hacer la modificación.
            else
            {
                lblMensaje.Visible = false;
                lblMensaje.Text = "";
                int id = Int32.Parse(txtIdDocumento.Text);
                string nombreDocumento = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                string ruta = lblrutaarchivo.Text;
                string Formato = lblTipoDoc.Text;
                string gradoliceal = ddlGradoLiceal.SelectedValue;
                string direccion = "";
                string nroPractico = "";
                bool esEnvio = false;
                bool EsPractico = false;
                bool EsDobleFaz = false;
                bool AColor = false;

                if (chkEsEnvio.Checked)
                {
                    direccion = txtDireccion.Text;
                    esEnvio = true;
                }
                if (chkEsPractico.Checked)
                {
                    nroPractico = txtNroPractico.Text;
                    EsPractico = true;
                }
                if (chkDobleFaz.Checked)
                    EsDobleFaz = true;
                if (chkColor.Checked)
                    AColor = true;
                int UserId = 2;

                Documento d = new Documento();
                d.IdDocumento = id;
                d.AColor = AColor;
                d.Descripcion = descripcion;
                d.Direccion = direccion;
                d.EsDobleFaz = EsDobleFaz;
                d.esEnvio = esEnvio;
                d.EsPractico = EsPractico;
                d.FechaIngreso = DateTime.Today;
                d.Formato = Formato;
                d.gradoLiceal = gradoliceal;
                d.NombreDocumento = nombreDocumento;
                d.NroPractico = nroPractico;
                d.ruta = ruta;
                d.UserId = UserId;


                bool exito = Sistema.GetInstancia().ModificarDocumento(d);
                if (exito)
                {
                    lblMensaje.Text = "Se modificó con éxito";
                    //Elimino campos luego que se modifico con éxito
                    VaciarCampos();
                }
                else
                {
                    lblMensaje.Text = "No se pudo modificar ";
                }
            }
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtIdDocumento.Text))
            {
                int id = int.Parse(txtIdDocumento.Text);
                bool exito = Sistema.GetInstancia().EliminarDocumento(id);
                if (exito)
                {
                    lblMensaje.Text = "Se elimino con éxito";
                    //Elimino campos luego que se modifico con éxito
                    VaciarCampos();
                }
                else
                {
                    lblMensaje.Text = "No se pudo eliminar ";
                }
            }
            else
            {
                lblMensaje.Text = "Complete id del docuemnto que desea eliminar";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtIdDocumento.Text))
            {
                int id = int.Parse(txtIdDocumento.Text);
                Documento d = Sistema.GetInstancia().BuscarDocumento(id);
                if (d != null)
                {
                    if (d.Activo == true)
                    {
                        
                        txtDescripcion.Text = d.Descripcion;
                        txtNombre.Text = d.NombreDocumento;
                        ddlGradoLiceal.SelectedValue = d.gradoLiceal;
                        if (d.AColor == true)
                            chkColor.Checked = true;
                        else chkColor.Checked = false;
                        if (d.EsDobleFaz)
                            chkDobleFaz.Checked = true;
                        else chkDobleFaz.Checked = false;
                        if (d.esEnvio == true)
                        {
                            chkEsEnvio.Checked = true;
                            txtDireccion.Text = d.Direccion;
                            txtDireccion.Visible = true;
                        }
                        else
                        {
                            chkEsEnvio.Checked = false;
                            txtDireccion.Text = string.Empty;
                            txtDireccion.Visible = false;
                        }
                        if (d.EsPractico == true)
                        {
                            chkEsPractico.Checked = true;
                            txtNroPractico.Text = d.NroPractico;
                            txtNroPractico.Visible = true;
                        }
                        else
                        {
                            chkEsPractico.Checked = false;
                            txtNroPractico.Text = string.Empty;
                            txtNroPractico.Visible = false;
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "El documento fue dado de baja";
                        VaciarCampos();
                    }
                }
                else
                {
                    lblMensaje.Text = "El documento buscado no éxiste en el sistema";
                    VaciarCampos();
                }
            }
            else
            {
                lblMensaje.Text = "Debe completar id del pedido";
                VaciarCampos();
            }
        }

        protected void GridViewDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    GridViewRow row = GridViewDocumentos.Rows[e.Row.RowIndex];
                    LinkButton btnEditar = row.FindControl("btnEditar") as LinkButton;
                    btnEditar.Visible = false;
                    LinkButton btnBorrar = row.FindControl("btnBorrar") as LinkButton;
                    btnBorrar.Visible = false;
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
    }
}


        
    
