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
            Console.WriteLine("-----------------------------------------------------");
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

        //metodo de dijkstra para encontrar la ruta más corta
        public void CalcularRutaOptima(Vertice inicio, ref float total, ref string ruta)
        {
            // 1) Reiniciamos los datos de Dijkstra de cada vértice, recorriendo la lista enlazada
            Vertice temp = l_vertices.primero;
            while (temp != null)
            {
                temp.distancia = float.MaxValue;
                temp.visitado = false;
                temp.anterior = null;
                temp = temp.sig;
            }

            inicio.distancia = 0;

            // 2) Bucle principal de Dijkstra: se repite "cantidad" de veces
            for (int c = 0; c < cantidad; c++)
            {
                // Buscamos, recorriendo la lista enlazada, el vértice NO visitado con menor distancia
                // (esto reemplaza a la cola de prioridad de la versión clásica)
                Vertice u = null;
                float menor = float.MaxValue;
                Vertice recorrido = l_vertices.primero;
                while (recorrido != null)
                {
                    if (!recorrido.visitado && recorrido.distancia < menor)
                    {
                        menor = recorrido.distancia;
                        u = recorrido;
                    }
                    recorrido = recorrido.sig;
                }

                if (u == null) break; // ya no quedan nodos alcanzables
                u.visitado = true;

                // 3) Relajamos las aristas que salen de u
                Arista a = u.ls.primero;
                while (a != null)
                {
                    float nuevaDistancia = u.distancia + a.peso;
                    if (nuevaDistancia < a.destino.distancia)
                    {
                        a.destino.distancia = nuevaDistancia;
                        a.destino.anterior = u;
                    }
                    a = a.sig;
                }
            }

            // 4) La meta es el último vértice de la lista enlazada ("La Cumbre")
            Vertice destino = l_vertices.primero;
            while (destino.sig != null)
            {
                destino = destino.sig;
            }
            total = destino.distancia;

            // 5) Reconstruimos la ruta usando nuestra Pila propia
            //    (anterior nos da el camino al revés: de la meta al inicio)
            Pila pila = new Pila();
            Vertice actual = destino;
            while (actual != null)
            {
                pila.Apilar(actual.dato.nombre);
                actual = actual.anterior;
            }

            ruta = "";
            while (!pila.EstaVacia())
            {
                ruta += " -> " + pila.Desapilar();
            }
        }
    }
}
