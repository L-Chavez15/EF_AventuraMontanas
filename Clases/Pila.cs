using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pila
    {
        //Usamos pila para organiar el orden al momento de desapilar
        //Disjktra crea la ruta yendo desde el destion al origen
        //Por lo que al momento de mostrar la ruta, se debe desapilar para mostrarla en el orden correcto

        public Nodo cima = null;
        
        public void Apilar(string nombreLugar)
        {
            Nodo nuevo = new Nodo();
            nuevo.dato = nombreLugar;

            nuevo.sig = cima;
            cima = nuevo;

        }
        public string Desapilar()
        {
            string dato = null;
            if (cima != null)
            {
                dato = cima.dato;
                cima = cima.sig;
            }
            return dato;
        }
        public bool EstaVacia()
        {
            return cima == null;
        }
        
    }
}
