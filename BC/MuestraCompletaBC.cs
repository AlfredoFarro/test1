using BE;

namespace BC
{
    public class MuestraCompletaBC
    {
        readonly MuestraBC muestraBC = new MuestraBC();
        readonly NeonatoBC neonatoBC = new NeonatoBC(); 
        readonly MadreBC madreBC = new MadreBC();

        public void RegistrarMuestra(MuestraCompletaBE muestraCompleta)
        {   
            //Madre madre = new Madre();
            muestraCompleta.Madre = madreBC.InsertarMadre(muestraCompleta.Madre);
            muestraCompleta.Neonato.idMadre = muestraCompleta.Madre.idMadre;
            muestraCompleta.Neonato = neonatoBC.InsertarNeonato(muestraCompleta.Neonato);
            muestraCompleta.Muestra.idNeonato = muestraCompleta.Neonato.idNeonato;
            muestraCompleta.Muestra = muestraBC.InsertarMuestra(muestraCompleta.Muestra);
            muestraCompleta.Neonato.idMuestra = muestraCompleta.Muestra.idMuestra;
            muestraCompleta.Neonato = neonatoBC.ActualizarNeonato(muestraCompleta.Neonato);
            
        }
        public void ActualizarMuestra(MuestraCompletaBE muestraCompleta)
        {
            madreBC.ActualizarMadre(muestraCompleta.Madre);
            neonatoBC.ActualizarNeonato(muestraCompleta.Neonato);
            muestraBC.ActualizarMuestra(muestraCompleta.Muestra);
        }

        public bool ExisteMuestra(string codigoMuestra)
        {
            return muestraBC.ExisteMuestra(codigoMuestra);
        }

        public MuestraCompletaBE ObtenerMuestra(string codigoMuestra)
        {
            var muestraCompleta = new MuestraCompletaBE();
            muestraCompleta.Muestra = muestraBC.ObtenerMuestraxCodigoMuestra(codigoMuestra);
            muestraCompleta.Neonato = neonatoBC.ObtenerNeonatoxIdNeonato(muestraCompleta.Muestra.idNeonato);
            muestraCompleta.Madre = madreBC.ObtenerMadrexIdMadre(muestraCompleta.Neonato.idMadre);
            return muestraCompleta;
        }

        public MuestraCompletaBE ObtenerMuestra(int idMuestra)
        {
            var muestraCompleta = new MuestraCompletaBE();
            muestraCompleta.Muestra = muestraBC.ObtenerMuestraxIdMuestra(idMuestra);
            muestraCompleta.Neonato = neonatoBC.ObtenerNeonatoxIdNeonato(muestraCompleta.Muestra.idNeonato);
            muestraCompleta.Madre = madreBC.ObtenerMadrexIdMadre(muestraCompleta.Neonato.idMadre);
            return muestraCompleta;
        }
        
        public void CambiarEstadoMadre(int idMadre, int estado)
        {
            Madre madre = madreBC.ObtenerMadrexIdMadre(idMadre);
            madre.Estado = estado;
            madreBC.ActualizarMadre(madre);
        }
        public void CambiarEstadoNeonato(int idNeonato, int estado)
        {
            Neonato neonato = neonatoBC.ObtenerNeonatoxIdNeonato(idNeonato);
            neonato.Estado = estado;
            neonatoBC.ActualizarNeonato(neonato);
        }

    }
}
