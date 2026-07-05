namespace Clases
{
    public class Lugar
    {
        //punto de control
        public string nombre;
        public string clima;
        public int temperatura;
        public string descripcion;

        public override string ToString()
        {
            return $"{nombre} - CLIMA: {clima}, TEMPERATURA: {temperatura}";
        }
    }
}