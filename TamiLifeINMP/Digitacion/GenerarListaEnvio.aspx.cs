using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TamiLifeSA.Digitacion
{
    public partial class RevisarDigitacion : System.Web.UI.Page
    {
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly MuestraBC _muestraBc = new MuestraBC();
        private readonly EnvioBC _envioBc = new EnvioBC();
        private readonly Reportes _fun = new Reportes();
        private readonly UsuarioBC _usuarioBc = new UsuarioBC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated;
                bool esUsuarioEstablecimiento = false;
                if (usuarioLogeado)
                {

                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");

                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                            Master.CambiarSiteMap("CentralSiteMap");
                        else
                        {
                            esUsuarioEstablecimiento = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                if (esUsuarioEstablecimiento)
                {
                    Usuario usuarioAux = _usuarioBc.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                    Establecimiento establecimientoAux = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(usuarioAux.IdEstablecimiento);
                    CargarTipoEstablecimiento();
                    ddlTipoEstablecimiento.SelectedValue = establecimientoAux.idTipoEstablecimiento.ToString();
                    CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);
                    int idHospital = usuarioAux.IdEstablecimiento;
                    ddlTipoEstablecimiento.Enabled = false;
                    ddlEstablecimiento.SelectedValue = idHospital.ToString();
                    ddlEstablecimiento.Enabled = false;
                    CargarGrilla();
                }
                else
                {
                    CargarTipoEstablecimiento();
                    //string tipo = ddlTipoEstablecimiento.SelectedValue;
                    int tipoAux = int.Parse(ddlTipoEstablecimiento.SelectedValue);
                    CargarEstablecimiento(tipoAux);
                }

                //if (Session["idEnvio"] != null)
                //{
                //    int idEnvio;
                //    if (int.TryParse(Session["idEnvio"].ToString(), out idEnvio))
                //    {
                //        Envio envio = envioBC.ObtenerEnvio(idEnvio);
                //        List<Vista_MuestrasxEnvio> listaMuestras = envioBC.ObtenerListaMuestrasxEnvio(envio.idEnvio);
                //        byte[] bytes = fun.ReporteEnvio(envio, listaMuestras);
                //        string nombreArchivo = string.Concat(envio.Establecimiento.Nombre, "_", DateTime.Parse(envio.FechaCreacion.ToString()).ToShortDateString());
                //        Response.Clear();
                //        Response.ContentType = "application/pdf";
                //        Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".pdf");
                //        Response.ContentType = "application/pdf";
                //        Response.Buffer = true;
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.BinaryWrite(bytes);
                //        Response.End();
                //        Response.Close();
                //    }
                //}
            }

            //; = Session["idEnvio"];

        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CambiarEstadoCheckBox(chkAll.Checked);
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int idEnvio = RegistrarEnvio();
            if (idEnvio > 0)
            {
                hdnIdEnvio.Value = idEnvio.ToString();
                mpeAttendanceReport.Show(); 
            }
            
            //Sesion("idEnvio") 
            //hdnIdEnvio.Value = ;

            //Response.Redirect("~/Digitacion/GenerarListaEnvio.aspx");
            ////Page.postba
            //txtNotas.Text = string.Empty;
            //CargarGrilla();

        }

        #region Metodos

        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
        }
        private void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            List<Establecimiento> establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            //ddlEstablecimiento.SelectedValue = 163.ToString();
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            List<Vista_Muestra> listaMuestras = _muestraBc.ObtenerMuestrasDigitadasPendientesEnvio(idEstablecimiento);
            dgvMuestras.DataSource = listaMuestras;
            dgvMuestras.DataBind();
            //Total Tarjetas Consultadas: 0 
            lblNumRegistros.Text = "Tarjetas Consultadas: " + listaMuestras.Count();
            lblNumRegistros.Visible = true;
            if (listaMuestras.Count() > 0)
                chkAll.Visible = true;
        }

        private int RegistrarEnvio()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            var envio = new Envio();
            envio.FechaCreacion = DateTime.Now;
            envio.CreadoPor = HttpContext.Current.User.Identity.Name; 
            envio.idEstablecimiento = idEstablecimiento;
            envio.CodigoEnvio = _establecimientoBc.ObtenerUltimoCodigoEnvio(idEstablecimiento);
            envio.Estado = 1;
            envio.Notas = txtNotas.Text;
            /*
            DateTime fechaEnvio;
            if (DateTime.TryParse(txtFechaEnvio.Text, out fechaEnvio) )
            {
                envio.FechaEnvio = fechaEnvio;
            }

            envio.Courier = txtCourier.Text;
            envio.CodigoRecibo = txtCodigoRecibo.Text;
            */
            //List<int> listaIdMuestrasEnviadas = new List<int>();
            var listaCodigosMuestras = new List<string>();
            foreach (GridViewRow row in dgvMuestras.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        //string idMuestra = row.Cells[1].Text;
                        //listaIdMuestrasEnviadas.Add(int.Parse(idMuestra));

                        string codigoMuestra = row.Cells[2].Text;
                        listaCodigosMuestras.Add(codigoMuestra);

                    }
                }

            }
            
            if (listaCodigosMuestras.Count > 0)
            {
                envio.NumTarjetas = listaCodigosMuestras.Count;
                if (_envioBc.RegistrarEnvio(envio, listaCodigosMuestras))
                {
                    CargarGrilla();
                    txtNotas.Text = string.Empty;
                    chkAll.Checked = false;
                    lblErrorEnvio.Visible = false;
                }
                return envio.idEnvio;
            }
            else
            {
                lblErrorEnvio.Visible = true;
                return 0;
            }
            
        }
        private void CambiarEstadoCheckBox(bool estadoCheckBox)
        {
            foreach (GridViewRow row in dgvMuestras.Rows)
            {
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                    cb.Checked = estadoCheckBox;
            }
        }

        #endregion

        protected void ddlEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void btnAceptarPopup_Click(object sender, EventArgs e)
        {
            int idEnvio;
            if (int.TryParse(hdnIdEnvio.Value, out idEnvio))
            {
                Envio envio = _envioBc.ObtenerEnvio(idEnvio);
                List<Vista_MuestrasxEnvio> listaMuestras = _envioBc.ObtenerListaMuestrasxEnvio(envio.idEnvio);
                byte[] bytes = _fun.ReporteEnvio(envio, listaMuestras);
                string nombreArchivo = string.Concat(envio.Establecimiento.Nombre, "_", DateTime.Parse(envio.FechaCreacion.ToString()).ToShortDateString());
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + "cesar" + "\\nCountry: " + "Home" + "');", true);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".pdf");
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }

            mpeAttendanceReport.Hide();
        }

        
    }
}