using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using BE;
using BC;

namespace TamiLifeSA.Establecimientos
{
    public partial class AdministrarRechazadasAlteradas : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly TipoEstablecimientoBC TipoEstablecimientoBC = new TipoEstablecimientoBC();
        private readonly EstablecimientoBC EstablecimientoBC = new EstablecimientoBC();
        private readonly MuestraBC muestraBC = new MuestraBC();
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();

        //Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;
                if (usuarioLogeado)
                {
                    CargarTipoEstablecimiento();
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                        {
                            Master.CambiarSiteMap("CentralSiteMap");
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                CargarGrillaResultados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
                CargarGrillaRechazadas(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
                //hdnIdEstablecimiento.Value = idEstablecimiento.ToString();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillas();
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }
        protected void btnRecibidas_Click(object sender, EventArgs e)
        {

            var listaCodigosMuestras = new List<string>();
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        //string idMuestra = row.Cells[1].Text;
                        //listaIdMuestrasEnviadas.Add(int.Parse(idMuestra));

                        string codigoMuestra = row.Cells[1].Text;
                        listaCodigosMuestras.Add(codigoMuestra);

                    }
                }
            }

            foreach (GridViewRow row in dgvRechazadas.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        //string idMuestra = row.Cells[1].Text;
                        //listaIdMuestrasEnviadas.Add(int.Parse(idMuestra));

                        string codigoMuestra = row.Cells[1].Text;
                        listaCodigosMuestras.Add(codigoMuestra);

                    }
                }
            }

            if (listaCodigosMuestras.Count > 0)
            {
                muestraBC.MarcarMuestrasRecibidas(HttpContext.Current.User.Identity.Name, listaCodigosMuestras);
            }

            CargarGrillas();

        }
        protected void dgvRechazadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string codigoMuestra = e.CommandArgument.ToString();
                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(codigoMuestra);
                //List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(muestraCompleta.Muestra.CodigoInternoLab);
                CargarDatosMuestra(muestraCompleta);
                //dgvResultado.DataSource = listaResultados;
                //dgvResultado.DataBind();
                dgvResultado.Visible = false;
                lblTextoResultado.Visible = false;
                mpeAttendanceReport.Show();

            }
        }
        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string codigoMuestra = e.CommandArgument.ToString();
                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(codigoMuestra);
                List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
                CargarDatosMuestra(muestraCompleta);
                dgvResultado.DataSource = listaResultados;
                dgvResultado.DataBind();
                dgvResultado.Visible = true;
                lblTextoResultado.Visible = true;
                mpeAttendanceReport.Show();

            }
            if (e.CommandName.CompareTo("reporte") == 0)
            {
                string codigoMuestra = e.CommandArgument.ToString();
                var fun = new Reportes();
                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = reporteBC.ObtenerResultados(muestraCompleta.Muestra.CodigoInternoLab);
                List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
                byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + codigoMuestra + ".pdf");
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
        
        //Metodos
        private void CargarGrillas()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;
            string apellidosNeonato = txtApellidosNeonato.Text;
            string apellidosMadre = txtApellidosMadre.Text;
            string dni = txtDNI.Text;

            CargarGrillaResultados(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
            CargarGrillaRechazadas(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
        }
        private void CargarGrillaResultados(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            List<Vista_ResultadosEstablecimiento> listaResultadosAlterados = _resultadoBc.ObtenerResultadosHospitalAlterados(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
            dgvResultados.DataSource = listaResultadosAlterados;
            dgvResultados.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + listaResultadosAlterados.Count;
            lblNumRegistros.Visible = true;
        }
        private void CargarGrillaRechazadas(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            
            List<Vista_Muestra> listaMuestrasRechazadas = muestraBC.ObtenerListaMuestrasRechazadasEstablecimiento(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
            dgvRechazadas.DataSource = listaMuestrasRechazadas;
            dgvRechazadas.DataBind();
            lblNumRechazados.Text = "Registros Consultados: " + listaMuestrasRechazadas.Count;
            lblNumRechazados.Visible = true;
        }
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = TipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            //if (!(hdnSede.Value.CompareTo("1") == 0))
            //{
            //    ListItem item = new ListItem("--Seleccionar--", "0");
            //    ddlTipoEstablecimiento.Items.Insert(0, item);
            //    ddlTipoEstablecimiento.SelectedValue = "0";
            //}

        }
        private void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = EstablecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            //if (!(hdnSede.Value.CompareTo("1") == 0))
            //{
            //    ListItem item = new ListItem("--Seleccionar--", "0");
            //    ddlEstablecimiento.Items.Insert(0, item);
            //    ddlEstablecimiento.SelectedValue = "0";
            //}

        }
        private void CargarDatosMuestra(MuestraCompletaBE muestraCompleta)
        {
            //Madre
            lblMadre.Text = string.Concat(muestraCompleta.Madre.Nombres, " , ", muestraCompleta.Madre.Apellidos);
            lblDireccion.Text = muestraCompleta.Madre.Direccion;
            lblDNI.Text = muestraCompleta.Madre.DNI;
            lblTelefono1.Text = muestraCompleta.Madre.Telefono1;
            lblTelefono2.Text = muestraCompleta.Madre.Telefono2;
            lblEdadMadre.Text = muestraCompleta.Madre.Edad.ToString();

            //Neonato
            lblNeonato.Text = string.Concat(muestraCompleta.Neonato.Nombres, " , ", muestraCompleta.Neonato.Apellidos);
            DateTime fechaNacimiento;
            if (DateTime.TryParse(muestraCompleta.Neonato.FechaNacimiento.ToString(), out fechaNacimiento))
            {
                lblFechaNacimiento.Text = fechaNacimiento.ToShortDateString();
                lblHoraNacimiento.Text = fechaNacimiento.ToShortTimeString();
            }
            lblEdadGestacional.Text = muestraCompleta.Neonato.EdadGestacional.ToString();
            lblSexo.Text = muestraCompleta.Neonato.Sexo == 2 ? "Masculino" : "Femenino";
            bool esPrematuro;
            if (bool.TryParse(muestraCompleta.Neonato.EsPrematuro.ToString(), out esPrematuro))
            {
                lblPrematuro.Text = esPrematuro ? "SI" : "NO";
            }
            //decimal peso;
            lblPeso.Text = muestraCompleta.Neonato.Peso.ToString();
            lblTalla.Text = muestraCompleta.Neonato.Talla.ToString();

            //Muestra
            lblNMuestra.Text = muestraCompleta.Muestra.NumMuestra.ToString();
            bool esTalon;
            if (bool.TryParse(muestraCompleta.Muestra.EsTalon.ToString(), out esTalon))
            {
                lblTalon.Text = esTalon ? "SI" : "NO";
            }
            bool esTransfundido;
            if (bool.TryParse(muestraCompleta.Muestra.EsTransfundido.ToString(), out esTransfundido))
            {
                lblTransfundido.Text = esTransfundido ? "SI" : "NO";
            }
            lblCodigoMuestra.Text = muestraCompleta.Muestra.CodigoMuestra;
            lblCodigoCorrelativo.Text = muestraCompleta.Muestra.CodigoInternoLab;

            lblEstablecimiento.Text = muestraCompleta.Muestra.Establecimiento.TipoEstablecimiento.Nombre + "-" + muestraCompleta.Muestra.Establecimiento.Nombre;

            lblAfiliacion.Text = "";//dtPaciente.Rows[0]["ptnSocialSecurity"].ToString();
            lblTomadoPor.Text = muestraCompleta.Muestra.TomadoPor;
            lblNotas.Text = muestraCompleta.Muestra.Notas;
            //Establecimiento establecimiento = establecimientoBC.ObtenerEstablecimientoxIdEstablecimiento(int.Parse(muestraCompleta.Muestra.idEstablecimiento.ToString()));
            //TipoEstablecimientoBC tipoEstablecimientoBc = new TipoEstablecimientoBC();
            //TipoEstablecimiento tipoEstablecimiento =
            //    tipoEstablecimientoBc.ObtenerTipoEstablecimientoxId(establecimiento.idTipoEstablecimiento);
            ////lblEstablecimiento.Text = string.Concat(tipoEstablecimiento.Nombre,"-",establecimiento.Nombre);
            //lblEstablecimiento.Text = string.Concat(establecimiento.Nombre, "(", tipoEstablecimiento.Nombre, ")");



            DateTime fechaRecepcion;
            if (muestraCompleta.Muestra.FechaRecepcion != null)
            {
                if (DateTime.TryParse(muestraCompleta.Muestra.FechaRecepcion.ToString(), out fechaRecepcion))
                {
                    lblFechaRecepcion.Text = fechaRecepcion.ToShortDateString();
                }

            }
            //lblFechaRecepcion.Text = "";//DateTime.TryParse(dtPaciente.Rows[0]["prMotherDOB"].ToString(), out fechaRecepcion) ? fechaRecepcion.ToString("dd/MM/yyyy") : "ND";

            DateTime fechaToma;
            if (muestraCompleta.Muestra.FechaToma != null)
            {
                if (DateTime.TryParse(muestraCompleta.Muestra.FechaToma.ToString(), out fechaToma))
                {
                    lblFechaToma.Text = fechaToma.ToShortDateString();
                    lblHoraToma.Text = fechaToma.ToShortTimeString();
                }

            }
            //lblFechaToma.Text = muestraCompleta.Muestra.FechaToma.ToString();
            //DateTime.TryParse(dtPaciente.Rows[0]["spcTimeCollected"].ToString(), out fechaToma) ? fechaToma.ToString("dd/MM/yyyy") : "ND";
            //hdnPatientId.Value = dtPaciente.Rows[0]["PatientID"].ToString();
            //--ViewState["idPatient"] = dtPaciente.Rows[0]["PatientID"].ToString();
            //lblFechaToma.Text = dtPaciente.Rows[0]["spcTimeCollected"].ToString();
        }

        protected void btnRetirar_Click(object sender, EventArgs e)
        {
            var listaCodigosMuestras = new List<string>();
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        //string idMuestra = row.Cells[1].Text;
                        //listaIdMuestrasEnviadas.Add(int.Parse(idMuestra));

                        string codigoMuestra = row.Cells[1].Text;
                        listaCodigosMuestras.Add(codigoMuestra);

                    }
                }
            }

            foreach (GridViewRow row in dgvRechazadas.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        //string idMuestra = row.Cells[1].Text;
                        //listaIdMuestrasEnviadas.Add(int.Parse(idMuestra));

                        string codigoMuestra = row.Cells[1].Text;
                        listaCodigosMuestras.Add(codigoMuestra);

                    }
                }
            }

            if (listaCodigosMuestras.Count > 0)
            {
                muestraBC.MarcarMuestrasRecibidRetiradas(HttpContext.Current.User.Identity.Name, listaCodigosMuestras);
            }

            CargarGrillas();
        }
    }
}