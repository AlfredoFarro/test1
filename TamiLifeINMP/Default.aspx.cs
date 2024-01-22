using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA
{
    public partial class _Default : System.Web.UI.Page
    {
        private readonly ResultadoBC resultadoBC = new ResultadoBC();
        private readonly MuestraBC muestraBC = new MuestraBC();
        private readonly MuestraCompletaBC muestraCompletaBC = new MuestraCompletaBC();
        private readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        private readonly UsuarioBC usuarioBC = new UsuarioBC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        if (HttpContext.Current.User.IsInRole("sede"))
                        {
                            DivPrincipal.Visible = true;
                            DivInformativo.Visible = false;

                            
                            Usuario user = usuarioBC.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                            int idEstablecimiento = user.IdEstablecimiento;
                            Establecimiento establecimiento = establecimientoBC.ObtenerEstablecimientoxIdEstablecimiento(idEstablecimiento);
                            CargarGrillaResultadosAlterados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
                            CargarGrillaRechazadas(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
                            hdnIdEstablecimiento.Value = idEstablecimiento.ToString();
                            lblTitulo.Text = establecimiento.TipoEstablecimiento.Nombre + "-" + establecimiento.Nombre;
                        }
                    }
                }
                else
                {
                    DivPrincipal.Visible = false;
                    DivInformativo.Visible = true;
                }
            }
        }

        public void CargarGrillas()
        {
            UsuarioBC usuarioBC = new UsuarioBC();
            Usuario user = usuarioBC.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
            int idEstablecimiento = user.IdEstablecimiento;
        }
        private void CargarGrillaResultadosAlterados(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            
            List<Vista_ResultadosEstablecimiento> listaResultadosAlterados = resultadoBC.ObtenerResultadosHospitalAlterados(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
            //List<Vista_ResultadosEstablecimiento> listaResultadosAlterados = new List<Vista_ResultadosEstablecimiento>();
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

        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int idEstablecimiento = int.Parse(hdnIdEstablecimiento.Value);
            dgvResultados.PageIndex = e.NewPageIndex;
            //CargarGrillaResultados(idEstablecimiento);
            CargarGrillaResultadosAlterados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
            
        }

        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string codigoMuestra = e.CommandArgument.ToString();
                MuestraCompletaBE muestraCompleta = muestraCompletaBC.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(codigoMuestra);
                //List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(muestraCompleta.Muestra.CodigoInternoLab);
                List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra,muestraCompleta.Muestra.CodigoInternoLab);
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
                MuestraCompletaBE muestraCompleta = muestraCompletaBC.ObtenerMuestra(codigoMuestra);
                //List<Vista_Resultado> listaResultados = reporteBC.ObtenerResultados(muestraCompleta.Muestra.CodigoInternoLab);
                List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
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

        protected void dgvRechazadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int idEstablecimiento = int.Parse(hdnIdEstablecimiento.Value);
            dgvRechazadas.PageIndex = e.NewPageIndex;
            //CargarGrillaRechazadas(idEstablecimiento);
            CargarGrillaRechazadas(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        protected void dgvRechazadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string codigoMuestra = e.CommandArgument.ToString();
                MuestraCompletaBE muestraCompleta = muestraCompletaBC.ObtenerMuestra(codigoMuestra);
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

        protected void btnReportePopup_Click(object sender, EventArgs e)
        {
            var fun = new Reportes();
            MuestraCompletaBE muestraCompleta = muestraCompletaBC.ObtenerMuestra(lblCodigoMuestra.Text);
            //List<Vista_Resultado> listaResultados = reporteBC.ObtenerResultados(muestraCompleta.Muestra.CodigoInternoLab);
            List<Vista_Resultado> listaResultados = resultadoBC.ObtenerResutados(muestraCompleta.Muestra.CodigoMuestra, muestraCompleta.Muestra.CodigoInternoLab);
            byte[] bytes = fun.ReporteResultados(muestraCompleta, listaResultados);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lblCodigoMuestra.Text + ".pdf");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }

        protected void dgvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string determinacion = e.Row.Cells[4].Text;
            //int det; 
            //if  (int.TryParse(determinacion, out det))
            //{
            //    if (det > 20)
            //    {
            //        e.Row.Cells[2].ForeColor = Color.White;
            //        e.Row.Cells[2].BackColor = Color.Red;
            //    }
            //}
            
        }

        protected void btnDescargarAlterados_Click(object sender, EventArgs e)
        {
            //Usuario user = usuarioBC.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
            int idEstablecimiento = int.Parse(hdnIdEstablecimiento.Value);
            //CargarGrillaResultadosAlterados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
            List<Vista_ResultadosEstablecimiento> listaResultadosAlterados = resultadoBC.ObtenerResultadosHospitalAlterados(idEstablecimiento,string.Empty, string.Empty, string.Empty, string.Empty);
            
            Encoding encoding = Encoding.UTF8;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.csv");
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/text";
            //dgvResultados.AllowPaging = false;
            //dgvResultados.DataBind();

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();

            string headers = string.Concat("Código", tabulador,
                                           "Nombres RN", tabulador,
                                           "Apellidos RN", tabulador,
                                           "Apellidos Madre", tabulador,
                                           "DNI(Madre)", tabulador,
                                           "Telefono", tabulador,
                                           "Prueba", tabulador,
                                           "Resultado", tabulador,
                                           "Unidad", tabulador,
                                           "Fecha Nacimiento", tabulador,
                                           "Fecha Toma", tabulador,
                                           "Fecha Recepción", tabulador,
                                           "Fecha Resultado", tabulador);
            columnbind.Append(headers);
            columnbind.Append("\r\n");

            //Resultados
            foreach (var resultado in listaResultadosAlterados)
            {
                string linea = string.Concat(resultado.CodigoMuestra, tabulador, 
                                             resultado.NombresNeonato, tabulador,
                                             resultado.ApellidosNeonato, tabulador, 
                                             resultado.ApellidosMadre, tabulador,
                                             resultado.DNI, tabulador,
                                             resultado.Telefono1, tabulador,
                                             resultado.TestName , tabulador,
                                             resultado.ConcTexto, tabulador,
                                             resultado.Unidad, tabulador);

                if (resultado.FechaNacimiento != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(resultado.FechaNacimiento.ToString(), out fechaNacimiento))
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

                if (resultado.FechaToma != null)
                {
                    DateTime fechaToma;
                    if (DateTime.TryParse(resultado.FechaToma.ToString(), out fechaToma))
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

                if (resultado.FechaRecepcion != null)
                {
                    DateTime fechaRecepcion;
                    if (DateTime.TryParse(resultado.FechaRecepcion.ToString(), out fechaRecepcion))
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

                if (resultado.FechaResultado != null)
                {
                    DateTime fechaResultado;
                    if (DateTime.TryParse(resultado.FechaResultado.ToString(), out fechaResultado))
                    {
                        linea = string.Concat(linea, fechaResultado.ToShortDateString(), tabulador);
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
                //linea = string.Concat(linea, resultado.ConcTexto, tabulador, resultado.Peso.ToString().Replace(',', '.'), tabulador, resultado.EdadGestacional, tabulador, resultado.Establecimiento, tabulador, resultado.Telefono1, tabulador, resultado.CodigoMuestra, tabulador, resultado.DNI, tabulador);

                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnDescargarRechazadas_Click(object sender, EventArgs e)
        {
            //Usuario user = usuarioBC.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
            int idEstablecimiento = int.Parse(hdnIdEstablecimiento.Value);
            //CargarGrillaResultadosAlterados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
            //List<Vista_ResultadosEstablecimiento> listaResultadosAlterados = resultadoBC.ObtenerResultadosHospitalAlterados(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
            List<Vista_Muestra> listaMuestrasRechazadas = muestraBC.ObtenerListaMuestrasRechazadasEstablecimiento(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
            Encoding encoding = Encoding.UTF8;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.csv");
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/text";
            //dgvResultados.AllowPaging = false;
            //dgvResultados.DataBind();

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();

            string headers = string.Concat("Código", tabulador,
                                           "Correlativo", tabulador,
                                           "Apellidos RN", tabulador,
                                           "Apellidos Madre", tabulador,
                                           "DNI(Madre)", tabulador,
                                           "Telefono", tabulador,
                                           "Fecha Nacimiento", tabulador,
                                           "Fecha Toma", tabulador,
                                           "TomadoPor", tabulador,
                                           "Rechazado Por", tabulador);
            columnbind.Append(headers);
            columnbind.Append("\r\n");

            //Resultados
            foreach (var muestra in listaMuestrasRechazadas)
            {
                string linea = string.Concat(muestra.CodigoMuestra, tabulador,
                                             muestra.CodigoInternoLab, tabulador,
                                             muestra.ApellidosNeonato, tabulador,
                                             muestra.ApellidosMadre, tabulador,
                                             muestra.DNI, tabulador,
                                             muestra.Telefono1, tabulador);

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

                linea = string.Concat(linea, muestra.TomadoPor, tabulador, muestra.MotivoRechazo, tabulador);

                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();
        }

    }
}
