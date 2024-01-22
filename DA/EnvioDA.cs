using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class EnvioDA
    {
        readonly AccesoDB _db = new AccesoDB();

        public Envio InsertarEnvio(Envio envio)
        {
            _db.dc.Envios.InsertOnSubmit(envio);
            try
            {
                _db.dc.SubmitChanges();
                return envio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Envio ActualizarEnvio(Envio envio)
        {
            try
            {
                _db.dc.SubmitChanges();
                return envio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Vista_Envio> ObtenerEnvios(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin)
        {
            List<Vista_Envio> listaEnvios = new List<Vista_Envio>();

            if (idEstablecimiento > 0)
            {
                if (usarInicio && usarFin)
                {
                    listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.FechaCreacion > dateInicio && e.FechaCreacion <= dateFin && e.Estado == 1).ToList();
                }
                else
                {
                    if (!usarInicio && usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.FechaCreacion <= dateFin && e.Estado == 1).ToList();
                    }
                    if (usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.FechaCreacion > dateInicio && e.Estado == 1).ToList();
                    }
                    if (!usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1).ToList();
                    }
                }
            }
            else
            {
                if (usarInicio && usarFin)
                {
                    listaEnvios = _db.dc.Vista_Envios.Where(e => e.FechaCreacion > dateInicio && e.FechaCreacion <= dateFin && e.Estado == 1).ToList();
                }
                else
                {
                    if (!usarInicio && usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.FechaCreacion <= dateFin && e.Estado == 1).ToList();
                    }
                    if (usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.FechaCreacion > dateInicio && e.Estado == 1).ToList();
                    }
                    if (!usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1).ToList();
                    }
                }
            }

            return listaEnvios.OrderByDescending(e=> e.FechaCreacion).ToList();
        }

        public List<Vista_Envio> ObtenerEnvios(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, bool estadoEnvio)
        {
            List<Vista_Envio> listaEnvios = new List<Vista_Envio>();

            if (idEstablecimiento > 0)
            {
                //listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1 && e.EnvioRecibido == estadoEnvio).ToList();
                if (usarInicio && usarFin)
                {
                    listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion > dateInicio && e.FechaCreacion <= dateFin).ToList();
                }
                else
                {
                    if (!usarInicio && usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion <= dateFin).ToList();
                    }
                    if (usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion > dateInicio).ToList();
                    }
                    if (!usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.idEstablecimiento == idEstablecimiento && e.Estado == 1 && e.EnvioRecibido == estadoEnvio).ToList();
                    }
                }
            }
            else
            {
                //listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1 && e.EnvioRecibido == estadoEnvio).ToList();
                if (usarInicio && usarFin)
                {
                    listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion > dateInicio && e.FechaCreacion <= dateFin).ToList();
                }
                else
                {
                    if (!usarInicio && usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion <= dateFin).ToList();
                    }
                    if (usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1 && e.EnvioRecibido == estadoEnvio && e.FechaCreacion > dateInicio).ToList();
                    }
                    if (!usarInicio && !usarFin)
                    {
                        listaEnvios = _db.dc.Vista_Envios.Where(e => e.Estado == 1 && e.EnvioRecibido == estadoEnvio).ToList();
                    }
                }
            }

            
            return listaEnvios.OrderByDescending(e => e.FechaCreacion).ToList();
        }

        public bool MarcarMuestrasEnviadas(Envio envio, List<string> listaCodigosMuestras, int estadoEdicion)
        {
            //ListaidMuestras;
            try
            {
                var lista = _db.dc.Muestras.Where(m => m.idEstablecimiento == envio.idEstablecimiento && m.MuestraEnviada == false && listaCodigosMuestras.Contains(m.CodigoMuestra)).ToList();
                lista.ForEach(m =>
                {
                    m.MuestraEnviada = true;
                    m.EstadoEdicion = estadoEdicion;
                    m.idEnvio = envio.idEnvio;
                });

                _db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }

        }

        public bool MarcarMuestrasEnviadas(int idEstablecimiento, int idEnvio, List<int> listaIdMuestras)
        {
            //ListaidMuestras;
            try
            {
                var lista = _db.dc.Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false && listaIdMuestras.Contains(m.idMuestra)).ToList();
                lista.ForEach(m =>
                {
                    m.MuestraEnviada = true;
                    m.idEnvio = idEnvio;
                });

                _db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }

        }

        public List<Vista_MuestrasxEnvio> ObtenerListaMuestrasxEnvio(int idEnvio)
        {
            return _db.dc.Vista_MuestrasxEnvios.Where(m => m.idEnvio == idEnvio).OrderBy(m => m.FechaToma).ToList();
        }

        //public bool EliminarEnvio(int idEnvio, string usuario)
        //{
        //    try
        //    {
        //        Envio envio = db.dc.Envios.Where(e => e.idEnvio == idEnvio).First();
        //        envio.Estado = 0;
        //        envio.ModificadoPor = usuario;
        //        envio.FechaModificacion = DateTime.Now;
        //        db.dc.SubmitChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }

        //}

        public Envio ObtenerEnvio(int idEnvio)
        {
            return _db.dc.Envios.Where(e => e.idEnvio == idEnvio && e.Estado == 1).First();
        }
        /*
            var ls=new int[]{2,3,4};
            var name="Foo";
            using (var db=new SomeDatabaseContext())
            {
                var some= db.SomeTable.Where(x=>ls.Contains(x.friendid)).ToList();
                some.ForEach(a=>
                                {
                                    a.status=true;
                                    a.name=name;
                                }
                            );
                db.SubmitChanges();
            }
         */
        //public bool MarcarEnvioRecibido(string usuario, List<string> listaIdEnvios)
        //{
        //    try
        //    {
        //        var lista = _db.dc.Envios.Where(m => m.EnvioRecibido == false && listaIdEnvios.Contains(m.idEnvio.ToString())).ToList();
        //        lista.ForEach(m =>
        //        {
        //            m.ModificadoPor = usuario;
        //            m.RecibioMuestra = true;
        //            m.FechaModificacion = DateTime.Today;
        //            //m.FechaTransfucion = DateTime.Today;
        //            m.AfiliacionSIS = "Manualmente Marcado_" + usuario + "_" + DateTime.Today.ToShortDateString();
        //        });

        //        db.dc.SubmitChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw;
        //        return false;
        //    }
        //}
    }
}
