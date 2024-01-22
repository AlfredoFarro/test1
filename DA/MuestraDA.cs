using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BE;

namespace DA
{
    public class MuestraDA
    {
        readonly AccesoDB db = new AccesoDB();

        //public List<ResultadoDNIResult> reporteDNI(string dni)
        //{
        //    List<ResultadoDNIResult> resultados = db.dc.ResultadoDNI(dni).ToList();
        //    return resultados;
        //}

        //public DataSet ObtenerReporte(string dni)
        //{
        //    var aux = new DataSet();
        //    var dt = new DataTable();
        //    var cn = new SqlConnection(db.conexionTamiLife);

        //    var comando = new SqlCommand();

        //    comando.CommandText = "ResultadoDNI";
        //    comando.CommandType = CommandType.StoredProcedure;
        //    comando.Connection = cn;
        //    comando.Parameters.AddWithValue("@dni", dni);
        //    cn.Open();

        //    var adapter = new SqlDataAdapter(comando);

        //    adapter.Fill(dt);
        //    cn.Close();

        //    aux.Tables.Add(dt);
        //    return aux;
        //}
        public void InsertarMuestras(List<Muestra> muestras)
        {
            db.dc.Muestras.InsertAllOnSubmit(muestras);
            try
            {
                db.dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Muestra InsertarMuestra(Muestra muestra)
        {
            db.dc.Muestras.InsertOnSubmit(muestra);
            try
            {
                db.dc.SubmitChanges();
                return muestra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarMuestra(Muestra muestra)
        {
            try
            {
                db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ExisteMuestra(string codigoMuestra)
        {
            return db.dc.Muestras.Where(m => m.CodigoMuestra == codigoMuestra && m.Estado == 1).Any();
        }
        public bool ExisteCorrelativo(string codigoCorrelativo)
        {
            return db.dc.Muestras.Where(m => m.CodigoInternoLab == codigoCorrelativo && m.Estado == 1).Any();
        }
        public bool CoincideCorrelativoMuestra(string codigoMuestra, string codigoCorrelativo)
        {
            return db.dc.Muestras.Where(m => m.CodigoMuestra == codigoMuestra && m.CodigoInternoLab == codigoCorrelativo).Any();
        }
        public Muestra ObtenerMuestraxCodigoMuestra(string codigoMuestra)
        {
            if (codigoMuestra.CompareTo(string.Empty) != 0)
                return db.dc.Muestras.Where(m => m.CodigoMuestra == codigoMuestra && m.Estado == 1).FirstOrDefault();
            else
            {
                var muestra = new Muestra { Estado = 0 };
                return muestra;
            }

        }
        public Muestra ObtenerMuestraxIdMuestra(int idMuestra)
        {
            return (from m in db.dc.Muestras where m.idMuestra == idMuestra select m).First();
        }

        //public List<Vista_BuscarPaciente> ObtenerMuestras(string codigoMuestra, string apellidosNeonato, string apellidosMadre, string DNI, string HClinica, int anho, int mes)
        //{
        //    var lista = new List<Vista_BuscarPaciente>();

        //    if (codigoMuestra.CompareTo(string.Empty) != 0)
        //        return (from m in db.dc.Vista_BuscarPacientes where m.CodigoMuestra.Contains(codigoMuestra) select m).ToList();

        //    if (DNI.CompareTo(string.Empty) != 0)
        //        return (from m in db.dc.Vista_BuscarPacientes where m.DNI.Contains(DNI) select m).ToList();

        //    //if (HClinica.CompareTo(string.Empty) != 0)
        //    //    return (from m in db.dc.Vista_BuscarPaciente where m.HistoriaClinica.Contains(HClinica) select m).ToList();

        //    if (apellidosMadre.CompareTo(string.Empty) != 0)
        //        return (from m in db.dc.Vista_BuscarPacientes where m.ApellidosMadre.Contains(apellidosMadre) select m).ToList();

        //    if (apellidosNeonato.CompareTo(string.Empty) != 0)
        //        return (from m in db.dc.Vista_BuscarPacientes where m.ApellidosNeonato.Contains(apellidosNeonato) select m).ToList();

        //    if (anho > 0)
        //    {
        //        lista = (from m in db.dc.Vista_BuscarPacientes where m.FechaNacimiento.Value.Year == anho select m).ToList(); //&& m.FechaNacimiento.Value.Month == mes select m).ToList();
        //        if (mes > 0)
        //            lista = lista.Where(p => p.FechaNacimiento.Value.Month == mes).ToList();

        //        return lista;
        //    }
        //    else
        //    {
        //        return db.dc.Vista_BuscarPacientes.Take(100).ToList();
        //    }
        //}
        public List<Vista_ResultadosEstablecimiento> ObtenerMuestrasxEstablecimiento(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni, int determinacion)
        {
            var lista = new List<Vista_ResultadosEstablecimiento>();

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.idEstablecimiento == idEstablecimiento && r.Publicado == true).ToList();
            }
            else
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.Publicado == true).ToList();
            }

            if (determinacion > 0)
            {
                lista = lista.Where(r => r.rdcDeterminationLevel > 20).ToList();
            }
            else
            {
                lista = lista.Where(r => r.rdcDeterminationLevel == 20).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.CodigoMuestra.Contains(codigoMuestra)).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.DNI.Contains(dni)).ToList();

            if (apellidosMadre.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosMadre.Contains(apellidosMadre)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosNeonato.Contains(apellidosNeonato)).ToList();

            //var lista2 = lista.GroupBy(r => r.idNeonato).ToList();


            return lista;

            //return (from m in db.dc.Vista_BuscarPaciente where m.FechaNacimiento.Value.Year == anho && m.FechaNacimiento.Value.Month == mes orderby m.idMuestra descending select m).Take(10).ToList();


        }
        //(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, string apellidosNeonato, string apellidosMadre,string codigoMuestra,string dni, int idPrueba, int determinacion)
        public List<Vista_BuscarPaciente> ObtenerMuestras(int estado, int idEstablecimiento, string codigoMuestra, string codigoCorrelativo, string apellidosNeonato,
                                                            string apellidosMadre, string dni, bool usarInicio, bool usarFin, DateTime dateInicio,
                                                            DateTime dateFin, bool usarInicioToma, bool usarFinToma, DateTime dateInicioToma,
                                                            DateTime dateFinToma)
        {
            var lista = new List<Vista_BuscarPaciente>();

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_BuscarPacientes.Where(p => p.idEstablecimiento == idEstablecimiento && p.Estado == estado).ToList();
            }
            else
            {
                lista = db.dc.Vista_BuscarPacientes.Where(p => p.Estado == estado).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.CodigoMuestra == codigoMuestra).ToList();

            if (codigoCorrelativo.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.CodigoInternoLab == codigoCorrelativo).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.DNI.Contains(dni)).ToList();

