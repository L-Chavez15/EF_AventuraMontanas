using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Clases
{
    public class Grafo
    {
        ListaSimple l_vertices = new ListaSimple();
        int[,] ma;//matriz de adyacencia
        int cantidad;//cantidad de vertices

        string[] nom_puntos = {"Faldas de la Montaña", "Bosque Susurrante", "Ciudad Olvidada", "Puente Colgante",
                                 "Resort Celestial", "Cueva de Cristal", "Templo del Espejo", "Glaciar Eterno",
                                 "Refugio del Águila", "La Cumbre"  };
        string[] climas = { "Tranquilo", "Nublado", "Viento", "Neblina", "Fantasmal", "Húmedo", "Oscuridad", "Helado", "Ventisca", "Helado" };

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

        public void GenerarMatriz()
        {
            Random r = new Random();
            for (int i = 0; i < cantidad; i++)
            {
                for (int j = 0; j < cantidad; j++)
                {
                    ma[i, j] = r.Next(1, 10);
                }
            }
        }
        public void CrearGarfo()
        {
            Random r = new Random();
            Vertice temp_i = l_vertices.primero;
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                Vertice temp_j = l_vertices.primero;
                for (int j = 0; j < ma.GetLength(1); j++)
                {
                    if (ma[i, j] == 1)
                    {
                        temp_i.ls.Insertar(temp_j, r.Next(10, 50));
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }
        public void RecorrerGrafo(Vertice v, ref float total, ref string ruta)
        {
            ruta += " -> " + v.dato.nombre;

            Console.Clear();
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" * UBICACIÓN ACTUAL: " + v.dato.nombre);
            Console.ResetColor();
            Console.WriteLine("   Estamina gastada: " + total + " Pts.");
            Console.WriteLine("==================================================\n");

            if (v.ls.primero == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ¡HAS LLEGADO A LA CUMBRE!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Saltos disponibles:");
            v.ls.Mostrar();
            Console.WriteLine("--------------------------------");
            Console.Write("* Ingresa el número del camino que deseas tomar: ");

            int op = int.Parse(Console.ReadLine());
            if (op == 0)
            {
                return;
            }

            Arista temp = v.ls.primero;
            for (int i = 1; i < op; i++)
            {
                if (temp != null)
                {
                    temp = temp.sig;
                }
            }

            if (temp != null)
            {
                total = total + temp.peso;
                RecorrerGrafo(temp.destino, ref total, ref ruta);
            }
        }

        //Metodo de Dijkstra para encontrar la ruta más corta
        //Creamos una busqueda de un indice de un vertice en la lista de vertices

    }
}
