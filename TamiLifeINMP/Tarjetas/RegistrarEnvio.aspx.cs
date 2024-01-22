using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;
using System.Data;

namespace TamizajePortal.Tarjetas
{
    public partial class RegistrarEnvio : Page
    {
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        readonly EnvioBC bc = new EnvioBC();
        readonly TarjetaBC tarjetaBC = new TarjetaBC();
        UsuarioBC usuarioBC = new UsuarioBC();
        Envio envio = new Envio();

        int codPagina = 11;
        string pagina = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!usuarioBC.VerificarPermiso(out pagina, Session["idUsuario"], codPagina))
            {
                Response.Redirect(pagina);
            }
            else
            {
                Master.ActivarMenu();
            }
            //ListItem item = new ListItem("--Seleccionar--", "0");
           
            if (!Page.IsPostBack)
            {
                if (Request["idEnvio"] != null)
                {
                    CargarTipoEstablecimiento();
                    hdnRangoValido.Value = "true";
                    hdnIdEnvio.Value = Request["idEnvio"];
                    Master.CambiarTitulo("EDITAR ENVIO");
                    envio = bc.ObtenerEnvio(int.Parse(hdnIdEnvio.Value));
                    CargarEnvio(envio);

                }
                else
                {
                    CargarTipoEstablecimiento();
                    hdnRangoValido.Value = "true";
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    Master.CambiarTitulo("REGISTRAR ENVIO");
                }
            }
        }

        protected void CargarEnvio(Envio envio)
        {
            Establecimiento establecimiento = establecimientoBC.ObtenerEstablecimientoxIdEstablecimiento(envio.idEstablecimiento);
            ddlTipoEstablecimiento.SelectedValue = establecimiento.idTipoEstablecimiento.ToString();
            CargarEstablecimiento(establecimiento.idTipoEstablecimiento);
            ddlEstablecimiento.SelectedValue = establecimiento.idEstablecimiento.ToString();
            txtFechaEnvio.Text = DateTime.Parse(envio.FechaEnvio.ToString()).ToShortDateString();
            txtCodigoInicial.Text = envio.CodigoInicial;
            txtCodigoFinal.Text = envio.CodigoFinal;
            List<Vista_ListaTarjetas> listaTarjetas = new List<Vista_ListaTarjetas>();

            listaTarjetas = tarjetaBC.ObtenerListaTarjetas(envio.idEnvio,1);
            dgvResultados.DataSource = listaTarjetas;
            dgvResultados.DataBind();
            dgvResultados.Visible = true;
            tablaAux.Visible = false;
            txtCodigoInicial.Enabled = false;
            txtCodigoFinal.Enabled = true;
            btnGenerar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void ddlEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (bool.Parse(hdnRangoValido.Value))
            {
                int inicio = int.Parse(txtCodigoInicial.Text);
                int final = int.Parse(txtCodigoFinal.Text);

                CargarGrilla(inicio, final);
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                dgvResultados.Visible = true;
                tablaAux.Visible = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Envio envio = new Envio();
            envio.CodigoInicial = txtCodigoInicial.Text;
            envio.CodigoFinal = txtCodigoFinal.Text;
            envio.FechaEnvio = DateTime.Parse(txtFechaEnvio.Text);
            envio.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            envio.Estado = 1;
            envio = bc.RegistrarEnvio(envio);

            List<Tarjeta> listaTarjetas = new List<Tarjeta>();

            foreach (GridViewRow dr in dgvResultados.Rows)
	        {
		        Tarjeta tarjeta =  new Tarjeta();
                tarjeta.CodigoMuestra = dr.Cells[1].Text;
                
                tarjeta.Recibido = false;
                tarjeta.Rechazado = false;
                tarjeta.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                tarjeta.idEnvio = envio.idEnvio;
	            tarjeta.Estado = 1;
                listaTarjetas.Add(tarjeta);

	        }
            tarjetaBC.RegitrarTarjetas(listaTarjetas);
            LimpiarControles();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            
        }

        #region Metodos
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = tipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            ListItem item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            ListItem item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";
        }

        private void CargarGrilla(int inicio, int final) 
        { 
            
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("CodigoMuestra");
            dt.Columns.Add("FechaEnvio");
            dt.Columns.Add("FechaRecepcion");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Recibido");
            dt.Columns.Add("Rechazado");

            int j = 1;
            for (int i = inicio; i <= final; i++)
            {
                
                DataRow dr = dt.NewRow();
                dr["CodigoMuestra"] = i;
                dr["FechaEnvio"] = txtFechaEnvio.Text;
                dr["FechaRecepcion"] = "";
                dr["Estado"] = 1;
                dr["Recibido"] = false;
                dr["Rechazado"] = false;
                dt.Rows.Add(dr);
            }

            dgvResultados.DataSource = dt;
            dgvResultados.DataBind();
        }

        private void LimpiarControles()
        {
            ddlTipoEstablecimiento.SelectedValue = "0";
            CargarEstablecimiento(0);
            txtCodigoInicial.Text = string.Empty;
            txtCodigoFinal.Text = string.Empty;
            txtFechaEnvio.Text = string.Empty;
            dgvResultados.DataSource = string.Empty;
            dgvResultados.DataBind();

            txtCodigoInicial.Enabled = true;
            txtCodigoFinal.Enabled = true;

            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            dgvResultados.Visible = false;
            tablaAux.Visible = true;
        }
        #endregion

        protected void cusValidarRango_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string inicio = txtCodigoInicial.Text;
            string final = txtCodigoFinal.Text;

            if (tarjetaBC.ValidarRangoTarjetas(inicio, final))
            {
                args.IsValid = true;
                hdnRangoValido.Value = "true";
            }
            else
            {
                args.IsValid = false;
                hdnRangoValido.Value = "false";
            }
        }
        

    }
}