            if (apellidosMadre.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.ApellidosMadre.Contains(apellidosMadre)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.ApellidosNeonato.Contains(apellidosNeonato)).ToList();

            if (usarInicio && usarFin)
            {
                lista = lista.Where(p => p.FechaNacimiento >= dateInicio && p.FechaNacimiento <= dateFin).ToList();
            }
            if (usarInicio && !usarFin)
            {
                lista = lista.Where(p => p.FechaNacimiento >= dateInicio).ToList();
            }

            if (!usarInicio && usarFin)
            {
                lista = lista.Where(p => p.FechaNacimiento <= dateFin).ToList();
            }

            if (usarInicioToma && usarFinToma)
            {
                lista = lista.Where(p => p.FechaToma >= dateInicioToma && p.FechaToma <= dateFinToma).ToList();
            }
            if (usarInicioToma && !usarFinToma)
            {
                lista = lista.Where(p => p.FechaToma >= dateInicioToma).ToList();
            }

            if (!usarInicioToma && usarFinToma)
            {
                lista = lista.Where(p => p.FechaToma <= dateFinToma).ToList();
            }

            return lista;
            //return (from m in db.dc.Vista_BuscarPaciente where m.FechaNacimiento.Value.Year == anho && m.FechaNacimiento.Value.Month == mes orderby m.idMuestra descending select m).Take(10).ToList();


        }

