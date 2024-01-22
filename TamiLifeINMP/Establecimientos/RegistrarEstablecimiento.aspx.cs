using System;
using System.Web;
using System.Web.UI.WebControls;
using BE;
using BC;
using System.Collections.Generic;

namespace TamiLifeSA.Establecimientos
{
    public partial class RegistrarEstablecimiento : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        readonly UbigeoBC _ubigeoBc = new UbigeoBC();
        readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();

        //Eventos ----------------------------------------------------------------
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
                    }
                }
                if (Request["idEstablecimiento"] != null)
                {
                    hdnIdEstablecimiento.Value = Request["idEstablecimiento"];
                    lblTitulo.Text = "Editar Establecimiento";
                    //Master.CambiarTitulo("EDITAR ESTABLECIMIENTO");
                    var establecimiento = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(int.Parse(hdnIdEstablecimiento.Value));
                    
                    //if (establecimiento.Departamento != null)
                    //{
                        CargarDepartamentos();
                        ddlDepartamento.SelectedValue = establecimiento.Departamento.ToString();
                        CargarProvincias(int.Parse(ddlDepartamento.SelectedValue));
                        ddlProvincia.SelectedValue = establecimiento.Provincia.ToString();
                        CargarDistritos(int.Parse(ddlProvincia.SelectedValue));
                        ddlDistrito.SelectedValue = establecimiento.Distrito.ToString();
                        
                    //}
                    CargarTipoEstablecimiento();
                    txtCodigo.Text = establecimiento.Codigo;
                    //txtCodigoRenaes.Text = establecimiento.CodRenaes;
                    txtEstablecimiento.Text = establecimiento.Nombre;
                    txtDireccion.Text = establecimiento.Direccion;
                    txtTelefono1.Text = establecimiento.Telefono1;
                    txtTelefono2.Text = establecimiento.Telefono1;
                    ddlTipoEstablecimiento.SelectedValue = establecimiento.idTipoEstablecimiento.ToString();
                }
                else
                {
                    //Master.CambiarTitulo("REGISTRAR ESTABLECIMIENTO");
                    CargarDepartamentos();
                    CargarProvincias(int.Parse(ddlDepartamento.SelectedValue));
                    CargarDistritos(int.Parse(ddlProvincia.SelectedValue));
                    CargarTipoEstablecimiento();
                }
                
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var establecimiento = new Establecimiento();
            if ((hdnIdEstablecimiento.Value != null) && (hdnIdEstablecimiento.Value.CompareTo(String.Empty)!= 0))
            {
                establecimiento = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(int.Parse(hdnIdEstablecimiento.Value));
                establecimiento.Codigo = txtCodigo.Text;
                //establecimiento.CodRenaes = txtCodigoRenaes.Text;
                establecimiento.Nombre = txtEstablecimiento.Text;
                establecimiento.idTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue);
                establecimiento.TipoEstablecimientoNombre = ddlTipoEstablecimiento.SelectedItem.Text;
                establecimiento.Direccion = txtDireccion.Text;
                establecimiento.Departamento = int.Parse(ddlDepartamento.SelectedValue);
                establecimiento.DepartamentoNombre = ddlDepartamento.SelectedItem.Text;
                establecimiento.Provincia = int.Parse(ddlProvincia.SelectedValue);
                establecimiento.ProvinciaNombre = ddlProvincia.SelectedItem.Text;
                establecimiento.Distrito = int.Parse(ddlDistrito.SelectedValue);
                if (establecimiento.Distrito != 0)
                {
                    establecimiento.DistritoNombre = ddlDistrito.SelectedItem.Text;
                }
                establecimiento.Telefono1 = txtTelefono1.Text;
                establecimiento.Telefono2 = txtTelefono2.Text;
                _establecimientoBc.ActualizarEstablecimiento(establecimiento);
                hdnIdEstablecimiento.Value = null;
                //Response.Redirect(Request.UrlReferrer.ToString());
                Response.Redirect("~/Establecimientos/AdministrarEstablecimientos.aspx");
            }
            else
            {
                establecimiento.Codigo = txtCodigo.Text;
                //establecimiento.CodRenaes = txtCodigoRenaes.Text;
                establecimiento.Nombre = txtEstablecimiento.Text;
                establecimiento.idTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue);
                establecimiento.TipoEstablecimientoNombre = ddlTipoEstablecimiento.SelectedItem.Text;
                establecimiento.Direccion = txtDireccion.Text;
                establecimiento.Departamento = int.Parse(ddlDepartamento.SelectedValue);
                establecimiento.DepartamentoNombre = ddlDepartamento.SelectedItem.Text;
                establecimiento.Provincia = int.Parse(ddlProvincia.SelectedValue);
                establecimiento.ProvinciaNombre = ddlProvincia.SelectedItem.Text;
                establecimiento.Distrito = int.Parse(ddlDistrito.SelectedValue);
                if (establecimiento.Distrito != 0)
                {
                    establecimiento.DistritoNombre = ddlDistrito.SelectedItem.Text;
                }
                //establecimiento.idUbigeo = establecimiento.Distrito;
                establecimiento.Telefono1 = txtTelefono1.Text;
                establecimiento.Telefono2 = txtTelefono2.Text;
                establecimiento.Estado = 1;
                _establecimientoBc.RegistrarEstablecimiento(establecimiento);
                LimpiarDatos();
            }
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartamento.SelectedValue.CompareTo("0") != 0)
            {
                CargarProvincias(int.Parse(ddlDepartamento.SelectedValue));
                CargarDistritos(int.Parse(ddlProvincia.SelectedValue));
            }    
                
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue.CompareTo("0") != 0)
                CargarDistritos(int.Parse(ddlProvincia.SelectedValue));
        }
        protected void ddlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Establecimientos/AdministrarEstablecimientos.aspx");
        }

        //Metodos ----------------------------------------------------------------
        public void CargarDepartamentos()
        {
            ddlDepartamento.DataSource = _ubigeoBc.ObtenerDepartamentos();
            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "idUbigeo";
            ddlDepartamento.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlDepartamento.Items.Insert(0, item);
            ddlDepartamento.SelectedValue = "0";
            //cargoDepartametos = true;
        }
        public void CargarProvincias(int idDepartamento)
        {
            ddlProvincia.DataTextField = "Nombre";
            ddlProvincia.DataValueField = "idUbigeo";
            var listaProvincias = new List<Ubigeo>();
            if (idDepartamento != 0)
            {
                listaProvincias = _ubigeoBc.ObtenerProvincias(idDepartamento);
            }
            ddlProvincia.DataSource = listaProvincias;
            ddlProvincia.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlProvincia.Items.Insert(0, item);
            ddlProvincia.SelectedValue = "0";
        }
        public void CargarDistritos(int idProvincia)
        {
            ddlDistrito.DataTextField = "Nombre";
            ddlDistrito.DataValueField = "idUbigeo";
            var listaDistritos = new List<Ubigeo>();
            if (idProvincia != 0)
            {
                listaDistritos = _ubigeoBc.ObtenerDistritos(idProvincia);
            }
            ddlDistrito.DataSource = listaDistritos;
            ddlDistrito.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlDistrito.Items.Insert(0, item);
            ddlDistrito.SelectedValue = "0";
            //cargoProvincias = true;
        }
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        private void LimpiarDatos()
        {
            txtCodigo.Text = string.Empty;
            //txtCodigoRenaes.Text = string.Empty;
            txtEstablecimiento.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            ddlDistrito.SelectedValue = "0";
            ddlProvincia.SelectedValue = "0";
            ddlDepartamento.SelectedValue = "0";
        }
    }
}