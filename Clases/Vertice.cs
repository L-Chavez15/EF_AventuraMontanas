namespace Clases
{
    public class Vertice
    {
        public Lugar dato;//Guardamos informacion del lugar

        public Vertice sig = null;//Siguiente vertice en la lista de vertices

        public ListaAristas ls = new ListaAristas();//Lista de aristas que salen de este vertice

        //Algoritmo de disjktra
        public float distancia;
        public bool visitado;
        public Vertice anterior;
    }
}