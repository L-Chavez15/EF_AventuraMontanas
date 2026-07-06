using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class ListaSimple
    {
        //Usamos lista simple para saber cuantos nodos hay en el grafo y poder recorrerlos
        public Vertice primero = null;
        public void Insertar(Lugar d)//crear un nuevo vertice y lo agrega al final
        {
            Vertice nuevo = new Vertice();
            nuevo.dato = d;
            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Vertice temp = primero;
                while (temp.sig != null)
                {
                    temp = temp.sig;
                }
                temp.sig = nuevo;
            }
        }
        public void Mostrar()
        {
            Vertice temp = primero;
            while (temp != null)
            {
                Console.WriteLine(temp.dato);
                temp = temp.sig;
            }
        }
    }
}
