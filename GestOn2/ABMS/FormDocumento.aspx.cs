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
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace GestOn2.ABMS
{
    public partial class FormDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] != null)
                {
                    Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(Session["IdUsuario"].ToString()));
                    if (!u.nivel.UserAdmin)
                    {
                        GridViewDocumentos.Columns[9].Visible = false;
                        DivFiltros.Visible = false;
                    }
                    else
                    {
                        GridViewDocumentos.Columns[9].Visible = true;
                        DivFiltros.Visible = true;
                    }
                    if (u.nivel.NombreNivel.Equals("Docente"))
                    {
                        CargarCursos(u.UserId);
                        divDocente.Visible = true;
                    }
                    else
                    {
                        divDocente.Visible = false;
                    }
                    //Lleno listados cuando la pagina inicia por primera vez.
                    GridViewDocumentos.DataSource = Sistema.GetInstancia().ListadoDocumentos();
                    GridViewDocumentos.DataBind();
                    ListarDropUsuarios();

                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }



        }

        //Ingreso un nuevo documento, dejandolo en estado pendiente para que sea gestionado.
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Documento doc = new Documento();
                string ruta = Session["RutaDocumento"].ToString();
                if (ruta.Equals("Error") || ruta.Equals(""))
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Error al cargar el archivo";
                    return;
                }
                else
                {
                    doc.ruta = ruta;
                    doc.Formato = Session["TipoDoc"].ToString();
                    doc.NombreDocumento = Session["NombreDoc"].ToString();
                    doc.Descripcion = txtDescripcion.Text;
                    doc.gradoLiceal = int.Parse(ddlCurso.SelectedValue);
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
                    string user = Session["IdUsuario"].ToString();
                    doc.UserId = int.Parse(user);
                    doc.estado = "Pendiente";
                    if (!CamposCompletos())
                    {
                        lblMensaje.Visible = true;
                        lblMensaje.Text = "Debe completar todos los campos";
                    }
                    else
                    {
                        int id = Sistema.GetInstancia().GuardarDocumento(doc);
                        if (id > 0)
                        {
                            Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                            Configuracion c2 = Sistema.GetInstancia().BuscarConfiguracion("CorreoAdmin");
                            Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(user));

                            bool ex = Sistema.GetInstancia().GuardarNotificacionDocumento(int.Parse(user), u.UserNombre, "NUEVO", id);
                            bool mail = EnviarMailNuevoDoc(c.Valor, c2.Valor, u);
                            if (mail)
                            {
                                lblMensaje.Visible = false;
                                lblMensaje.Text = "EXITO";
                            }
                            else
                            {
                                lblMensaje.Visible = false;
                                lblMensaje.Text = "ERROR";
                            }
                            VaciarCampos();
                            llenarGrillaDocumentos();
                            lblMensaje.Visible = true;
                            lblMensaje.Text = "Guardado con exito";

                        }
                        else
                        {
                            lblMensaje.Visible = true;
                            lblMensaje.Text = "No se pudo guardar";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMensaje.Visible = true;
                //lblMensaje.Text = "ERROR EN LA CARGA DEL ARCHIVO";
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
                    txtNombre.Text = Path.GetFileNameWithoutExtension(fuDocs.PostedFile.FileName);
                    Session["NombreDoc"] = txtNombre.Text;
                    Session["RutaDocumento"] = filePath;
                    Session["TipoDoc"] = extencion;
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
            if (e.Row.RowType == DataControlRowType.DataRow &&
       (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
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

        protected void GridViewDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDocumentos.EditIndex = e.NewEditIndex;
            llenarGrillaDocumentos();
        }

        protected void GridViewDocumento_RowUpdated(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lnkNuevoProducto.Visible = false;
                btnActualizar.Enabled = true;
                fuDocs.Visible = false;
                btnSubir.Visible = false;
                GridViewDocumentos.Visible = false;
                btnUpload.Visible = false;
                btnActualizar.Visible = true;
                divNuevaOferta.Visible = true;
                GridViewRow row = GridViewDocumentos.Rows[e.RowIndex];
                int Id = Convert.ToInt32((row.FindControl("lblIdDocumento") as Label).Text);
                CargarDatos(Id);
             
            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Excepción no controlada";
            }
        }
        protected void CargarDatos(int idDocumento)
        {
            Documento d = Sistema.GetInstancia().BuscarDocumento(idDocumento);
            txtID.Text = d.IdDocumento.ToString();
            txtDescripcion.Text = d.Descripcion;
            txtNombre.Text = d.NombreDocumento;
            if (d.AColor)
            {
                chkColor.Checked = true;
            }
            else
            {
                chkColor.Checked = false;
            }
            if (d.EsDobleFaz)
            {
                chkDobleFaz.Checked = true;
            }
            else
            {
                chkDobleFaz.Checked = false;

            }
            if (d.EsPractico)
            {
                chkEsPractico.Checked = true;
                txtNroPractico.Text = d.NroPractico.ToString();
            }
            if (d.esEnvio)
            {
                chkEsEnvio.Checked = true;
                txtDireccion.Visible = true;
                txtDireccion.Text = d.Direccion;
            }
            else
            {
                chkEsEnvio.Checked = false;
                txtDireccion.Visible = false;
                txtDireccion.Text = "";
            }
            if (d.gradoLiceal != 0)
            {
                divDocente.Visible = true;
                CargarCursos(d.UserId);
                txtNroPractico.Visible = true;
                ddlCurso.SelectedValue = d.gradoLiceal.ToString();
                ddlCurso.Visible = true;
                
            }
            else
            {
                ddlCurso.Visible = false;
                divDocente.Visible = false;
               
                txtNroPractico.Visible = false;
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
            Documento d = Sistema.GetInstancia().BuscarDocumento(Id);
            bool exito = Sistema.GetInstancia().EliminarDocumento(Id);
            string user = Session["IdUsuario"].ToString();
            Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(user));
            
            if (exito)
            {
               
                bool ex = Sistema.GetInstancia().GuardarNotificacionDocumento(int.Parse(user), u.UserNombre, "CANCELACION", Id);
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                Configuracion c2 = Sistema.GetInstancia().BuscarConfiguracion("CorreoAdmin");
                bool mail = EnviarMailCancelarDoc(c.Valor, c2.Valor, u,d );
                if (mail)
                {
                    lblResultado.Visible = false;
                    lblResultado.Text = "EXITO";
                }
                else
                {
                    lblResultado.Visible = false;
                    lblResultado.Text = "ERROR";
                }
                lblResultado.Text = "Se elimino con éxito";
                lblResultado.Visible = true;
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
            if (ddlSeleccionaFiltro.SelectedItem.Value.Equals("Fecha"))
            {
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
            else
            {
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
            DivGeneral.Visible = false;
            divNuevaOferta.Visible = true;
        }

        //Muestra Div con el listado de todos los documentos y los filtros a aplicar
        protected void lnkVerDocumentos_Click(object sender, EventArgs e)
        {
            divNuevaOferta.Visible = false;
            DivGridDocumentos.Visible = true;
            Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(Session["IdUsuario"].ToString()));
            if (!u.nivel.UserAdmin)
            {
                GridViewDocumentos.Columns[9].Visible = false;
                DivFiltros.Visible = false;
                DivGeneral.Visible = true;
            }
            else
            {
                GridViewDocumentos.Columns[9].Visible = true;
                DivFiltros.Visible = true;
                DivGeneral.Visible = true;
            }
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
            ddlCurso.SelectedIndex = 0;
        }

        /* Envío de Email utilizado para notificar el ingreso de un documento */
        protected bool EnviarMailNuevoDoc(String mailEmpresa, String mailDestino, Usuario usuario)
        {
            try
            {
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("Contraseñamail");
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add(mailDestino); //Correo destino?
                correo.Subject = "Se ha ingresado un nuevo documento."; //Asunto
                correo.Body = "El usuario:" + usuario.UserNombre + "ha realizado el ingreso de un nuevo documento. "; //Mensaje del correo
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 25; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa, c.Valor);//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /* Envío de Email utilizado para notificar la modificación de un documento */
        protected bool EnviarMailModificarDoc(String mailEmpresa, String mailDestino, Usuario usuario, Documento doc)
        {
            try
            {
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("Contraseñamail");
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(mailDestino); //Correo destino?
            correo.Subject = "Se modificado un documento."; //Asunto
            correo.Body = "El usuario:" + usuario.UserNombre + "ha realizado cambios en el documento " + doc.NombreDocumento; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 25; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa,c.Valor);//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);
            return true;
            }
            catch (Exception ex)
            {
                return false;
            }
}

        protected bool EnviarMailCancelarDoc(String mailEmpresa, String mailDestino, Usuario usuario, Documento doc)
        {
            try
            {
                Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("Contraseñamail");
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(mailEmpresa, "Bertinat Papeleria", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add(mailDestino); //Correo destino?
                correo.Subject = "Se modificado un documento."; //Asunto
                correo.Body = "El usuario:" + usuario.UserNombre + "ha cancelado el documento " + doc.NombreDocumento; //Mensaje del correo
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 25; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential(mailEmpresa, c.Valor);//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void CargarCursos(int idUsuario)
        {
            List<MateriaCursoDocente> cursosDocente = Sistema.GetInstancia().ListadoMateriaCursoDocente(idUsuario);
            DataTable table = new DataTable();
            DataColumn c1 = new DataColumn();
            c1.ColumnName = "IDCURSO";
            table.Columns.Add(c1);
            DataColumn c2 = new DataColumn();
            c2.ColumnName = "CURSO";
            table.Columns.Add(c2);
            foreach (MateriaCursoDocente c in cursosDocente)
            {
                Curso cu = Sistema.GetInstancia().BuscarCurso(c.IdCurso);
                DataRow r = table.NewRow();
                r["IDCURSO"] = cu.Id;
                r["CURSO"] = cu.Grado + "-" + cu.Grupo;
                table.Rows.Add(r);
            }
            ddlCurso.DataSource = table;
            ddlCurso.DataTextField = "CURSO";
            ddlCurso.DataValueField = "IDCURSO";
            ddlCurso.DataBind();

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            DateTime FechaIngreso = DateTime.Now;
            bool AColor = chkColor.Checked;
            bool DobleFaz = chkDobleFaz.Checked;
            bool EsPractico = chkEsPractico.Checked;
            bool Adomicilio = chkEsEnvio.Checked;
            string NroPractico = "";
            string direccion = "";
            if (chkEsPractico.Checked) {
                 NroPractico = txtNroPractico.Text;
            }
            if (chkEsEnvio.Checked)
            {
                direccion = txtDireccion.Text;
            }
               

            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;

            if (id.Equals(0) || nombre.Equals("") || descripcion.Equals("") || FechaIngreso.Equals(null) )
            {
                if (chkEsEnvio.Checked)
                {
                    if (direccion.Equals(""))
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = "Debe completar todos los campos";
                    }
                }
                    lblResultado.Visible = true;
                lblResultado.Text = "Debe completar todos los campos";
            }
            else
            {
                Documento doc = null;
                doc = Sistema.GetInstancia().BuscarDocumento(id);
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
                    doc.Direccion = direccion;

                bool exito = Sistema.GetInstancia().ModificarDocumento(doc);
                if (exito)
                {
                    Configuracion c = Sistema.GetInstancia().BuscarConfiguracion("CorreoEmpresa");
                    Configuracion c2 = Sistema.GetInstancia().BuscarConfiguracion("CorreoAdmin");
                    string user = Session["IdUsuario"].ToString();
                    Usuario u = Sistema.GetInstancia().BuscarUsuario(int.Parse(user));
                    bool ex = Sistema.GetInstancia().GuardarNotificacionDocumento(int.Parse(user), u.UserNombre, "MODIFICACION", id);
                    bool mail = EnviarMailModificarDoc(c.Valor, c2.Valor, u, doc);
                    if (mail)
                    {
                        lblMensaje.Visible = false;
                        lblMensaje.Text = "EXITO";
                    }
                    else
                    {
                        lblMensaje.Visible = false;
                        lblMensaje.Text = "ERROR";
                    }
                    lblResultado.Visible = true;
                    lblResultado.Text = "Modificado con éxito";
                    GridViewDocumentos.EditIndex = -1;
                    llenarGrillaDocumentos();
                    btnActualizar.Enabled = false;
                    fuDocs.Visible = true;
                    btnSubir.Visible = true;
                    GridViewDocumentos.Visible = true;
                    btnUpload.Visible = true;
                    btnActualizar.Visible = false;
                    divNuevaOferta.Visible = false;
                    lnkNuevoProducto.Visible = true;
                }
            }
        }
    }
}
