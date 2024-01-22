using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;
using System.Data;

namespace TamiLifeSA.Muestras
{
    public partial class EditarMuestra : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly MadreBC _madreBc = new MadreBC();
        private readonly NeonatoBC _neonatoBc = new NeonatoBC();
        private readonly MuestraBC _muestraBc = new MuestraBC();
        private readonly UsuarioBC _usuarioBc = new UsuarioBC();
        MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
        //Eventos ----------------------------------------------------------------
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            //ListItem item = new ListItem("--Seleccionar--", "0");
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;

                if (usuarioLogeado)
                {
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                        pnlAdministrador.Visible = true;
                        //CargarTipoEstablecimiento();
                        //CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                        {
                            Master.CambiarSiteMap("CentralSiteMap");
                            pnlAdministrador.Visible = true;
                            //CargarTipoEstablecimiento();
                            //CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                        }
                        else
                        {
                            hdnSede.Value = "1";
                            txtDNI.Enabled = false;
                            //Master.CambiarSiteMap("CentralSiteMap");
                            //CargarEstablecimientoUsuarioLogeado(HttpContext.Current.User.Identity.Name);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }



                if (Request["CodigoMuestra"] != null)
                {
                    //Esta Editando una muestra
                    lblTitulo.Text = "EDITAR MUESTRA";
                    btnIngresar.Text = "Cancelar";
                    btnGuardar.Visible = true;
                    txtFechaRecepcion.Text = DateTime.Today.ToShortDateString();
                    CargarTipoEstablecimiento();
                    hdnCodigoMuestra.Value = Request["CodigoMuestra"];
                    txtCodigoMuestra.Text = Request["CodigoMuestra"];
                    CargarFormularioEditarMuestra(hdnCodigoMuestra.Value);
                    CambiarEstadoControlesMadre(true);
                    CambiarEstadoControlesNeonato(true);
                    CambiarEstadoControlesMuestra(true);
                    lblNeonato.Visible = true;
                    ddlNeonato.Visible = true;
                    if (Request["Origen"] != null)
                    {
                        hdnPaginaProcedencia.Value = Request["Origen"];
                    }
                    //cfvExisteCodigoCorrelativo.Enabled = false;
                    //cfvExisteCodigoMuestra.
                }
                else
                {   //Esta registrando una Muestra
                    if (hdnSede.Value.CompareTo("1") == 0)
                    {
                        CargarEstablecimientoUsuarioLogeado(HttpContext.Current.User.Identity.Name);
                    }
                    else
                    {
                        CargarTipoEstablecimiento();
                        CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    }
                    CambiarEstadoControlesMadre(false);
                    CambiarEstadoControlesNeonato(false);
                    CambiarEstadoControlesMuestra(false);
                }

                //--------------------------------------------------------------
                //txtFechaRecepcion.Text = DateTime.Today.ToShortDateString();


            }
        }
        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {

            //string codigoMuestra = txtCodigoMuestra.Text;
            string DNI = txtDNI.Text;
            if (rfvDNI.IsValid && cvaDNI.IsValid)
            {
                if (btnIngresar.Text.CompareTo("Cancelar") == 0)
                {
                    LiberarMuestra();
                    //CargarEstablecimientoTipo();
                    CargarEstablecimientoUsuarioLogeado(HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    if (_madreBc.ExisteMadre(DNI))
                    {
                        Madre madre = _madreBc.ObtenerMadrexDNI(DNI);
                        hdnIdMadre.Value = madre.idMadre.ToString();
                        ddlNeonato.Visible = true;
                        lblNeonato.Visible = true;
                        CargarFormularioAgregarMuestra(madre); //EditarMuestra(codigoMuestra);
                    }
                    else
                    {
                        CargarFormularioNuevaMuestra();
                    }

                }
            }

            //}
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //hdnCodigoMuestra.Value.ToString()
            #region EditarMuestra
            //Editar Muestra
            if (hdnCodigoMuestra.Value.CompareTo(string.Empty) != 0)
            {
                bool registrarNoExisteCodigo = true;
                int existeCodigoMuestra = int.Parse(hdnExisteMuestra.Value);

                if (existeCodigoMuestra == 1)
                {   //Si el codigo de muestra existe
                    if (hdnCodigoMuestra.Value.CompareTo(txtCodigoMuestra.Text) != 0)
                    {
                        registrarNoExisteCodigo = false;
                        ValidationSummary1.ShowSummary = true;
                    }
                }
                if (HttpContext.Current.User.IsInRole("Administrador") || HttpContext.Current.User.IsInRole("Central"))
                {
                    int existeCodigoCorrelativo = int.Parse(hdnExisteCorrelativo.Value);
                    if (existeCodigoCorrelativo == 1)
                    {
                        if (hdnCodigoCorrelativo.Value.CompareTo(txtCodigoCorrelativo.Text) != 0)
                        {
                            registrarNoExisteCodigo = false;
                            ValidationSummary1.ShowSummary = true;
                        }
                    }
                }

                if (registrarNoExisteCodigo)
                {
                    MuestraCompletaBE muestraCompleta;
                    if (hdnIdNeonato.Value.CompareTo(string.Empty) != 0)
                    {   //Es el mismo neonato o es un neonato seleccionado de la lista que ya estaba registrado en el sistema.

                        Muestra muestra = _muestraBc.ObtenerMuestraxIdMuestra(int.Parse(hdnIdMuestra.Value));

                        int idNeonatoActual = int.Parse(hdnIdNeonato.Value); //idNeonato que tenia asigando inicialmente la muestra
                        if (idNeonatoActual != muestra.idNeonato)
                        {
                            muestra.idNeonato = idNeonatoActual;
                            _muestraBc.ActualizarMuestra(muestra);
                        }
                        muestraCompleta = _muestraCompletaBc.ObtenerMuestra(int.Parse(hdnIdMuestra.Value));
                        LlenarDatosMadre(muestraCompleta.Madre);
                        LlenarDatosNeonato(muestraCompleta.Neonato);
                        LlenarDatosMuestra(muestraCompleta.Muestra);
                        //muestraCompleta.Muestra.CodigoMuestra = txtCodigoMuestra.Text;
                        //muestraCompleta.Muestra.ModificadoPor = HttpContext.Current.User.Identity.Name; //"cesar"; // usuario.Nombre;
                        //muestraCompleta.Muestra.FechaModificacion = DateTime.Today;
                        //DateTime fechaAux;
                        //if (DateTime.TryParse(txtFechaRecepcion.Text, out fechaAux))
                        //{
                        //    muestraCompleta.Muestra.FechaRecepcion = fechaAux;
                        //}
                        //_muestraCompletaBc.ActualizarMuestra(muestraCompleta);
                        //Response.Redirect("~/Muestras/AdministrarMuestras.aspx");
                    }
                    else
                    {   //Se esta creando un nuevo neonato al cual asignar la muestra
                        //registramos el nuevo neonato
                        Neonato neonatoNuevo = new Neonato();
                        LlenarDatosNeonato(neonatoNuevo);
                        neonatoNuevo.idMadre = int.Parse(hdnIdMadre.Value);
                        neonatoNuevo = _neonatoBc.InsertarNeonato(neonatoNuevo);

                        Muestra muestra = _muestraBc.ObtenerMuestraxIdMuestra(int.Parse(hdnIdMuestra.Value));
                        muestra.idNeonato = neonatoNuevo.idNeonato;
                        _muestraBc.ActualizarMuestra(muestra);
                        muestraCompleta = _muestraCompletaBc.ObtenerMuestra(int.Parse(hdnIdMuestra.Value));
                        LlenarDatosMadre(muestraCompleta.Madre);
                        //LlenarDatosNeonato(muestraCompleta.Neonato);
                        LlenarDatosMuestra(muestraCompleta.Muestra);

                    }

                    muestraCompleta.Muestra.ModificadoPor = HttpContext.Current.User.Identity.Name; //"cesar"; // usuario.Nombre;
                    muestraCompleta.Muestra.FechaModificacion = DateTime.Today;
                    DateTime fechaAux;
                    if (DateTime.TryParse(txtFechaRecepcion.Text, out fechaAux))
                    {
                        muestraCompleta.Muestra.FechaRecepcion = fechaAux;
                    }
                    _muestraCompletaBc.ActualizarMuestra(muestraCompleta);
                    string PaginaProcedencia = hdnPaginaProcedencia.Value;

                    if (PaginaProcedencia.CompareTo("2") == 0)
                    {
                        Response.Redirect("~/Digitacion/AdministrarDigitacion.aspx");
                    }else
                    {
                        if (PaginaProcedencia.CompareTo("3") == 0)
                        {
                            Response.Redirect("~/Resultados/ResultadosVerificacion.aspx");
                        }else
                        {
                            Response.Redirect("~/Muestras/AdministrarMuestras.aspx");
                        }
                    }
                    
                }



            }
            #endregion
            #region RegistrarMuestra
            else
            {   //Registrar Muestra
                //validamos que el codigo no existe en el sistema
                bool registrarNoExisteCodigo = true;
                int existeCodigoMuestra = int.Parse(hdnExisteMuestra.Value);

                if (existeCodigoMuestra == 1)
                {   //Si el codigo de muestra existe
                    registrarNoExisteCodigo = false;
                    ValidationSummary1.ShowSummary = true;
                }
                if (HttpContext.Current.User.IsInRole("Administrador") || HttpContext.Current.User.IsInRole("Central"))
                {
                    int existeCodigoCorrelativo = int.Parse(hdnExisteCorrelativo.Value);
                    if (existeCodigoCorrelativo == 1)
                    {
                        registrarNoExisteCodigo = false;
                        ValidationSummary1.ShowSummary = true;
                    }
                }
                //registra una muestra nueva ya sea de un paciente existente o no
                if (registrarNoExisteCodigo)
                {
                    //Agregar Muestra de un Paciente ya existente
                    if (hdnIdMadre.Value.CompareTo(string.Empty) != 0)
                    {
                        if (hdnIdNeonato.Value.CompareTo(string.Empty) != 0)
                        {
                            var muestra = new Muestra();
                            LlenarDatosMuestra(muestra);
                            //muestra.ModificadoPor = HttpContext.Current.User.Identity.Name; //"cesar"; // usuario.Nombre;
                            //muestra.FechaModificacion = DateTime.Today;
                            muestra.idNeonato = int.Parse(hdnIdNeonato.Value);
                            //muestra.FechaRecepcion = DateTime.Parse(txtFechaRecepcion.Text);
                            muestra.Estado = 1;
                            //muestra.PrimeraMuestra = true;
                            muestra.CreadoPor = HttpContext.Current.User.Identity.Name; //"cesar";// usuario.Nombre;
                            muestra.FechaCreacion = DateTime.Today;
                            muestra.RecibioMuestra = false;
                            muestra.MuestraEnviada = false;
                            _muestraBc.InsertarMuestra(muestra);
                            //ValidarMarcarMuestraRecibida(HttpContext.Current.User.Identity.Name, muestra.CodigoMuestra);//, muestra.idNeonato);
                            _muestraBc.MarcarMuestraRecibida(HttpContext.Current.User.Identity.Name, muestra.CodigoMuestra, muestra.idNeonato);
                            //ValidarMarcarMuestraRecibida(HttpContext.Current.User.Identity.Name, muestra.idNeonato);
                        }
                        else //Agregar un neonato de una madre ya registrada
                        {
                            var neonato = new Neonato();
                            LlenarDatosNeonato(neonato);
                            neonato.idMadre = int.Parse(hdnIdMadre.Value);
                            neonato = _neonatoBc.InsertarNeonato(neonato);
                            var muestra = new Muestra();
                            LlenarDatosMuestra(muestra);
                            muestra.idNeonato = neonato.idNeonato;
                            muestra.Estado = 1;
                            muestra.PrimeraMuestra = true;
                            muestra.CreadoPor = HttpContext.Current.User.Identity.Name; //"cesar";// usuario.Nombre;
                            muestra.FechaCreacion = DateTime.Today;
                            muestra.RecibioMuestra = false;
                            muestra.MuestraEnviada = false;
                            _muestraBc.InsertarMuestra(muestra);
                        }
                    }
                    else //Registrar un Paciente Nuevo
                    {
                        var madre = new Madre();
                        LlenarDatosMadre(madre);
                        madre = _madreBc.InsertarMadre(madre);
                        var neonato = new Neonato();
                        LlenarDatosNeonato(neonato);
                        neonato.idMadre = madre.idMadre;
                        neonato = _neonatoBc.InsertarNeonato(neonato);
                        var muestra = new Muestra();
                        LlenarDatosMuestra(muestra);
                        muestra.idNeonato = neonato.idNeonato;
                        //----------------------------------
                        muestra.Estado = 1;
                        muestra.PrimeraMuestra = true;
                        muestra.CreadoPor = HttpContext.Current.User.Identity.Name; //"cesar";// usuario.Nombre;
                        muestra.FechaCreacion = DateTime.Today;
                        muestra.RecibioMuestra = false;
                        muestra.MuestraEnviada = false;
                        //----------------------------------
                        _muestraBc.InsertarMuestra(muestra);
                    }
                    //----------------------------------
                    //muestra.Estado = 1;
                    //muestra.PrimeraMuestra = true;
                    //muestra.CreadoPor = HttpContext.Current.User.Identity.Name; //"cesar";// usuario.Nombre;
                    //muestra.FechaCreacion = DateTime.Today;
                    //muestra.RecibioMuestra = false;
                    //muestra.MuestraEnviada = false;
                    //----------------------------------
                    LiberarMuestra();
                    hdnCodigoMuestra.Value = string.Empty;
                }
            }
            #endregion

        }

        /*
         * Descripcion: Este combobox se muestra y habilita en caso el DNI corresponda a la madre de un paciente que ya tiene una muestra o muestras
         *              registradas en el sistema a fin de poder agregar una segunda muestra o en caso de seleccionar la opcion "Nuevo" agregar un nuevo
         *              paciente que tenga la misma madre.
         *           
         */
        protected void ddlNeonato_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idNeonato = int.Parse(ddlNeonato.SelectedValue);
            //Editar una muestra
            if (hdnCodigoMuestra.Value.CompareTo(string.Empty) != 0)
            {
                if (idNeonato > 0)
                {
                    //Neonato seleccionado de la lista
                    CargarControlesNeonato(idNeonato);
                    CambiarEstadoControlesNeonato(true);
                    //MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
                    MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(hdnCodigoMuestra.Value);
                    CargarEstablecimientoEdicion(int.Parse(muestraCompleta.Muestra.idEstablecimiento.ToString()));
                }
                else
                {   //Nuevo Neonato
                    hdnIdNeonato.Value = string.Empty;
                    CambiarEstadoControlesNeonato(true);
                    CambiarEstadoControlesMuestra(true);
                    CargarEstablecimientoUsuarioLogeado(HttpContext.Current.User.Identity.Name);
                    LimpiarControlesNeonato();
                }
            }
            //Registrar una muestra nueva
            else
            {
                if (idNeonato > 0)
                {
                    CargarControlesNeonato(idNeonato);
                    CambiarEstadoControlesNeonato(false);
                    
                }
                else
                {   //si el idNeonato == 0 significa que es un nuevo paciente.
                    hdnIdNeonato.Value = string.Empty;
                    CambiarEstadoControlesNeonato(true);
                    CambiarEstadoControlesMuestra(true);
                    //CargarEstablecimientoTipo();
                    CargarEstablecimientoUsuarioLogeado(HttpContext.Current.User.Identity.Name);
                    LimpiarControlesNeonato();
                }
            }


        }
        protected void cfvExisteCodigoMuestra_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (_muestraBc.ExisteMuestra(txtCodigoMuestra.Text))
            {
                args.IsValid = false;
                hdnExisteMuestra.Value = "1";
            }
            else
            {
                args.IsValid = true;
                hdnExisteMuestra.Value = "0";
            }
        }
        protected void cfvExisteCodigoCorrelativo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (_muestraBc.ExisteCorrelativo(txtCodigoCorrelativo.Text))
            {
                args.IsValid = false;
                hdnExisteCorrelativo.Value = "1";
            }
            else
            {
                args.IsValid = true;
                hdnExisteCorrelativo.Value = "0";
            }
        }
        #endregion
        //Metodos ----------------------------------------------------------------
        #region Metodos
        //private void ValidarMarcarMuestraRecibida(string usuario, string codigoMuestra,int idNeonato)
        //{
        //    /*
        //    string dni = txtDNI.Text;
        //    string nombre = txtNombreNeonato.Text;
        //    string apellidos = txtApellidoNeonato.Text;
        //    string fechaNacimiento = DateTime.Today.ToString();

        //    if (txtFechaNacimiento.Text.CompareTo(string.Empty) != 0)
        //    {
        //        if (txtHoraNacimiento.Text.CompareTo(string.Empty) != 0)
        //        {
        //            string fecha = txtFechaNacimiento.Text;
        //            string tiempo = txtHoraNacimiento.Text;
        //            fechaNacimiento = string.Concat(fecha, ' ', tiempo);
        //        }
        //        else
        //        {
        //            fechaNacimiento = txtFechaNacimiento.Text;
        //        }
        //    }
        //    */
        //    _muestraBc.MarcarMuestraRecibida(usuario,codigoMuestra,idNeonato);
        //}
        /*
         * Descripcion: Carga los datos del establecimiento al cual pertenece el usuario logeado en el sistema en caso sea un usuario de alguna sede que no deberia poder registrar
         *              una muestra que no pertenesca a su sede.
         */
        protected void CargarEstablecimientoUsuarioLogeado(string nombreUsuario)
        {
            Usuario usuarioLogeado = _usuarioBc.ObtenerUsuario(nombreUsuario);// HttpContext.Current.User.Identity.Name);
            Establecimiento establecimientoUsuario = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(usuarioLogeado.IdEstablecimiento);

            int idEstablecimiento = usuarioLogeado.IdEstablecimiento;
            if (idEstablecimiento > 0)
            {
                CargarTipoEstablecimiento();
                ddlTipoEstablecimiento.SelectedValue = establecimientoUsuario.idTipoEstablecimiento.ToString();
                ddlTipoEstablecimiento.Enabled = false;
                CargarEstablecimiento(establecimientoUsuario.idTipoEstablecimiento);
                //CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                ddlEstablecimiento.SelectedValue = idEstablecimiento.ToString();
                ddlEstablecimiento.Enabled = false;
            }
            else
            {
                CargarTipoEstablecimiento();
                CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
            }

        }
        private void LimpiarControles()
        {
            //Madre
            txtNombreMadre.Text = string.Empty;
            txtApellidoMadre.Text = string.Empty;
            txtEdadMadre.Text = string.Empty;
            txtDireccionMadre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCelular.Text = string.Empty;

            //Neonato
            ddlNeonato.Visible = false;
            lblNeonato.Visible = false;
            txtNombreNeonato.Text = string.Empty;
            txtApellidoNeonato.Text = string.Empty;
            txtEdadGestacional.Text = string.Empty;
            txtTalla.Text = string.Empty;
            txtPeso.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            radMasculino.Checked = false;
            radFemenino.Checked = true;
            radPrematuroSi.Checked = false;
            radPrematuroNo.Checked = true;
            radTransfundidoSi.Checked = false;
            radTransfundidoNo.Checked = true;

            //Muestra
            txtCodigoMuestra.Text = string.Empty;
            txtTomadoPor.Text = string.Empty;
            txtFechaToma.Text = string.Empty;
            txtHoraNacimiento.Text = string.Empty;
            txtHoraToma.Text = string.Empty;
            txtNotas.Text = string.Empty;
            radTalonSi.Checked = true;
            radTalonNo.Checked = false;
            txtNMuestra.Text = "1";

            //Laboratorio
            txtCodigoCorrelativo.Text = string.Empty;
            


        }
        private void CargarEstablecimientoEdicion(int idEstablecimiento)
        {
            //int idEstablecimiento = idEstablecimiento;
            Establecimiento establecimiento = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(idEstablecimiento);
            ddlTipoEstablecimiento.SelectedValue = establecimiento.idTipoEstablecimiento.ToString();
            CargarEstablecimiento(establecimiento.idTipoEstablecimiento);
            ddlEstablecimiento.SelectedValue = idEstablecimiento.ToString();
        }
        private void LlenarDatosMuestra(Muestra muestra)
        {
            //Muestra
            muestra.CodigoMuestra = txtCodigoMuestra.Text;
            muestra.Notas = txtNotas.Text;
            muestra.Estado = 1;
            muestra.TomadoPor = txtTomadoPor.Text;

            if (ddlEstablecimiento.SelectedValue.CompareTo("0") != 0)
                muestra.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);

            if (txtFechaToma.Text.CompareTo(string.Empty) != 0)
            {
                if (txtHoraToma.Text.CompareTo(string.Empty) != 0)
                {
                    muestra.FechaToma = DateTime.Parse(string.Concat(txtFechaToma.Text, ' ', txtHoraToma.Text));
                }
                else
                {
                    muestra.FechaToma = DateTime.Parse(txtFechaToma.Text);
                }
            }

            muestra.NumMuestra = int.Parse(txtNMuestra.Text);

            muestra.EsTransfundido = radTransfundidoSi.Checked;

            muestra.EsTalon = radTalonSi.Checked;

            //Datos Laboratorio
            if (HttpContext.Current.User.IsInRole("Administrador") || HttpContext.Current.User.IsInRole("Central"))
            {

                muestra.CodigoInternoLab = txtCodigoCorrelativo.Text;
                int codigoCorrelativo = 0;
                if (int.TryParse(txtCodigoCorrelativo.Text, out codigoCorrelativo))
                {
                    muestra.CodigoCorrelativoInt = codigoCorrelativo;
                }

                if (txtFechaRecepcion.Text.CompareTo(string.Empty) != 0)
                {
                    muestra.FechaRecepcion = DateTime.Parse(txtFechaRecepcion.Text);
                }

                
            }

           

        }
        private void LlenarDatosNeonato(Neonato neonato)
        {
            neonato.Nombres = txtNombreNeonato.Text;
            neonato.Apellidos = txtApellidoNeonato.Text;
            //muestra.Neonato.Sexo = radFemenino.Checked == true ? 1 : 2; -- Pendiente de revision
            //muestra.Neonato.EsPrematuro = radPrematuroSI.Checked; 


            if (txtTalla.Text.CompareTo(string.Empty) != 0)
            {
                neonato.Talla = decimal.Parse(txtTalla.Text);
            }

            if (txtPeso.Text.CompareTo(string.Empty) != 0)
            {
                neonato.Peso = decimal.Parse(txtPeso.Text);
            }
            if (txtEdadGestacional.Text.CompareTo(string.Empty) != 0)
            {
                neonato.EdadGestacional = byte.Parse(txtEdadGestacional.Text);
            }

            if (txtFechaNacimiento.Text.CompareTo(string.Empty) != 0)
            {
                if (txtHoraNacimiento.Text.CompareTo(string.Empty) != 0)
                {

                    string fecha = txtFechaNacimiento.Text;
                    string tiempo = txtHoraNacimiento.Text;
                    fecha = string.Concat(fecha, ' ', tiempo);
                    neonato.FechaNacimiento = DateTime.Parse(fecha);

                }
                else
                {
                    neonato.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                }
            }

            if (radFemenino.Checked)
            {
                neonato.Sexo = 1; //femenino
            }
            else
            {
                neonato.Sexo = 2; //masculino
            }

            neonato.EsPrematuro = radPrematuroSi.Checked;
            neonato.Estado = 1;
        }
        private void LlenarDatosMadre(Madre madre)
        {
            //Madre
            madre.Nombres = txtNombreMadre.Text;
            madre.Apellidos = txtApellidoMadre.Text;
            madre.DNI = txtDNI.Text;
            madre.Direccion = txtDireccionMadre.Text;
            madre.Estado = 1;
            if (txtEdadMadre.Text.CompareTo(string.Empty) != 0)
            {
                madre.Edad = short.Parse(txtEdadMadre.Text);
            }
            //madre.HistoriaClinica = txtHClinica.Text;
            madre.Telefono1 = txtTelefono.Text;
            madre.Telefono2 = txtCelular.Text;
            //muestraCompleta.Madre.Departamento = int.Parse(ddlDepartamento.SelectedValue);
            //muestraCompleta.Madre.Provincia = int.Parse(ddlProvincia.SelectedValue);
            //muestraCompleta.Madre.Distrito = int.Parse(ddlDistrito.SelectedValue);
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

            var item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";
        }
        private void LiberarMuestra()
        {
            LimpiarControles();
            //CambiarEstadoControles(false);
            CambiarEstadoControlesMadre(false);
            CambiarEstadoControlesNeonato(false);
            CambiarEstadoControlesMuestra(false);
            txtDNI.Enabled = true;
            txtDNI.Text = string.Empty;
            btnGuardar.Visible = false;
            btnIngresar.Text = "Ingresar";
            btnIngresar.CausesValidation = true;
            hdnIdMadre.Value = string.Empty;
            hdnIdNeonato.Value = string.Empty;
        }
        private void CargarFormularioAgregarMuestra(Madre madre)
        {
            //Establecimiento


            //Madre
            txtApellidoMadre.Text = madre.Apellidos;
            txtNombreMadre.Text = madre.Nombres;
            txtEdadMadre.Text = madre.Edad.ToString();
            txtDireccionMadre.Text = madre.Direccion;
            txtHClinica.Text = madre.HistoriaClinica;
            txtTelefono.Text = madre.Telefono1;
            txtCelular.Text = madre.Telefono2;
            //txtEdadGestacional.Text = madre.

            //Neonato

            int idneonato = CargarListaNeonatos(madre.idMadre);
            CargarControlesNeonato(idneonato);

            //Muestra
            List<Muestra> listaMuestra = _muestraBc.ObtenerMuestrasxIdNeonato(idneonato);
            Muestra muestra = listaMuestra[0];
            CargarEstablecimientoEdicion(int.Parse(muestra.idEstablecimiento.ToString()));
            CambiarEstadoControlesMadre(false);
            CambiarEstadoControlesNeonato(false);
            CambiarEstadoControlesMuestra(true);
            //CambiarEstadoValidadores(true);
            btnIngresar.Text = "Cancelar";
            btnIngresar.CausesValidation = false;
            txtDNI.Enabled = false;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;
            btnGuardar.CausesValidation = true;
        }
        private void CargarFormularioNuevaMuestra()
        {
            CambiarEstadoControlesMadre(true);
            CambiarEstadoControlesNeonato(true);
            CambiarEstadoControlesMuestra(true);
            //ViewState.Add("codigoMuestra", codigoMuestra);
            btnIngresar.Text = "Cancelar";
            btnIngresar.CausesValidation = false;
            btnGuardar.Visible = true;
            //CambiarEstadoControles(true);
            txtDNI.Enabled = false;
            LimpiarControles();
        }
        private void CambiarEstadoControlesMadre(bool habilitado)
        {
            //Madre--------------------
            txtNombreMadre.Enabled = habilitado;
            txtApellidoMadre.Enabled = habilitado;
            txtDireccionMadre.Enabled = habilitado;
            txtTelefono.Enabled = habilitado;
            txtCelular.Enabled = habilitado;
            txtCodigoMuestra.Enabled = habilitado;
            txtHClinica.Enabled = habilitado;
            txtEdadMadre.Enabled = habilitado;

            //Validators
            //rfvCodigoMuestra.Enabled = habilitado;
            rfvApellidoMadre.Enabled = habilitado;
            rfvEdadGestacional.Enabled = habilitado;
            rfvNombreMadre.Enabled = habilitado;
            rfvTelefono.Enabled = habilitado;
            rfvApellidosNeonato.Enabled = habilitado;

            //rfvFechaToma.Enabled = habilitado;
            //rfvHoraUltima.Enabled = habilitado;
            //rfvTipoEstablecimiento.Enabled = habilitado;
            //rfvEstablecimiento.Enabled = habilitado;

        }
        private void CambiarEstadoControlesNeonato(bool habilitado)
        {
            //Neonato--------------------
            //ddlNeonato.Enabled = habilitado;
            txtNombreNeonato.Enabled = habilitado;
            txtApellidoNeonato.Enabled = habilitado;
            txtEdadGestacional.Enabled = habilitado;
            txtFechaNacimiento.Enabled = habilitado;
            txtHoraNacimiento.Enabled = habilitado;
            txtTalla.Enabled = habilitado;
            txtPeso.Enabled = habilitado;

            rfvFechaNacimiento.Enabled = habilitado;
            rfvApellidosNeonato.Enabled = habilitado;

            radFemenino.Enabled = habilitado;
            radMasculino.Enabled = habilitado;
            radTransfundidoNo.Enabled = habilitado;
            radTransfundidoSi.Enabled = habilitado;
            radPrematuroNo.Enabled = habilitado;
            radPrematuroSi.Enabled = habilitado;
        }
        private void CambiarEstadoControlesMuestra(bool habilitado)
        {
            //Establecimiento--------------------
            //txtCodRenaes.Enabled = habilitado;
            if (hdnSede.Value.CompareTo("1") != 0)
            {
                ddlTipoEstablecimiento.Enabled = habilitado;
                ddlEstablecimiento.Enabled = habilitado;
            }


            //txtFechaRecepcion.Enabled = habilitado;

            //Muestra--------------------
            txtCodigoMuestra.Enabled = habilitado;
            txtTomadoPor.Enabled = habilitado;
            txtFechaToma.Enabled = habilitado;
            txtHoraToma.Enabled = habilitado;
            txtNotas.Enabled = habilitado;
            radMasculino.Enabled = habilitado;
            radTransfundidoSi.Enabled = habilitado;
            radTalonSi.Enabled = habilitado;
            //radTipoGestacionMultiple.Enabled = habilitado;
            //radNMuestraSegunda.Enabled = habilitado;
            txtNMuestra.Enabled = habilitado;
            radPrematuroSi.Enabled = habilitado;
            //txtCodigoCorrelativo.Enabled = habilitado;txtUltLactancia.Enabled = habilitado;

            //laboratorio
            if (HttpContext.Current.User.IsInRole("Administrador") || HttpContext.Current.User.IsInRole("Central"))
            {
                txtCodigoCorrelativo.Enabled = habilitado;
                txtFechaRecepcion.Enabled = habilitado;
                
                rfvFechaRecepcion.Enabled = habilitado;
                rfvCodigoCorrelativo.Enabled = habilitado;
                cfvExisteCodigoCorrelativo.Enabled = habilitado;
            }

            //Validators
            rfvCodigoMuestra.Enabled = habilitado;
            rfvFechaToma.Enabled = habilitado;
            rfvTipoEstablecimiento.Enabled = habilitado;
            rfvEstablecimiento.Enabled = habilitado;


        }
        private void LimpiarControlesNeonato()
        {
            //Neonato
            txtNombreNeonato.Text = string.Empty;
            txtApellidoNeonato.Text = string.Empty;
            txtEdadGestacional.Text = string.Empty;
            txtTalla.Text = string.Empty;
            txtPeso.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtHoraNacimiento.Text = string.Empty;
            radMasculino.Checked = false;
            radFemenino.Checked = true;
            radPrematuroSi.Checked = false;
            radPrematuroNo.Checked = true;
            radTransfundidoSi.Checked = false;
            radTransfundidoNo.Checked = true;
        }

        /*
         * Metodo: CargarListaNeonatos
         * Descripcion: popula el combobox de neonatos que se habilita cuando el DNI ingresado corresponde a la madre de un paciente que ya ha sido registrado
         *              previamente, permitiendo si es el caso agregar una nueva muestra del mismo paciente o un nuevo paciente cuya madre sea la misma. 
         * Resultado: El metodo carga el combobox y devuelve el id del primer neonato de la lista de neonatos para que los datos de este neonato puedan
         *            ser cargados en el formulario.
         */
        private int CargarListaNeonatos(int idMadre)
        {
            int idNeonato = 0;
            ddlNeonato.DataTextField = "Neonato";
            ddlNeonato.DataValueField = "idNeonato";

            if (idMadre != 0)
            {
                //List<Neonato> listaNeonatos = _neonatoBc.ObtenerNeonatosxIdMadre(idMadre);
                var dt = _neonatoBc.ObtenerDataTableNeonatosxidMadre(idMadre);
                ddlNeonato.DataSource = dt;
                ddlNeonato.DataBind();
                idNeonato = int.Parse(dt.Rows[0]["idNeonato"].ToString());
            }
            var item = new ListItem("--Nuevo--", "0");
            ddlNeonato.Items.Add(item);
            return idNeonato;
        }

        /*
         *  Metodo:  CargarControlesNeonato
         *  Descripcion:    Popula los controles del formulario que corresponden a los datos del neonato.
         */
        private void CargarControlesNeonato(int idNeonato)
        {
            //Neonato
            Neonato neonato = _neonatoBc.ObtenerNeonatoxIdNeonato(idNeonato);
            hdnIdNeonato.Value = neonato.idNeonato.ToString();
            txtApellidoNeonato.Text = neonato.Apellidos;
            txtNombreNeonato.Text = neonato.Nombres;
            txtEdadGestacional.Text = neonato.EdadGestacional.ToString();
            if (neonato.FechaNacimiento != null)
            {
                txtFechaNacimiento.Text = neonato.FechaNacimiento.Value.ToShortDateString();
                txtHoraNacimiento.Text = neonato.FechaNacimiento.Value.ToShortTimeString();
            }
            if (neonato.Sexo != null)
            {
                if (neonato.Sexo == 1)
                {
                    radFemenino.Checked = true;
                }
                else
                {
                    radMasculino.Checked = true;
                }
            }

            txtPeso.Text = neonato.Peso.ToString();
            txtTalla.Text = neonato.Talla.ToString();

        }
        #endregion

        #region EditarMuestra

        private void CargarFormularioEditarMuestra(string codigoMuestra)
        {
            //MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
            MuestraCompletaBE muestraCompleta = _muestraCompletaBc.ObtenerMuestra(codigoMuestra);
            //cargamos lista de neonatos y una opcion NEW en el combobox por si se desea crear un nuevo neonato
            CargarListaNeonatos(muestraCompleta.Madre.idMadre);
            ddlNeonato.SelectedValue = muestraCompleta.Neonato.idNeonato.ToString();
            hdnIdMadre.Value = muestraCompleta.Madre.idMadre.ToString();
            hdnIdNeonato.Value = muestraCompleta.Neonato.idNeonato.ToString();
            hdnIdMuestra.Value = muestraCompleta.Muestra.idMuestra.ToString();
            hdnCodigoCorrelativo.Value = muestraCompleta.Muestra.CodigoInternoLab;
            //, muestraCompleta.Neonato.idNeonato);
            //Cargamos los controles del formulario con los datos de la muestra.
            //CargarControles(muestraCompleta);
            CargarControlesMadre(muestraCompleta.Madre);
            CargarControlesNeonato(muestraCompleta.Neonato);
            CargarControlesMuestra(muestraCompleta.Muestra);
            CargarControlesLaboratorio(muestraCompleta.Muestra);
            CargarEstablecimientoEdicion(int.Parse(muestraCompleta.Muestra.idEstablecimiento.ToString()));

            //if (muestraCompleta.Muestra.idEstablecimiento != null)
            //{
            //    CargarEstablecimientoEdicion(int.Parse(muestraCompleta.Muestra.idEstablecimiento.ToString()));
            //}

            //----ViewState.Add("codigoMuestra", codigoMuestra);
            //btnIngresar.Text = "Cancelar";
            //btnIngresar.CausesValidation = false;
            //Habilitamos la edicion de los campos del formulario
            //CambiarEstadoControles(true);
            //txtCodigoMuestra.Enabled = false;------------------------------------------------
            //btnGuardar.Visible = true;
        }

        private void CargarControlesMadre(Madre madre)
        {
            //Madre
            txtNombreMadre.Text = madre.Nombres;
            txtApellidoMadre.Text = madre.Apellidos;
            txtTelefono.Text = madre.Telefono1;
            txtCelular.Text = madre.Telefono2;
            txtDireccionMadre.Text = madre.Direccion;
            txtEdadMadre.Text = madre.Edad.ToString();
            txtDNI.Text = madre.DNI;
            //txtHClinica.Text = muestraCompleta.Madre.HistoriaClinica;
        }
        private void CargarControlesNeonato(Neonato neonato)
        {
            //Neonato
            txtNombreNeonato.Text = neonato.Nombres;
            txtApellidoNeonato.Text = neonato.Apellidos;
            txtEdadGestacional.Text = neonato.EdadGestacional.ToString();
            txtTalla.Text = neonato.Talla.ToString();
            txtPeso.Text = neonato.Peso.ToString();
            if (neonato.FechaNacimiento != null)
            {
                txtFechaNacimiento.Text = neonato.FechaNacimiento.Value.ToShortDateString();
                txtHoraNacimiento.Text = neonato.FechaNacimiento.Value.ToShortTimeString();
            }
            if (neonato.Sexo != null)
            {
                if (neonato.Sexo == 1)
                {
                    radFemenino.Checked = true;
                }
                else
                {
                    radMasculino.Checked = true;
                }
            }
        }
        private void CargarControlesMuestra(Muestra muestra)
        {
            //Muestra
            if (muestra.FechaToma != null)
            {
                txtFechaToma.Text = muestra.FechaToma.Value.ToShortDateString();
                txtHoraToma.Text = muestra.FechaToma.Value.ToShortTimeString();
            }

           

            txtTomadoPor.Text = muestra.TomadoPor;
            txtNotas.Text = muestra.Notas;

            if (muestra.EsTransfundido == true)
            {
                radTransfundidoSi.Checked = true;
            }
            else
            {
                radTransfundidoNo.Checked = true;
            }


            if (muestra.EsTalon == true)
            {
                radTalonSi.Checked = true;
                radTalonNo.Checked = false;
            }
            else
            {
                radTalonSi.Checked = false;
                radTalonNo.Checked = true;
            }
            //txtTiempoLactanciaDias.Text = muestra.TiempoLactanciaDias.ToString();
            //txtTiempoLactanciaHoras.Text = muestra.TiempoLactanciaHoras.ToString();

            txtNMuestra.Text = muestra.NumMuestra.ToString();

            //if (muestra.idEstablecimiento != null)
            //{
            //    CargarEstablecimientoEdicion(int.Parse(muestra.idEstablecimiento.ToString()));
            //}


            //txtCodRenaes.Text = muestra.CodRenaes;

            txtCodigoCorrelativo.Text = muestra.CodigoInternoLab;
        }
        private void CargarControlesLaboratorio(Muestra muestra)
        {
            //Datos Laboratorio
            if (HttpContext.Current.User.IsInRole("Administrador") || HttpContext.Current.User.IsInRole("Central"))
            {

                txtCodigoCorrelativo.Text = muestra.CodigoInternoLab;
                //txtFechaRecepcion.Text = muestra.FechaRecepcion.ToString();

                if (muestra.FechaRecepcion != null)
                {
                    DateTime fechaAux;
                    if (DateTime.TryParse(muestra.FechaRecepcion.ToString(), out fechaAux))
                    {
                        txtFechaRecepcion.Text = fechaAux.ToShortDateString();
                    }
                }

                
            }
        }


        #endregion
    }
}