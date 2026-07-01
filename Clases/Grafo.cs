using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Grafo
    {
        ListaSimple l_vertices= new ListaSimple();
        int[,] ma;//matriz de adyacencia
        int cantidad;//cantidad de vertices

        string[] nom_puntos = {"Faldas de la Montaña", "Bosque Susurrante", "Ciudad Olvidada", "Puente Colgante",
                                 "Resort Celestial", "Cueva de Cristal", "Templo del Espejo", "Glaciar Eterno",
                                 "Refugio del Águila", "La Cumbre"  };
        string [] climas = { "Tranquilo", "Nublado", "Viento", "Neblina", "Fantasmal", "Húmedo", "Oscuridad", "Helado", "Ventisca", "Helado" };

        public Grafo(int cant)
        {
            cantidad = cant;
            Random r = new Random();
            for (int i = 0; i < cantidad; i++)
            {
                Lugar l = new Lugar();
                l.nombre = nom_puntos[i];
                l.clima = climas[i];
                l.temperatura = r.Next(-10, 15);

                l_vertices.Insertar(l);
            }
            ma = new int[cant, cant];
        }
        public Vertice GetInicio()
        {
            return l_vertices.primero;
        }

    }
}
