using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class ListaSimple
    {
        public Vertice primero = null;
        public void Insertar(Lugar d)
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
    }
}
