namespace Clases
{
    public class Lugar
    {
        //punto de control
        public string nombre;
        public string clima;
        public string temperatura;

        public override string ToString()
        {
            return $"{nombre} - CLIMA: {clima}, TEMPERATURA: {temperatura}";
        }
    }
}