namespace Clases
{
    public class Vertice
    {
        public Lugar dato;
        public string dato2;//pila
        public Vertice sig = null;

        public ListaAristas ls = new ListaAristas();

        //Algoritmo de disjktra
        public float distancia;
        public bool visitado;
        public Vertice anterior;
    }
}