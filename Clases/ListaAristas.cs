using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class ListaAristas
    {
        public Arista primero = null;

        public void Insertar(Vertice v, float p)
        {
            Arista nuevo = new Arista();
            nuevo.destino = v;
            nuevo.peso = p;

            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Arista temp= primero;
                while (temp.sig != null)
                {
                    temp = temp.sig;
                }
                temp.sig = nuevo;
            }
        }
    }
}