        public List<Vista_BuscarPaciente> ObtenerMuestras(int idEstablecimiento, string codigoMuestra, bool exportada, string digitador)
        {
            var lista = db.dc.Vista_BuscarPacientes.Where(p => p.Importado == exportada).ToList();

            if (idEstablecimiento > 0)
            {
                lista = lista.Where(p => p.idEstablecimiento == idEstablecimiento).ToList();
            }
            if (digitador.CompareTo("0") != 0)
            {
                lista = lista.Where(p => p.CreadoPor == digitador).ToList();
            }
            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(p => p.CodigoMuestra == codigoMuestra).ToList();

            return lista;
        }

        //public List<Vista_CodigoMuestraxDNI> ObtenerCantidadMuestras(string dni)
        //{
        //    return db.dc.Vista_CodigoMuestraxDNIs.Where(m => m.DNI == dni).ToList();

        //}

        public List<Muestra> ObtenerMuestrasxIdNeonato(int idNeonato)
        {
            return db.dc.Muestras.Where(m => m.idNeonato == idNeonato).ToList();
        }

        public List<Muestra> ObtenerMuestrasBloqueadas()
        {
            return db.dc.Muestras.Where(m => m.Estado == 2).ToList();
        }

        public List<Vista_MuestrasxNeonato> ObtenerMuestrasxNeonato(int idNeonato)
        {
            return db.dc.Vista_MuestrasxNeonatos.Where(m => m.idNeonato == idNeonato).ToList();
        }
        public List<Muestra> ObtenerListaMuestrasxEnvio(int idEnvio)
        {
            return db.dc.Muestras.Where(m => m.idEnvio == idEnvio).ToList();
        }

