using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Digitacion
{
    public partial class AdministrarDigitacion : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly MuestraBC _muestraBc = new MuestraBC();
        private readonly UsuarioBC _usuarioBc = new UsuarioBC();
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();

        //Eventos ----------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
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
                            hdnSede.Value = "1";
                            Usuario usuarioAux = _usuarioBc.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                            Establecimiento establecimientoAux = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(usuarioAux.IdEstablecimiento);
                            ddlTipoEstablecimiento.SelectedValue = establecimientoAux.idTipoEstablecimiento.ToString();
                            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                            //CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);

                            int idHospital = usuarioAux.IdEstablecimiento;
                            ddlTipoEstablecimiento.Enabled = false;
                            ddlEstablecimiento.SelectedValue = idHospital.ToString();
                            ddlEstablecimiento.Enabled = false;
                        }

                    }
                    dgvMuestras.PageSize = int.Parse(ConfigurationManager.AppSettings["numpaginacion"]);
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void dgvMuestras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            if (e.CommandName.CompareTo("editar") == 0)
            {
                Response.Redirect("~/Muestras/EditarMuestra.aspx?CodigoMuestra=" + e.CommandArgument + "&Origen=2");
            }
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                if (_muestraBc.EliminarMuestra(e.CommandArgument.ToString()))
                {
                    CargarGrilla();
                }
                else
                {
                    lblNumRegistros.Text = "Error al eliminar la muestra";
                }

                //Response.Redirect("~/Muestras/EditarMuestra.aspx?CodigoMuestra=" + e.CommandArgument);

            }
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                string codigoMuestra = e.CommandArgument.ToString();

                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                CargarDatosMuestra(muestraCompleta);
                mpeAttendanceReport.Show();
            }
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }


        //Metodos ----------------------------------------------------------------

        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
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
                establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

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

        private void CargarGrilla()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;
            string apellidosNeonato = txtApellidosNeonato.Text;
            string apellidosMadre = txtApellidosMadre.Text;
            string DNI = txtDNI.Text;
            string fechaInicial = txtFechaInicio.Text;
            string fechaFinal = txtFechaFin.Text;

            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestrasDigitadas(idEstablecimiento, apellidosNeonato, apellidosMadre, fechaInicial, fechaFinal, codigoMuestra, DNI);

            dgvMuestras.DataSource = listaMuestras;
            dgvMuestras.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + listaMuestras.Count();
            lblNumRegistros.Visible = true;
        }
        private void ExportGridToCSV()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;
            string apellidosNeonato = txtApellidosNeonato.Text;
            string apellidosMadre = txtApellidosMadre.Text;
            string DNI = txtDNI.Text;
            string fechaInicial = txtFechaInicio.Text;
            string fechaFinal = txtFechaFin.Text;

            List<Vista_BuscarPaciente> listaMuestras = _muestraBc.ObtenerMuestrasDigitadas(idEstablecimiento, apellidosNeonato, apellidosMadre, fechaInicial, fechaFinal, codigoMuestra, DNI);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Neonatos.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            //dgvMuestras.AllowPaging = false;
            //dgvMuestras.DataBind();

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();

            //Encabezado
            string headers = string.Concat("Codigo Tarjeta", tabulador,
                                            "Nombres RN", tabulador,
                                            "Apellidos RN", tabulador,
                                            "Apellidos Madre", tabulador,
                                            "DNI (Madre)", tabulador,
                                            "Fecha Nac.", tabulador,
                                            "Hora Nac.", tabulador,
                                            "Sexo", tabulador,
                                            "Peso", tabulador,
                                            "Talla", tabulador,
                                            "E.G", tabulador,
                                            "Fecha Toma", tabulador);
            columnbind.Append(headers);
            columnbind.Append("\r\n");

            foreach (var muestra in listaMuestras)
            {
                string linea = string.Concat(muestra.CodigoMuestra, tabulador,
                                             muestra.NombresNeonato, tabulador,
                                             muestra.ApellidosNeonato, tabulador,
                                             muestra.ApellidosMadre, tabulador,
                                             muestra.DNI, tabulador);

                if (muestra.FechaNacimiento != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(muestra.FechaNacimiento.ToString(), out fechaNacimiento))
                    {
                        linea = string.Concat(linea, fechaNacimiento.ToShortDateString(), tabulador, fechaNacimiento.ToShortTimeString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador, "", tabulador);
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }
                // sexo = 1 Femenino / sexo = 2 Masculino

                if (muestra.Sexo != null)
                {
                    if (muestra.Sexo == 1)
                    {
                        linea = string.Concat(linea, "F", tabulador);
                    }
                    else
                    {
                        if (muestra.Sexo == 2)
                        {
                            linea = string.Concat(linea, "M", tabulador);
                        }
                        else
                        {
                            linea = string.Concat(linea, "", tabulador);
                        }
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                linea = string.Concat(linea, muestra.Peso.ToString().Replace(',', '.'), tabulador,
                                            muestra.Talla.ToString().Replace(',', '.'), tabulador,
                                            muestra.EdadGestacional.ToString(), tabulador);

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
                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

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

            string muestraAceptada = muestraCompleta.Muestra.MuestraAceptada.ToString();
            lblEstadoMuestra.Text = string.Empty;
            if (muestraAceptada.Contains("True"))
            {
                lblEstadoMuestra.Text = "Aceptada";
                lblEstadoMuestra.ForeColor = Color.Green;
                lblEtiquetaMotivo.Visible = false;
                lblMotivoRechazo.Visible = false;

            }
            if (muestraAceptada.Contains("False"))
            {
                lblEstadoMuestra.Text = "Rechazada";
                lblEstadoMuestra.ForeColor = Color.Red;
                lblEtiquetaMotivo.Visible = true;
                lblMotivoRechazo.Text = muestraCompleta.Muestra.MotivoRechazo;
                lblMotivoRechazo.ForeColor = Color.Red;
                lblMotivoRechazo.Font.Bold = true;
                lblMotivoRechazo.Visible = true;
            }

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

        }

        protected void dgvMuestras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMuestras.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvMuestras_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            int indice = 9;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                if (e.Row.Cells[indice].Text != string.Empty)
                {
                    string muestraAceptada = e.Row.Cells[indice].Text;
                    if (muestraAceptada.Contains("True"))
                    {
                        e.Row.Cells[indice].Text = "Aceptada";
                        e.Row.Cells[indice].ForeColor = Color.White;
                        e.Row.Cells[indice].BackColor = Color.Green;
                    }
                    if (muestraAceptada.Contains("False"))
                    {
                        e.Row.Cells[indice].Text = "Rechazada";
                        e.Row.Cells[indice].ForeColor = Color.White;
                        e.Row.Cells[indice].BackColor = Color.Red;
                    }
                }
                else
                {
                    e.Row.Cells[indice].Text = "Pendiente";
                }


            }



        }
    }
}