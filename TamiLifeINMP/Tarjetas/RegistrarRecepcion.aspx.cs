using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BC;
using BE;
using System.Data;

namespace TamizajePortal.Tarjetas
{
    public partial class RegistrarRecepcion : System.Web.UI.Page
    {
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        readonly UsuarioBC usuarioBC = new UsuarioBC();
        readonly RecepcionBC bc = new RecepcionBC();
        readonly TarjetaBC tarjetaBC = new TarjetaBC();
        private Recepcion recepcion = new Recepcion();

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
                if (Request["idRecepcion"] != null)
                {
                    CargarTipoEstablecimiento();
                    //hdnRangoValido.Value = "true";
                    hdnIdRecepcion.Value = Request["idEnvio"];
                    Master.CambiarTitulo("EDITAR ENVIO");
                    recepcion = bc.ObtenerRecepcion(int.Parse(hdnIdRecepcion.Value));
                    CargarRecepcion(recepcion);
                }
                else
                {
                    CargarTipoEstablecimiento();
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    Master.CambiarTitulo("REGISTRAR RECEPCION");
                }
                //CargarDataTable();
            }
        }

        protected void CargarRecepcion(BE.Recepcion recepcion)
        {
            Establecimiento establecimiento = establecimientoBC.ObtenerEstablecimientoxIdEstablecimiento(recepcion.idEstablecimiento);
            ddlTipoEstablecimiento.SelectedValue = establecimiento.idTipoEstablecimiento.ToString();
            CargarEstablecimiento(establecimiento.idTipoEstablecimiento);
            ddlEstablecimiento.SelectedValue = establecimiento.idEstablecimiento.ToString();
            txtFechaRecepcion.Text = DateTime.Parse(recepcion.FechaRecepcion.ToString()).ToShortDateString();
            //txtCodigoInicial.Text = envio.CodigoInicial;
            //txtCodigoFinal.Text = envio.CodigoFinal;
            List<Vista_ListaTarjetas> listaTarjetas = new List<Vista_ListaTarjetas>();

            listaTarjetas = tarjetaBC.ObtenerListaTarjetas(recepcion.idRecepcion,2);
            dgvResultados.DataSource = listaTarjetas;
            dgvResultados.DataBind();
            dgvResultados.Visible = true;
            tablaAux.Visible = false;
            //txtCodigoInicial.Enabled = false;
            //txtCodigoFinal.Enabled = true;
            //btnGenerar.Visible = false;
            //btnGuardar.Visible = true;
            //btnCancelar.Visible = true;
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvResultados.Rows.Count > 0)
            {
                Recepcion recepcion = new Recepcion();
                recepcion.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                recepcion.FechaRecepcion = DateTime.Parse(txtFechaRecepcion.Text);
                recepcion.Estado = 1;
                recepcion = bc.RegistrarRecepcion(recepcion);

                List<Tarjeta> listaTarjetas = new List<Tarjeta>();

                for (int i = 0; i < dgvResultados.Rows.Count; i++)
                {//chkRechazada
                    Tarjeta tarjeta = new Tarjeta();
                    tarjeta.CodigoMuestra = dgvResultados.Rows[i].Cells[1].Text;
                    tarjeta.Recibido = true;
                    tarjeta.idRecepcion = recepcion.idRecepcion;
                    //(CheckBox)dgvResultados.Rows[rowIndex].Cells[2].FindControl("chkRechazada");
                    tarjeta.Rechazado = ((CheckBox)dgvResultados.Rows[i].FindControl("chkRechazada")).Checked;
                    tarjeta.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                    listaTarjetas.Add(tarjeta);
                }



                tarjetaBC.RegitrarRecepcionTarjetas(listaTarjetas);

                //dgvResultados.Rows.Count()
                LimpiarControles();

                //EnvioBC envioBC = new EnvioBC();    
            }
            else
            {
                
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
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

        protected void AgregarFila(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void AddNewRowToGrid()
        {

            int rowIndex = 0;
            if (ViewState["TablaTarjetas"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TablaTarjetas"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)dgvResultados.Rows[rowIndex].Cells[1].FindControl("txtCodigo");
                        CheckBox box2 = (CheckBox)dgvResultados.Rows[rowIndex].Cells[2].FindControl("chkRechazada");
                        //TextBox box3 = (TextBox)dgvResultados.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                        drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["NumFila"] = i + 1;
                        drCurrentRow["CodigoMuestra"] = box1.Text;
                        drCurrentRow["Rechazada"] = box2.Checked;
                        //drCurrentRow["Column3"] = box3.Text;

                        rowIndex++;
                    }

                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["TablaTarjetas"] = dtCurrentTable;

                    //Rebind the Grid with the current data
                    dgvResultados.DataSource = dtCurrentTable;
                    dgvResultados.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["TablaTarjetas"] != null)
            {
                DataTable dt = (DataTable)ViewState["TablaTarjetas"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {

                        TextBox box1 = (TextBox)dgvResultados.Rows[rowIndex].Cells[1].FindControl("txtCodigo");
                        CheckBox box2 = (CheckBox)dgvResultados.Rows[rowIndex].Cells[2].FindControl("chkRechazada");


                        box1.Text = dt.Rows[i]["CodigoMuestra"].ToString();
                        box2.Checked = bool.Parse(dt.Rows[i]["Rechazada"].ToString());
                        //box3.Text = dt.Rows[i]["Column3"].ToString();

                        rowIndex++;

                    }
                }
            }
        }
       
        private void LimpiarControles()
        {
            ddlTipoEstablecimiento.SelectedValue = "0";
            CargarEstablecimiento(0);
            txtFechaRecepcion.Text = string.Empty;
            dgvResultados.DataSource = string.Empty;
            dgvResultados.DataBind();
            ViewState["TablaTarjetas"] = null;
            tablaAux.Visible = true;
            dgvResultados.Visible = false;
        }
        #endregion

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var codigo = txtCodigoAgregar.Text;
            AgregarFila(codigo);
            txtCodigoAgregar.Text = string.Empty;
            txtCodigoAgregar.Focus();
            if (!dgvResultados.Visible)
            {
                tablaAux.Visible = false;
                dgvResultados.Visible = true;
            }
            
        }
        protected void AgregarFila(string codigo)
        {

            if (ViewState["TablaTarjetas"] != null)
            {
                
                DataTable dtCurrentTable = (DataTable)ViewState["TablaTarjetas"];
                
                DataRow dr = dtCurrentTable.NewRow();
                dr["CodigoMuestra"] = codigo;
                dr["Rechazada"] = false;
                try
                {
                    dtCurrentTable.Rows.Add(dr);
                }
                catch (Exception e)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "alert('El Codigo ya esta ingresado en la lista');");
                    //Response.Write(@"<script language='javascript'>alert('El Codigo ya esta ingresado en la lista')</script>");
                    //throw e;
                    //string display = "Message Pop-up!";
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');",true);
                }
                

                ViewState["TablaTarjetas"] = dtCurrentTable;
                dgvResultados.DataSource = dtCurrentTable;
                dgvResultados.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("CodigoMuestra");
                dt.Columns.Add("Rechazada");
                dt.PrimaryKey = new DataColumn[] { dt.Columns["CodigoMuestra"] };

                DataRow dr = dt.NewRow();
                dr["CodigoMuestra"] = codigo;
                dr["Rechazada"] = false;
                dt.Rows.Add(dr);

                ViewState["TablaTarjetas"] = dt;

                dgvResultados.DataSource = dt;
                dgvResultados.DataBind();
               
            }

            //Set Previous Data on Postbacks
            CargarDataPrevia();
        }

        protected void CargarDataPrevia()
        {
            //int rowIndex = 0;
            if (ViewState["TablaTarjetas"] != null)
            {
                DataTable dt = (DataTable)ViewState["TablaTarjetas"];
                if (dt.Rows.Count > 0)
                {
                    dgvResultados.DataSource = dt;
                    dgvResultados.DataBind();

                }
                else
                {
                    dgvResultados.DataSource = string.Empty;
                    dgvResultados.DataBind();
                    dgvResultados.Visible = false;
                    tablaAux.Visible = true;
                }
            }
        }

        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                if (ViewState["TablaTarjetas"] != null)
                {
                    DataTable dt = (DataTable)ViewState["TablaTarjetas"];
                    DataRow row = dt.Rows.Find(e.CommandArgument);
                    dt.Rows.Remove(row);
                    ViewState["TablaTarjetas"] = dt;
                    CargarDataPrevia();
                }
            }
        }

        protected void cusGridValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = dgvResultados.Rows.Count >= 1;
        }
    }
}