using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Muestras
{
    public partial class AdministrarMuestrasRechazadas : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly MuestraBC _muestraBc = new MuestraBC();
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();

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
                //CargarGrillaRechazadas(idEstablecimiento, string.Empty, string.Empty, string.Empty, string.Empty);
                
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
       
        protected void dgvRechazadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                string codigoMuestra = e.CommandArgument.ToString();
                MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
                CargarDatosMuestra(muestraCompleta);
                dgvResultado.Visible = false;
                lblTextoResultado.Visible = false;
                mpeAttendanceReport.Show();
            }
        }
        

        //Metodos

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

        private List<Vista_Muestra> ListaMuestrasRechazadas()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;
            string apellidosNeonato = txtApellidosNeonato.Text;
            string codigoCorrelativo = txtCorrelativo.Text;
            string dni = txtDNI.Text;

            List<Vista_Muestra> listaMuestrasRechazadas = _muestraBc.ObtenerListaMuestrasRechazadas(idEstablecimiento,
                                                                                                    apellidosNeonato,
                                                                                                    codigoCorrelativo,
                                                                                                    codigoMuestra, dni,
                                                                                                    txtFechaInicioRecepcion.Text,
                                                                                                    txtFechaFinRecepcion.Text);
            return listaMuestrasRechazadas;
        }

        private void CargarGrilla()
        {
            var lista = ListaMuestrasRechazadas();
            dgvRechazadas.DataSource = lista;
            dgvRechazadas.DataBind();
            lblNumRechazados.Text = "Registros Consultados: " + lista.Count;
            lblNumRechazados.Visible = true;
        }
        
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            //if (!(hdnSede.Value.CompareTo("1") == 0))
            //{
                ListItem item = new ListItem("--Seleccionar--", "0");
                ddlTipoEstablecimiento.Items.Insert(0, item);
                ddlTipoEstablecimiento.SelectedValue = "0";
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
            if (tipoEstablecimiento == 0)
            {
                ListItem item = new ListItem("--Seleccionar--", "0");
                ddlEstablecimiento.Items.Insert(0, item);
                ddlEstablecimiento.SelectedValue = "0";
            }
                
            //}

        }
        
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }

        private void ExportGridToCSV()
        {
            //List<Vista_ResultadosEstablecimiento> listaResultados = _resultadoBc.ObtenerResultadosHospital(ddlEstablecimiento.SelectedValue, txtFechaInicio.Text, txtFechaFin.Text, txtApellidosNeonato.Text, txtApellidosMadre.Text, txtCodigoMuestra.Text, txtDNI.Text);

            var ListaMuestras = ListaMuestrasRechazadas();
            
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

            //Encabezado
            string headers = string.Concat("N° Tarjeta", tabulador,
                                            "Correlativo", tabulador,
                                            "APELLIDOS DEL RN", tabulador,
                                            "FECHA DE NACIMIENTO", tabulador,
                                            "PROCEDENCIA", tabulador,
                                            "TELEFONO", tabulador,
                                            "DNI", tabulador,
                                            "DIA DE RECEPCION", tabulador,
                                            "TOMADO POR", tabulador);
            columnbind.Append(headers);
            columnbind.Append("\r\n");
            //Resultados
            foreach (var muestra in ListaMuestras)
            {
                string linea = string.Concat(muestra.CodigoMuestra, tabulador,
                                             muestra.CodigoInternoLab, tabulador,
                                             muestra.ApellidosNeonato, tabulador); 

                if (muestra.FechaNacimiento != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(muestra.FechaNacimiento.ToString() ,out fechaNacimiento))
                    {
                        linea = string.Concat(linea, fechaNacimiento.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                        
                }else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                linea = string.Concat(linea, muestra.Establecimiento, tabulador,
                                             muestra.Telefono1, tabulador,
                                             muestra.DNI, tabulador);

                if (muestra.FechaRecepcion != null)
                {
                    DateTime fechaRecepcion;
                    if (DateTime.TryParse(muestra.FechaRecepcion.ToString(), out fechaRecepcion))
                    {
                        linea = string.Concat(linea, fechaRecepcion.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                }else
                {
                    linea = string.Concat(linea, "", tabulador);
                }
                    linea = string.Concat(linea, muestra.TomadoPor, tabulador);
                                             
                                             
                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }

            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }
    }
}