using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Digitacion
{
    public partial class EditarEnvio : System.Web.UI.Page
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
                if (usuarioLogeado)
                {
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                        btnRecibido.Visible = true;
                        //pnlAdministrador.Visible = true;
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                        {
                            Master.CambiarSiteMap("CentralSiteMap");
                            btnRecibido.Visible = true;
                            btnAgregar.Visible = true;
                            //pnlAdministrador.Visible = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                //CargarNMuestras();
                if (Request["idEnvio"] != null)
                {
                    hdnIdEnvio.Value = Request["idEnvio"];
                    var envio = _envioBc.ObtenerEnvio(int.Parse(hdnIdEnvio.Value));
                    btnRecibido.Enabled = !envio.EnvioRecibido;
                    lblNumCorrelativo.Text = envio.CodigoEnvio.ToString();
                    CargarFormularioEditarEnvio(envio);

                    //Usuario usuarioAux = _usuarioBc.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                    int idEstablecimiento;
                    var establecimientoAux = new Establecimiento();
                    if (int.TryParse(envio.idEstablecimiento.ToString(), out idEstablecimiento))
                    {
                        establecimientoAux = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(idEstablecimiento);
                    }
                    CargarTipoEstablecimiento();
                    ddlTipoEstablecimiento.SelectedValue = establecimientoAux.idTipoEstablecimiento.ToString();
                    CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);
                    ddlEstablecimiento.SelectedValue = idEstablecimiento.ToString();
                    CargarGrillaEnvios();
                }
                else
                {
                    Response.Redirect("~/Digitacion/AdministrarEnvios.aspx");
                }
            }
        }

        #region Metodos

        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            List<Establecimiento> establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            //ddlEstablecimiento.SelectedValue = 163.ToString();
            CargarGrillaEnvios();
        }

        private void CargarFormularioEditarEnvio(Envio envio)
        {
            //var envio = _envioBc.ObtenerEnvio(idEnvio);
            txtNotas.Text = envio.Notas;
            ddlCourier.SelectedValue = envio.Courier;
            //txtCourier.Text = envio.Courier;
            txtCodigoRecibo.Text = envio.CodigoRecibo;

            string textoFechaEnvio = string.Empty;
            if (envio.FechaEnvio != null)
            {
                DateTime fechaEnvio;
                if (DateTime.TryParse(envio.FechaEnvio.ToString(), out fechaEnvio))
                {
                    textoFechaEnvio = fechaEnvio.ToShortDateString();
                }
            }
            txtFechaEnvio.Text = textoFechaEnvio;
            CargarGrillaEnvios();

        }

        private void CargarGrillaEnvios()
        {
            var listaMuestras = _muestraBc.ObtenerVistaMuestrasEnvio(int.Parse(hdnIdEnvio.Value));
            dgvMuestras.DataSource = listaMuestras;
            dgvMuestras.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + listaMuestras.Count();
            lblNumRegistros.Visible = true;
            //if (listaEnvios.Count() > 0)
            //    chkAll.Visible = true;
        }
        #endregion



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var envio = _envioBc.ObtenerEnvio(int.Parse(hdnIdEnvio.Value));
            envio.Notas = txtNotas.Text;
            envio.NumTarjetas = dgvMuestras.Rows.Count;
            envio.ModificadoPor = HttpContext.Current.User.Identity.Name;
            envio.FechaModificacion = DateTime.Now;
            envio.Courier = ddlCourier.Text; //txtCourier.Text;
            envio.CodigoRecibo = txtCodigoRecibo.Text;

            //var fechaTexto = txtFechaEnvio.Text;
            DateTime fechaEnvio;
            if (DateTime.TryParse(txtFechaEnvio.Text, out fechaEnvio))
            {
                envio.FechaEnvio = fechaEnvio;
            }
            //envio.FechaEnvio = DateTime.Parse("13/12/2020");
            _envioBc.GuardarCambiosMuestrasEnvio(envio.idEnvio);
            _envioBc.ActualizarEnvio(envio);
            Response.Redirect("~/Digitacion/AdministrarEnvios.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            _envioBc.CancelarCambiosMuestrasEnvio(int.Parse(hdnIdEnvio.Value));
            Response.Redirect("~/Digitacion/AdministrarEnvios.aspx");
        }

        protected void dgvMuestras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idMuestra;
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                if (int.TryParse(e.CommandArgument.ToString(), out idMuestra))
                {
                    if (_envioBc.QuitarMuestraEnvio(idMuestra))
                    {
                        CargarGrillaEnvios();
                    }
                    else
                    {
                        lblNumRegistros.Text = "Error al retirar la muestra";
                    }
                }

            }
        }

        protected void ddlEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaEnvios();
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        private void CargarGrillaMuestrasPendientes(int idEstablecimiento)
        {
            //int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            List<Vista_Muestra> listaMuestras = _muestraBc.ObtenerMuestrasDigitadasEdicion(idEstablecimiento);
            dgvMuestrasPendientes.DataSource = listaMuestras;
            dgvMuestrasPendientes.DataBind();
            //Total Tarjetas Consultadas: 0 
            lblNumPendientes.Text = "Tarjetas Consultadas: " + listaMuestras.Count();
            lblNumPendientes.Visible = true;
            if (listaMuestras.Count() > 0)
                chkAll.Visible = true;

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            var envio = _envioBc.ObtenerEnvio(int.Parse(hdnIdEnvio.Value));
            CargarGrillaMuestrasPendientes( int.Parse(envio.idEstablecimiento.ToString()));
            mpeAttendanceReport.Show();
        }

        #region PopUpAgregarMuestras
        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CambiarEstadoCheckBox(chkAll.Checked);
        }
        private void CambiarEstadoCheckBox(bool estadoCheckBox)
        {
            // Iterate through the Products.Rows property
            foreach (GridViewRow row in dgvMuestrasPendientes.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                    cb.Checked = estadoCheckBox;
            }
        }


        protected void btnAceptarPopup_Click(object sender, EventArgs e)
        {
            var listaCodigosMuestras = new List<string>();
            foreach (GridViewRow row in dgvMuestrasPendientes.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        string codigoMuestra = row.Cells[2].Text;
                        listaCodigosMuestras.Add(codigoMuestra);
                    }
                }

            }

            if (listaCodigosMuestras.Count > 0)
            {
                _envioBc.MarcarMuestrasAgregadasEnvio(int.Parse(hdnIdEnvio.Value), listaCodigosMuestras);
                CargarGrillaEnvios();
            }
            mpeAttendanceReport.Hide();

        }
        #endregion

        protected void dgvMuestrasPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMuestrasPendientes.PageIndex = e.NewPageIndex;
            var envio = _envioBc.ObtenerEnvio(int.Parse(hdnIdEnvio.Value));
            CargarGrillaMuestrasPendientes(int.Parse(envio.idEstablecimiento.ToString()));
        }

        protected void btnRecibido_Click(object sender, EventArgs e)
        {
            _envioBc.MarcarEnvioRecibido(HttpContext.Current.User.Identity.Name, int.Parse(hdnIdEnvio.Value));
        }

        
    }
}