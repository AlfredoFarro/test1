using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;
using System.Data;

namespace TamizajePortal.Tarjetas
{
    public partial class RegistrarRecepcionAux : Page
    {
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        UsuarioBC usuarioBC = new UsuarioBC();

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
                CargarTipoEstablecimiento();
                CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                Master.CambiarTitulo("REGISTRAR RECEPCION");
                CargarGrilla();
            }
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
            CargarGrilla();
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

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
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ////List<Establecimiento> establecimietos = establecimientoDA.obtenerEstablecimientosxTipo(tipoEstablecimiento);
            //List<Establecimiento> establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            //ddlEstablecimiento.DataSource = establecimientos;
            //ddlEstablecimiento.DataTextField = "Nombre";
            //ddlEstablecimiento.DataValueField = "idEstablecimiento";
            //ddlEstablecimiento.DataBind();
            ////LlenarDatosEstablecimiento();

            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("--Seleccionar--", "0");
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
                        drCurrentRow["NumFila"] = i + 1;
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
        private void CargarGrilla()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("NumFila");
            dt.Columns.Add("CodigoMuestra");
            dt.Columns.Add("Rechazada");

            DataRow dr = dt.NewRow();
            dr["NumFila"] = 1;
            dr["CodigoMuestra"] = string.Empty;
            dr["Rechazada"] = false;
            dt.Rows.Add(dr);

            ViewState["TablaTarjetas"] = dt;

            dgvResultados.DataSource = dt;
            dgvResultados.DataBind();
        }

        private void LimpiarControles()
        {
            ddlTipoEstablecimiento.SelectedValue = "0";
            CargarEstablecimiento(0);

            txtFechaEnvio.Text = string.Empty;
            dgvResultados.DataSource = string.Empty;
            dgvResultados.DataBind();
        }
        #endregion

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigoAgregar.Text;
            AgregarFila(codigo);
            txtCodigoAgregar.Text = string.Empty;
            txtCodigoAgregar.Focus();
        }
        protected void AgregarFila(string codigo)
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
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["NumFila"] = i + 1;
                        drCurrentRow["CodigoMuestra"] = codigo;
                        drCurrentRow["Rechazada"] = false;
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
            CargarDataPrevia();
        }

        protected void CargarDataPrevia()
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
    }
}