        public List<Vista_Muestra> ObtenerListaMuestrasRechazadasEstablecimiento(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            var lista = new List<Vista_Muestra>();

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.Estado == 1 && m.MuestraAceptada == false && m.RecibioMuestra == false && m.RetiradaPanelAlertas == false).ToList();
            }
            else
            {
                lista = db.dc.Vista_Muestras.Where(m => m.Estado == 1 && m.MuestraAceptada == false && m.RecibioMuestra == false && m.RetiradaPanelAlertas == false).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.CodigoMuestra.Contains(codigoMuestra)).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.DNI.Contains(dni)).ToList();

            if (apellidosMadre.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosMadre.Contains(apellidosMadre)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosNeonato.Contains(apellidosNeonato)).ToList();

            return lista.OrderBy(r => r.idMuestra).ToList();
            //return db.dc.Vista_Muestras.Where(m => m.Estado == 1 && m.MuestraAceptada == false && m.idEstablecimiento == idEstablecimiento).ToList();
        }

        public List<Vista_Muestra> ObtenerListaMuestrasRechazadas(int idEstablecimiento, string apellidosNeonato, string codigoCorrelativo, string codigoMuestra, string dni, bool usarInicio, bool usarFin, DateTime dateInicioRecepcion, DateTime dateFinRecepcion)
        {
            var lista = new List<Vista_Muestra>();

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.Estado == 1 && m.MuestraAceptada == false && m.RecibioMuestra == false && m.FechaRecepcion != null).ToList();
            }
            else
            {
                lista = db.dc.Vista_Muestras.Where(m => m.Estado == 1 && m.MuestraAceptada == false && m.RecibioMuestra == false && m.FechaRecepcion != null).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(m => m.CodigoMuestra == codigoMuestra).ToList();

            if (codigoCorrelativo.CompareTo(string.Empty) != 0)
                lista = lista.Where(m => m.CodigoInternoLab == codigoCorrelativo).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(m => m.DNI.Contains(dni)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(m => m.ApellidosNeonato.Contains(apellidosNeonato)).ToList();

            if (usarInicio && usarFin)
            {
                lista = lista.Where(m => m.FechaRecepcion >= dateInicioRecepcion && m.FechaRecepcion <= dateFinRecepcion).ToList();
            }
            if (usarInicio && !usarFin)
            {
                lista = lista.Where(m => m.FechaRecepcion >= dateInicioRecepcion).ToList();
            }

            if (!usarInicio && usarFin)
            {
                lista = lista.Where(m => m.FechaRecepcion <= dateFinRecepcion).ToList();
            }

            return lista.OrderBy(m => m.CodigoInternoLab).ToList();
            /*
            var lista = db.dc.Vista_Muestras.Where(m => m.Estado == 1 && m.MuestraAceptada == false && m.RecibioMuestra == false && m.FechaRecepcion != null).ToList();
           
            return lista.Where(m => m.FechaRecepcion.Value.Year == fechaRecepcion.Year && m.FechaRecepcion.Value.Month == fechaRecepcion.Month && m.FechaRecepcion.Value.Day == fechaRecepcion.Day).ToList();
        
             */
        }


        public bool MarcarMuestrasRecibidas(string usuario, List<string> listaCodigosMuestras)
        {
            try
            {
                var lista = db.dc.Muestras.Where(m => m.RecibioMuestra == false && listaCodigosMuestras.Contains(m.CodigoMuestra)).ToList();
                lista.ForEach(m =>
                {
                    m.ModificadoPor = usuario;
                    m.RecibioMuestra = true;
                    m.FechaModificacion = DateTime.Today;
                    //m.FechaTransfucion = DateTime.Today;
                    m.AfiliacionSIS = "Manualmente Marcado_" + usuario + "_" + DateTime.Today.ToShortDateString();
                });

                db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
        }
        public bool MarcarMuestrasRetiradas(string usuario, List<string> listaCodigosMuestras)
        {
            try
            {
                var lista = db.dc.Muestras.Where(m => m.RecibioMuestra == false && listaCodigosMuestras.Contains(m.CodigoMuestra)).ToList();
                lista.ForEach(m =>
                {
                    m.ModificadoPor = usuario;
                    m.RetiradaPanelAlertas = true;
                    m.FechaModificacion = DateTime.Today;
                    //m.FechaTransfucion = DateTime.Today;
                    m.AfiliacionSIS = "RetiradoPor_" + usuario + "_" + DateTime.Today.ToShortDateString();
                });

                db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
        }

        #region PublicarMuestras

        public void ExportarMuestrasINMP()
        {
            string query = string.Concat("SELECT m.CodigoInternoLab AS CodigoMuestra,",
                                                "ma.Nombres AS NombreMadre,",
                                                "ma.Apellidos AS ApellidosMadre,",
                                                "ma.Nombres + ' ' + ma.Apellidos AS 'Madre',",
                                                "n.Nombres + ' ' + n.Apellidos AS 'Neonato',",
                                                "ma.DNI,",
                                                "'' AS HCLinica, ",
                                                "n.FechaNacimiento,",
                                                "m.FechaToma,",
                                                "1 AS Estado,",
                                                "m.FechaRecepcion, ",
                                                "e.Codigo AS CodigoEstablecimiento,",
                                                "0 AS TipoEstablecimiento,",
                                                "e.Nombre AS Establecimiento,",
                                                "n.EdadGestacional,",
                                                "ma.Departamento,",
                                                "ma.Provincia,",
                                                "ma.Distrito,",
                                                "ma.Telefono1,",
                                                "ma.Telefono2,",
                                                "n.Peso/1000 AS 'Peso', ",
                                                "n.Talla,",
                                                "(CASE WHEN n.Sexo = 1 THEN 'FEMENINO' WHEN n.Sexo = 2 THEN 'MASCULINO' END ) AS 'Sexo', ",
                                                "(CASE WHEN n.EsPrematuro = 0 THEN 'NO' WHEN n.EsPrematuro = 1 THEN 'SI' END ) AS 'EsPrematuro', ",
                                                "'' As EsTransfundido,",
                                                "m.CodigoInternoLab AS CodigoCorrelativo,",
                                                "m.Notas,",
                                                "m.NumMuestra AS NumRepeticion,",
                                                "GETDATE() AS FechaRegistro,",
                                                "1 AS 'TipoMigracion' ",
                                                "FROM [TamiLifeINMP].[dbo].muestra m ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Neonato n ON n.idNeonato = m.idNeonato ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Madre ma ON ma.idMadre = n.idMadre ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Establecimiento e ON e.idEstablecimiento = m.idEstablecimiento ",
                                                "WHERE m.Importado = 0");
            DataTable dt = db.QueryDataTable(query);
            string MuestrasEnviar = "('";
            foreach (DataRow dr in dt.Rows)
            {

               	//EsPrematuro	EsTransfundido	CodigoCorrelativo	Notas	NumRepeticion	FechaRegistro	TipoMigracion

                MuestrasEnviar = string.Concat(MuestrasEnviar, dr["CodigoMuestra"], "','",
                dr["NombreMadre"],"','",
                dr["ApellidosMadre"],"','",
                dr["Madre"],"','",
                dr["Neonato"],"','",
                dr["DNI"],"','",
                dr["HCLinica"],"','",
                dr["FechaNacimiento"],"','",
                dr["FechaToma"],"',",
                dr["Estado"],",'",
                dr["FechaRecepcion"],"','",
                dr["CodigoEstablecimiento"],"','",
                dr["TipoEstablecimiento"],"','",
                dr["Establecimiento"],"',",
                dr["EdadGestacional"],",'",
                dr["Departamento"],"','",
                dr["Provincia"],"','",
                dr["Distrito"],"','",
                dr["Telefono1"],"','",
                dr["Telefono2"],"',",
                dr["Peso"].ToString().Replace(',','.').Substring(0,6),",",
                dr["Talla"].ToString().Replace(',', '.'), ",'",
                dr["Sexo"],"','",
                dr["EsPrematuro"],"','",
                dr["EsTransfundido"],"','",
                dr["CodigoCorrelativo"],"','",
                dr["Notas"],"',",
                dr["NumRepeticion"],",'",
                dr["FechaRegistro"],"','",
                dr["TipoMigracion"],"'),('");
                
            }
            MuestrasEnviar = MuestrasEnviar.Remove(MuestrasEnviar.Length - 3, 3);
            string query2 = string.Concat("INSERT INTO [dbo].[Muestra_INMP]",
                                  "([CodigoMuestra]",
                                  ",[NombreMadre]",
                                  ",[ApellidoMadre]",
                                  ",[Madre]",
                                  ",[Neonato]",
                                  ",[DNI]",
                                  ",[HistoriaClinica]",
                                  ",[FechaNacimiento]",
                                  ",[FechaToma]",
                                  ",[Estado]",
                                  ",[FechaRecepcion]",
                                  ",[CodigoEstablecimiento]",
                                  ",[TipoEstablecimiento]",
                                  ",[Establecimiento]",
                                  ",[EdadGestacional]",
                                  ",[Departamento]",
                                  ",[Provincia]",
                                  ",[Distrito]",
                                  ",[Telefono]",
                                  ",[Telefono2]",
                                  ",[Peso]",
                                  ",[Talla]",
                                  ",[Sexo]",
                                  ",[esPrematuro]",
                                  ",[esTransfundido]",
                                  ",[CodigoCorrelativo]",
                                  ",[Notas]",
                                  ",[NumRepeticion]",
                                  ",[FechaRegistro]",
                                  ",[TipoMigracion]) ",
                                  " VALUES ", MuestrasEnviar);
            db.ExecuteNoQueryINMP(query2);
        }

        public void ExportarMuestrasINMP(string listaCodigosCorrelativos)
        {
            string query = string.Concat("SELECT m.CodigoInternoLab AS CodigoMuestra,",
                                                "ma.Nombres AS NombreMadre,",
                                                "ma.Apellidos AS ApellidosMadre,",
                                                "ma.Nombres + ' ' + ma.Apellidos AS 'Madre',",
                                                "n.Nombres + ' ' + n.Apellidos AS 'Neonato',",
                                                "ma.DNI,",
                                                "'' AS HCLinica, ",
                                                "n.FechaNacimiento,",
                                                "m.FechaToma,",
                                                "1 AS Estado,",
                                                "m.FechaRecepcion, ",
                                                "e.Codigo AS CodigoEstablecimiento,",
                                                "0 AS TipoEstablecimiento,",
                                                "e.Nombre AS Establecimiento,",
                                                "n.EdadGestacional,",
                                                "ma.Departamento,",
                                                "ma.Provincia,",
                                                "ma.Distrito,",
                                                "ma.Telefono1,",
                                                "ma.Telefono2,",
                                                "n.Peso/1000 AS 'Peso', ",
                                                "n.Talla,",
                                                "(CASE WHEN n.Sexo = 1 THEN 'FEMENINO' WHEN n.Sexo = 2 THEN 'MASCULINO' END ) AS 'Sexo', ",
                                                "(CASE WHEN n.EsPrematuro = 0 THEN 'NO' WHEN n.EsPrematuro = 1 THEN 'SI' END ) AS 'EsPrematuro', ",
                                                "'' As EsTransfundido,",
                                                "m.CodigoInternoLab AS CodigoCorrelativo,",
                                                "m.Notas,",
                                                "m.NumMuestra AS NumRepeticion,",
                                                "GETDATE() AS FechaRegistro,",
                                                "1 AS 'TipoMigracion' ",
                                                "FROM [TamiLifeINMP].[dbo].muestra m ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Neonato n ON n.idNeonato = m.idNeonato ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Madre ma ON ma.idMadre = n.idMadre ",
                                                "LEFT JOIN [TamiLifeINMP].[dbo].Establecimiento e ON e.idEstablecimiento = m.idEstablecimiento ",
                                                "WHERE m.CodigoInternoLab IN (", listaCodigosCorrelativos, ")");
            DataTable dt = db.QueryDataTable(query);
            string MuestrasEnviar = "('";
            foreach (DataRow dr in dt.Rows)
            {

                //EsPrematuro	EsTransfundido	CodigoCorrelativo	Notas	NumRepeticion	FechaRegistro	TipoMigracion
                string strPeso = string.Empty;
                string strTalla = string.Empty;

                if (dr["Peso"] != null && dr["Peso"].ToString() != string.Empty)
                {
                    strPeso = dr["Peso"].ToString().Replace(',', '.');
                    if (strPeso.Length > 6) strPeso = strPeso.Substring(0, 6);
                }
                else strPeso = "NULL";

                if (dr["Talla"] != null && dr["Talla"].ToString() != string.Empty) strTalla = dr["Talla"].ToString().Replace(',', '.');
                else strTalla = "NULL";

                MuestrasEnviar = string.Concat(MuestrasEnviar, dr["CodigoMuestra"], "','",
                dr["NombreMadre"], "','",
                dr["ApellidosMadre"], "','",
                dr["Madre"], "','",
                dr["Neonato"].ToString().Trim(), "','",
                dr["DNI"], "','",
                dr["HCLinica"], "','",
                dr["FechaNacimiento"], "','",
                dr["FechaToma"], "',",
                dr["Estado"], ",'",
                dr["FechaRecepcion"], "','",
                dr["CodigoEstablecimiento"], "','",
                dr["TipoEstablecimiento"], "','",
                dr["Establecimiento"], "',",
                dr["EdadGestacional"], ",'",
                dr["Departamento"], "','",
                dr["Provincia"], "','",
                dr["Distrito"], "','",
                dr["Telefono1"], "','",
                dr["Telefono2"], "',",
                strPeso, ",",
                strTalla, ",'",
                dr["Sexo"], "','",
                dr["EsPrematuro"], "','",
                dr["EsTransfundido"], "','",
                dr["CodigoCorrelativo"], "','",
                dr["Notas"], "',",
                dr["NumRepeticion"], ",'",
                dr["FechaRegistro"], "','",
                dr["TipoMigracion"], "'),('");

            }
            MuestrasEnviar = MuestrasEnviar.Remove(MuestrasEnviar.Length - 3, 3);
            string query2 = string.Concat("INSERT INTO [dbo].[Muestra_INMP]",
                                  "([CodigoMuestra]",
                                  ",[NombreMadre]",
                                  ",[ApellidoMadre]",
                                  ",[Madre]",
                                  ",[Neonato]",
                                  ",[DNI]",
                                  ",[HistoriaClinica]",
                                  ",[FechaNacimiento]",
                                  ",[FechaToma]",
                                  ",[Estado]",
                                  ",[FechaRecepcion]",
                                  ",[CodigoEstablecimiento]",
                                  ",[TipoEstablecimiento]",
                                  ",[Establecimiento]",
                                  ",[EdadGestacional]",
                                  ",[Departamento]",
                                  ",[Provincia]",
                                  ",[Distrito]",
                                  ",[Telefono]",
                                  ",[Telefono2]",
                                  ",[Peso]",
                                  ",[Talla]",
                                  ",[Sexo]",
                                  ",[esPrematuro]",
                                  ",[esTransfundido]",
                                  ",[CodigoCorrelativo]",
                                  ",[Notas]",
                                  ",[NumRepeticion]",
                                  ",[FechaRegistro]",
                                  ",[TipoMigracion]) ",
                                  " VALUES ", MuestrasEnviar);
            db.ExecuteNoQueryINMP(query2);

            string query3 = string.Concat("UPDATE Muestra SET Importado = 1 WHERE CodigoInternoLab IN (", listaCodigosCorrelativos, ")");
            db.QueryDataTable(query3);
        }

        #endregion

        #region ValidacionAutomatica2daMuestra

        //public bool MarcarMuestraRecibidaRechazadas(string usuario,string dni,string nombres,string apellidos,DateTime fechaNacimiento,string codigoMuestra)
        public bool MarcarMuestraRecibidaRechazadas(string usuario, string codigoMuestra, int idNeonato)
        {
            /*
            List<Vista_BuscarPaciente> listaRechazadas = db.dc.Vista_BuscarPacientes.Where(p => p.RecibioMuestra == false && p.Estado == 1 && p.MuestraAceptada == false && p.DNI == dni ).ToList();
            Vista_BuscarPaciente paciente = listaRechazadas.Where(p => p.ApellidosNeonato == apellidos && p.FechaNacimiento == fechaNacimiento).FirstOrDefault();

            if (paciente != null)
            {
                Muestra muestra = db.dc.Muestras.Where(m => m.idMuestra == paciente.idMuestra).First();
                muestra.RecibioMuestra = true;
                muestra.CodRenaes = "2da Muestra Recibida(Rechazada)_" + usuario + "_" + DateTime.Today.ToShortDateString();
                muestra.AfiliacionSIS = codigoMuestra;
                return ActualizarMuestra(muestra);
            }
            return false;
             */
            Vista_BuscarPaciente paciente = db.dc.Vista_BuscarPacientes.Where(p => p.idNeonato == idNeonato && p.MuestraAceptada == false && p.RecibioMuestra == false && p.Estado == 1).FirstOrDefault();

            if (paciente != null)
            {
                Muestra muestra = db.dc.Muestras.Where(m => m.idMuestra == paciente.idMuestra).First();
                muestra.RecibioMuestra = true;
                muestra.CodRenaes = "1ra Rechazada_" + usuario + "_" + DateTime.Today.ToShortDateString();
                muestra.AfiliacionSIS = codigoMuestra;
                return ActualizarMuestra(muestra);
            }
            return false;
        }

        //public bool MarcarMuestraRecibidaAlteradas(string usuario,string dni, string nombres, string apellidos, DateTime fechaNacimiento,string codigoMuestra)
        public bool MarcarMuestraRecibidaAlteradas(string usuario, string codigoMuestra, int idNeonato)
        {
            /*
            List<Vista_ResultadosEstablecimiento> listaAlterados = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.RecibioMuestra == false && r.Publicado == true && r.rdcDeterminationLevel > 20 && r.DNI == dni).ToList();
            Vista_ResultadosEstablecimiento paciente = listaAlterados.Where(r => r.ApellidosNeonato == apellidos && r.FechaNacimiento == fechaNacimiento).FirstOrDefault();
            
            if (paciente != null)
            {
                
                Muestra muestra = db.dc.Muestras.Where(m => m.idMuestra == paciente.idMuestra).First();
                muestra.RecibioMuestra = true;
                muestra.CodRenaes = "2da Muestra Recibida(Alterada)" + usuario + "_" + DateTime.Today.ToShortDateString();
                muestra.AfiliacionSIS = codigoMuestra;
                return ActualizarMuestra(muestra);
            }
            return false;
             */
            //List<Vista_ResultadosEstablecimiento> listaAlterados = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.RecibioMuestra == false && r.Publicado == true && r.rdcDeterminationLevel > 20 && r.DNI == dni).ToList();
            Vista_ResultadosEstablecimiento paciente = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.idNeonato == idNeonato && r.RecibioMuestra == false && r.rdcDeterminationLevel > 20).FirstOrDefault();

            if (paciente != null)
            {

                Muestra muestra = db.dc.Muestras.Where(m => m.idMuestra == paciente.idMuestra).First();
                muestra.RecibioMuestra = true;
                muestra.CodRenaes = "1era Alterada_" + usuario + "_" + DateTime.Today.ToShortDateString();
                muestra.AfiliacionSIS = codigoMuestra;
                return ActualizarMuestra(muestra);
            }
            return false;
        }

        #endregion
        #region Digitacion
        /*
         * 
         */
        public List<Vista_Muestra> ObtenerMuestrasDigitadasPendientesEnvio(int idEstablecimiento)
        {
            return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false && m.Estado == 1 && m.CodigoInternoLab == null).OrderBy(m => m.FechaToma).ToList();
        }

        public List<Vista_Muestra> ObtenerMuestrasDigitadasEdicion(int idEstablecimiento)
        {
            return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false && m.Estado == 1 && m.CodigoInternoLab == null).ToList();
        }

        public List<Vista_BuscarPaciente> ObtenerMuestrasDigitadas(int idEstablecimiento, string apellidosNeonato, string apellidosMadre, bool usarFechaDigitadoInicial, bool usarFechaDigitadoFinal, DateTime fechaDigitadoInicial, DateTime fechaDigitadoFinal, string codigoMuestra, string DNI)
        {
            List<Vista_BuscarPaciente> listaMuestras;

            if (idEstablecimiento > 0)
            {
                listaMuestras = db.dc.Vista_BuscarPacientes.Where(m => m.idEstablecimiento == idEstablecimiento && m.Estado == 1).ToList();
            }
            else
            {
                listaMuestras = db.dc.Vista_BuscarPacientes.Where(m => m.Estado == 1).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
            {
                return listaMuestras.Where(m => m.CodigoMuestra == codigoMuestra).ToList();
            }
            if (DNI.CompareTo(string.Empty) != 0)
            {
                return listaMuestras.Where(m => DNI.Contains(m.DNI)).ToList();
            }
            if (apellidosNeonato.CompareTo(string.Empty) != 0)
            {
                return listaMuestras.Where(m => m.ApellidosNeonato.Contains(apellidosNeonato)).ToList();
            }
            if (apellidosMadre.CompareTo(string.Empty) != 0)
            {
                return listaMuestras.Where(m => m.ApellidosMadre.Contains(apellidosMadre)).ToList();
            }

            if (usarFechaDigitadoInicial && usarFechaDigitadoFinal)
            {
                return listaMuestras.Where(m => m.FechaCreacion >= fechaDigitadoInicial && m.FechaCreacion < fechaDigitadoFinal).ToList();
            }
            else
            {
                if (usarFechaDigitadoInicial)
                {
                    return listaMuestras.Where(m => m.FechaCreacion >= fechaDigitadoInicial).ToList();
                }
                if (usarFechaDigitadoFinal)
                {
                    return listaMuestras.Where(m => m.FechaCreacion < fechaDigitadoFinal).ToList();
                }
            }

            return listaMuestras.OrderByDescending(m => m.idMuestra).ToList();
        }
        #endregion
        #region Envio
        public List<Vista_Muestra> ObtenerVistaMuestrasEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return db.dc.Vista_Muestras.Where(m => m.idEnvio == idEnvio && (m.EstadoEdicion == 0 || m.EstadoEdicion == 2)).ToList();
        }

        public List<Muestra> ObtenerMuestrasEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return db.dc.Muestras.Where(m => m.idEnvio == idEnvio).ToList();
        }
        public List<Muestra> ObtenerMuestrasCancelarEdicionEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return db.dc.Muestras.Where(m => m.idEnvioAnterior == idEnvio).ToList();
        }

        /*
         *  Descripcion: Retorna las muestras eliminadas del envio durante el proceso de edición
         */
        public List<Muestra> ObtenerMuestrasEliminadasEnvio(int idEnvio)
        {
            return db.dc.Muestras.Where(m => m.idEnvio == idEnvio && m.EstadoEdicion == 1).ToList();
        }

        /*
         *  Descripcion: Retorna las muestras agregadas a un envio durante el proceso de edición
         */
        public List<Muestra> ObtenerMuestrasAgregadasEnvio(int idEnvio)
        {
            return db.dc.Muestras.Where(m => m.idEnvio == idEnvio && m.EstadoEdicion == 2).ToList();
        }
        #endregion

        #region ResultadosVerificacion

        public List<Vista_Muestra> ObtenerMuestrasVerificacion(int inicial, int final)
        {
            return db.dc.Vista_Muestras.Where(m => m.CodigoCorrelativoInt >= inicial && m.CodigoCorrelativoInt <= final).OrderBy(m => m.CodigoCorrelativoInt).ToList();
        }

        #endregion
    }
}
