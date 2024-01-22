using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using BE;
using BC;
using System.Text;

namespace TamiLifeSA.Resultados
{
    public partial class ResultadosEstablecimiento : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();
        private readonly UsuarioBC _usuarioBc = new UsuarioBC();
        
        //Eventos ----------------------------------------------------------------
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            //ListItem item = new ListItem("--Seleccionar--", "0");
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;
                if (!usuarioLogeado)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    CargarTipoEstablecimiento();
                    string tipo = ddlTipoEstablecimiento.SelectedValue;
                    int tipoAux = int.Parse(ddlTipoEstablecimiento.SelectedValue);
                    CargarEstablecimiento(tipoAux);

                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                        //CargarEstablecimiento(int.Parse(ddlTipoResultado.SelectedValue));
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                            Master.CambiarSiteMap("CentralSiteMap");
                        else
                        {
                            Usuario usuarioAux = _usuarioBc.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                            Establecimiento establecimientoAux = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(usuarioAux.IdEstablecimiento);
                            
                            ddlTipoEstablecimiento.SelectedValue = establecimientoAux.idTipoEstablecimiento.ToString();
                            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                            //CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);

                            int idHospital = usuarioAux.IdEstablecimiento;
                            ddlTipoEstablecimiento.Enabled = false;
                            ddlEstablecimiento.SelectedValue = idHospital.ToString();
                            ddlEstablecimiento.Enabled = false;
                            //CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);

                            //int idHospital = usuarioAux.IdEstablecimiento;
                            //ddlTipoEstablecimiento.Enabled = false;
                            //ddlEstablecimiento.SelectedValue = idHospital.ToString();
                            //ddlEstablecimiento.Enabled = false;
                        }
                    }
                }

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }
        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvResultados.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }
        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("reporte") == 0)
            {
                string codigoMuestra = e.CommandArgument.ToString();
                //Response.Redirect("~/Muestras/AgregarMuestraAux.aspx?CodigoMuestra=" + e.CommandArgument);
                var fun = new Reportes();
                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoInternoLab);
                List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
                byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

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

                mpeAttendanceReport.Show();
            }
        }
        protected void btnReportePopup_Click(object sender, EventArgs e)
        {
            var fun = new Reportes();
            MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(lblCodigoMuestra.Text);
            List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
            byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + muestraCompleta.Muestra.CodigoMuestra + ".pdf");
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
            lblPeso.Text = string.Format("{0:0.##}", muestraCompleta.Neonato.Peso);
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
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
        }
        private void CargarGrilla()
        {
            List<Vista_ResultadosEstablecimiento> listaResultados = _resultadoBc.ObtenerResultadosHospital(ddlEstablecimiento.SelectedValue, txtFechaInicio.Text, txtFechaFin.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtCodigoMuestra.Text, txtDNI.Text);
            DataTable dt = ObtenerDataTable(listaResultados);
            dgvResultados.DataSource = dt;
            dgvResultados.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + dt.Rows.Count;
            lblNumRegistros.Visible = true;
        }
        private DataTable ObtenerDataTable(List<Vista_ResultadosEstablecimiento> listaResultados)
        {
            var dt = new DataTable();
            dt.Columns.Add("CodigoMuestra");
            dt.Columns.Add("CodigoInternoLab");
            dt.Columns.Add("NumMuestra");
            dt.Columns.Add("NombresNeonato");
            dt.Columns.Add("ApellidosNeonato");
            dt.Columns.Add("ApellidosMadre");
            dt.Columns.Add("DNI");
            dt.Columns.Add("Telefono1");
            dt.Columns.Add("Telefono2");
            dt.Columns.Add("FechaNacimiento");
            dt.Columns.Add("FechaToma");
            dt.Columns.Add("FechaRecepcion");
            dt.Columns.Add("NTSH");
            dt.Columns.Add("N17OHP");
            dt.Columns.Add("NeoPhe");
            dt.Columns.Add("IRT");

            string codigoMuestra = string.Empty;
            int index = 0;
            foreach (var resultado in listaResultados)
            {
                if (codigoMuestra.CompareTo(resultado.CodigoMuestra) != 0)
                {
                    codigoMuestra = resultado.CodigoMuestra;
                    DataRow dr = dt.NewRow();
                    dr["CodigoMuestra"] = resultado.CodigoMuestra;
                    dr["CodigoInternoLab"] = resultado.CodigoInternoLab;
                    dr["NumMuestra"] = resultado.NumMuestra;
                    dr["NombresNeonato"] = resultado.NombresNeonato;
                    dr["ApellidosNeonato"] = resultado.ApellidosNeonato;
                    dr["ApellidosMadre"] = resultado.ApellidosMadre;
                    dr["DNI"] = resultado.DNI;
                    dr["Telefono1"] = resultado.Telefono1;
                    dr["Telefono2"] = resultado.Telefono2;

                    if (resultado.FechaNacimiento != null)
                    {
                        dr["FechaNacimiento"] = resultado.FechaNacimiento.ToString().Substring(0, 10);
                    }

                    if (resultado.FechaToma != null)
                    {
                        dr["FechaToma"] = resultado.FechaToma.ToString().Substring(0, 10);
                    }

                    if (resultado.FechaRecepcion != null)
                    {
                        dr["FechaRecepcion"] = resultado.FechaRecepcion.ToString().Substring(0, 10);
                    }

                    dr[resultado.TestName] = resultado.ConcTexto;
                    dt.Rows.Add(dr);
                    index = dt.Rows.Count - 1;
                }
                else
                {
                    dt.Rows[index][resultado.TestName] = resultado.ConcTexto;
                }

            }
            return dt;
        }
        private void ExportGridToCSV()
        {
            List<Vista_ResultadosEstablecimiento> listaResultados = _resultadoBc.ObtenerResultadosHospital(ddlEstablecimiento.SelectedValue, txtFechaInicio.Text, txtFechaFin.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtCodigoMuestra.Text, txtDNI.Text);
            DataTable dt = ObtenerDataTable(listaResultados);


            string nombreArchivo = string.Concat("Resultados_", HttpContext.Current.User.Identity.Name,"_",DateTime.Now.ToString("ddMMyyyy"));

            Encoding encoding = Encoding.UTF8;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".csv");
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/text";
            //dgvResultados.AllowPaging = false;
            //dgvResultados.DataBind();

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();

            //Encabezado
            string headers = string.Concat("Código", tabulador,
                                            "RN", tabulador,
                                            "Apellidos RN", tabulador,
                                            "Apellidos Madre", tabulador,
                                            "DNI (Madre)", tabulador,
                                            "Telefono1", tabulador,
                                            "Telefono2", tabulador,
                                            "Fecha Nac.", tabulador,
                                            "Fecha Toma", tabulador,
                                            "Fecha Recep.", tabulador,
                                            "N° de Muestra", tabulador,
                                            "TSH uU/mL", tabulador,
                                            "17OHP ng/ml", tabulador,
                                            "IRT ng/ml", tabulador,
                                            "NeoPhe mg/dl", tabulador
                                            );
            columnbind.Append(headers);
            columnbind.Append("\r\n");
            //Resultados
            foreach (DataRow dr in dt.Rows)
            {

                string linea = string.Concat(dr["CodigoMuestra"], tabulador,
                                            dr["NombresNeonato"], tabulador,
                                            dr["ApellidosNeonato"], tabulador, 
                                            dr["ApellidosMadre"], tabulador, 
                                            dr["DNI"], tabulador, 
                                            dr["Telefono1"], tabulador,
                                            dr["Telefono2"], tabulador);
                if (dr["FechaNacimiento"] != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(dr["FechaNacimiento"].ToString(), out fechaNacimiento))
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

                if (dr["FechaToma"] != null)
                {
                    DateTime fechaToma;
                    if (DateTime.TryParse(dr["FechaToma"].ToString(), out fechaToma))
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

                if (dr["FechaRecepcion"] != null)
                {
                    DateTime fechaRecepcion;
                    if (DateTime.TryParse(dr["FechaRecepcion"].ToString(), out fechaRecepcion))
                    {
                        linea = string.Concat(linea, fechaRecepcion.ToShortDateString(), tabulador);
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

                linea = string.Concat(linea, dr["NumMuestra"], tabulador, dr["NTSH"].ToString().Replace(',', '.'), tabulador, dr["N17OHP"].ToString().Replace(',', '.'), tabulador, dr["IRT"].ToString().Replace(',', '.'), tabulador, dr["NeoPhe"].ToString().Replace(',', '.'), tabulador);

                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }
        #endregion




    }
}