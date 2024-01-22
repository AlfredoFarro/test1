
namespace BE
{
    public class MuestraCompletaBE
    {
        public MuestraCompletaBE()
        {
            Muestra = new Muestra();
            Neonato = new Neonato();
            Madre = new Madre();
            Madre.Nombres = string.Empty;
            Madre.Apellidos = string.Empty;
            Madre.DNI = string.Empty;

        }
        public Muestra Muestra { get; set; }
        public Neonato Neonato { get; set; }
        public Madre Madre { get; set; }
    }
}
