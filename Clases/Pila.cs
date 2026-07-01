using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Pila
    {
        public Vertice cima = null;
        
        public void Apilar(string nombreLugar)
        {
            Vertice nuevo = new Vertice();
            nuevo.dato2 = nombreLugar;

            nuevo.sig = cima;
            cima = nuevo;

        }
        public string Desapilar()
        {
            string dato = null;
            if (cima != null)
            {
                dato = cima.dato2;
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
