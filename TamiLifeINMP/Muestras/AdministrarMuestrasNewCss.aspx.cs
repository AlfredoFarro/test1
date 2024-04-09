using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Muestras
{
    public partial class AdministrarMuestrasNewCss : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly MuestraBC _muestraBc = new MuestraBC();
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
        private readonly Reportes _rep = new Reportes();
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();

        //Eventos ----------------------------------------------------------------
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;
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
                            Response.Redirect("~/Default.aspx");
                        }
                    }

                    dgvMuestras.PageSize = int.Parse(ConfigurationManager.AppSettings["numpaginacion"]);
                    hdnSede.Value = "1";
                    CargarTipoEstablecimiento();
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //int estado = int.Parse(ddlEstado.SelectedValue);
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            CargarGrilla(listaMuestras);
        }

        private void DescargarReporteMuestra(string codigoMuestra)
        {
            MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
            List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
            byte[] bytes = _rep.ReporteResultados(muestraCompleta, listaResultados);
            //byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + codigoMuestra + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
        protected void dgvMuestras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("editar") == 0)
            {
                Response.Redirect("~/Muestras/EditarMuestra.aspx?CodigoMuestra=" + e.CommandArgument);
            }
            if (e.CommandName.CompareTo("reporte") == 0)
            {
                DescargarReporteMuestra(e.CommandArgument.ToString());
            }
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                string codigoMuestra = e.CommandArgument.ToString();

                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
                CargarDatosMuestra(muestraCompleta);
                dgvResultados.DataSource = listaResultados;
                dgvResultados.DataBind();

                mpeAttendanceReport.Show();
            }
            if (e.CommandName.CompareTo("agregar") == 0)
            {
                Response.Redirect("~/Muestras/AgregarMuestraAux.aspx?CodigoMuestra=" + e.CommandArgument);
            }
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }
        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMuestras.PageIndex = e.NewPageIndex;
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            CargarGrilla(listaMuestras);
        }

        protected void btnReportePopup_Click(object sender, EventArgs e)
        {
            DescargarReporteMuestra(lblCodigoMuestra.Text);
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }
        protected void btnReporte_Click(object sender, EventArgs e)
        {
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            string establecimiento = ddlEstablecimiento.SelectedItem.Text;
            byte[] bytes = _rep.ReporteResultadosxEstablecimiento(listaMuestras);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename= ListaReportes_" + establecimiento + ".pdf");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
        protected void btnConsolidado_Click(object sender, EventArgs e)
        {
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            string rango = txtFechaInicioNac.Text + "-" + txtFechaFinNac.Text;

            string establecimiento = ddlEstablecimiento.SelectedItem.Text;
            string tipoEstablecimiento = ddlTipoEstablecimiento.SelectedItem.Text;
            string auxEstablecimiento = tipoEstablecimiento + "-" + establecimiento;
            byte[] bytes = _rep.ReporteResultadosxEstablecimiento(auxEstablecimiento, rango, listaMuestras);
            //byte[] bytes = fun.reporteResultadosxEstablecimientoPDF(listaMuestras);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename= Consolidad_" + establecimiento + ".pdf");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
        #endregion

        //Metodos ----------------------------------------------------------------
        #region Metodos
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            if ((hdnSede.Value.CompareTo("1") == 0))
            {
                var item = new ListItem("--Seleccionar--", "0");
                ddlTipoEstablecimiento.Items.Insert(0, item);
                ddlTipoEstablecimiento.SelectedValue = "0";
            }

        }
        private void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            if (tipoEstablecimiento == 0)
            {
                if (hdnSede.Value.CompareTo("1") == 0)
                {
                    var item = new ListItem("--Seleccionar--", "0");
                    ddlEstablecimiento.Items.Insert(0, item);
                    ddlEstablecimiento.SelectedValue = "0";
                }
            }
        }

        /* Nombre: CargarDatosMuestra
         * Argumentos: la clase muestraCompleta contiene todos los datos demograficos del paciente al cual corresponde la muestra.
         * Descripción: Carga los datos de la muestra seleccionada en el popup de detalle de información de la muestra  
         */
        private void CargarDatosMuestra(MuestraCompletaBE muestraCompleta)
        {
            //Madre
            lblMadre.Text = string.Concat(muestraCompleta.Madre.Nombres, " , ", muestraCompleta.Madre.Apellidos);
            lblDireccion.Text = muestraCompleta.Madre.Direccion;
            lblDNI.Text = muestraCompleta.Madre.DNI;
            lblTelefono1.Text = muestraCompleta.Madre.Telefono1;
            //lblTelefono2.Text = muestraCompleta.Madre.Telefono2;
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
            //if (bool.TryParse(muestraCompleta.Neonato.EsPrematuro.ToString(), out esPrematuro))
            //{
            //    lblPrematuro.Text = esPrematuro ? "SI" : "NO";
            //}
            //decimal peso;
            lblPeso.Text = string.Format("{0:0.##}", muestraCompleta.Neonato.Peso);
            lblTalla.Text = muestraCompleta.Neonato.Talla.ToString();

            //Muestra
            lblNMuestra.Text = muestraCompleta.Muestra.NumMuestra.ToString();
            //bool esTalon;
            //if (bool.TryParse(muestraCompleta.Muestra.EsTalon.ToString(), out esTalon))
            //{
            //    lblTalon.Text = esTalon ? "SI" : "NO";
            //}
            bool esTransfundido;
            if (bool.TryParse(muestraCompleta.Muestra.EsTransfundido.ToString(), out esTransfundido))
            {
                lblTransfundido.Text = esTransfundido ? "SI" : "NO";
            }
            lblCodigoMuestra.Text = muestraCompleta.Muestra.CodigoMuestra;
            lblCodigoCorrelativo.Text = muestraCompleta.Muestra.CodigoInternoLab;

            lblEstablecimiento.Text = muestraCompleta.Muestra.Establecimiento.TipoEstablecimiento.Nombre + "-" + muestraCompleta.Muestra.Establecimiento.Nombre;

            //lblAfiliacion.Text = "";//dtPaciente.Rows[0]["ptnSocialSecurity"].ToString();
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

        }


        private void CargarGrilla(List<Vista_BuscarPaciente> listaMuestras)
        {
            dgvMuestras.DataSource = listaMuestras;
            dgvMuestras.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + listaMuestras.Count();
            lblNumRegistros.Visible = true;
        }

        /*
         * Nombre: EcportGridCSV
         * Argumentos: -
         * Descripción: Exporta los resultados de la grilla a un archivo CSV y genera la descarga del mismo
         */
        private void ExportGridToCSV()
        {
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            CargarGrilla(listaMuestras);

            Encoding encoding = Encoding.UTF8;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.csv");
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/text";
            dgvMuestras.AllowPaging = false;
            dgvMuestras.DataBind();

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();
            //Encabezado
            for (int k = 0; k < dgvMuestras.Columns.Count - 3; k++)
            {
                columnbind.Append(dgvMuestras.Columns[k].HeaderText + tabulador);
            }

            columnbind.Append("\r\n");

            //Resultados
            foreach (var muestra in listaMuestras)
            {
                string linea = string.Concat(muestra.CodigoMuestra, tabulador, muestra.CodigoInternoLab, tabulador, muestra.NombresNeonato, tabulador, muestra.ApellidosNeonato, tabulador, muestra.ApellidosMadre, tabulador, muestra.DNI, tabulador);

                if (muestra.FechaNacimiento != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(muestra.FechaNacimiento.ToString(), out fechaNacimiento))
                    {
                        linea = string.Concat(linea, fechaNacimiento.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                if (muestra.FechaToma != null)
                {
                    DateTime fechaToma;
                    if (DateTime.TryParse(muestra.FechaToma.ToString(), out fechaToma))
                    {
                        linea = string.Concat(linea, fechaToma.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                linea = string.Concat(linea, muestra.Telefono1, tabulador);//, muestra.Peso.ToString().Replace(',', '.'), tabulador, muestra.EdadGestacional, tabulador, muestra.Establecimiento, tabulador);

                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }



        #endregion

        protected void dgvMuestras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMuestras.PageIndex = e.NewPageIndex;
            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
            CargarGrilla(listaMuestras);
        }

        //protected void btnReporteConsolidado_Click(object sender, EventArgs e)
        //{
        //    List<Vista_BuscarPaciente> listaMuestras = _muestraBc.Ob_tenerMuestras(1, int.Parse(ddlEstablecimiento.SelectedValue), txtCodigoMuestra.Text, txtCodigoCorrelativo.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtDNI.Text, txtFechaInicioNac.Text, txtFechaFinNac.Text, txtFechaInicioToma.Text, txtFechaFinToma.Text);
        //    string establecimiento = ddlEstablecimiento.SelectedItem.Text;
        //    byte[] bytes = _rep.ReporteResultadosxEstablecimiento(listaMuestras);
        //    Response.Clear();
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", "attachment; filename= ListaReportes_" + establecimiento + ".pdf");
        //    //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
        //    Response.ContentType = "application/pdf";
        //    Response.Buffer = true;
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.BinaryWrite(bytes);
        //    Response.End();
        //    Response.Close();
        //}

        protected void btnHistorico_Click(object sender, EventArgs e)
        {
            MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(lblCodigoMuestra.Text);
            //List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
            byte[] bytes = _rep.ReporteHistoricoResultados(muestraCompleta);
            //byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

            string nombreArchivo = string.Concat(muestraCompleta.Neonato.Apellidos, "_", muestraCompleta.Neonato.Nombres);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }

        protected void dgvMuestras_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}