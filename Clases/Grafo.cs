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

            cantidad = cant;// si elminamos esto, el bucle de dijkstra no se ejecutará correctamente
            //si elminamos cantidad dijkstra no sabrá cuántos vértices hay en el grafo y no podrá recorrerlos correctamente
            Random r = new Random();
            for (int i = 0; i < cantidad; i++)
            {
                Lugar l = new Lugar();//crea un nuevo lugar por cada nombre
                l.nombre = nom_puntos[i];
                l.clima = climas[i];
                l.temperatura = r.Next(-10, 15);

                l_vertices.Insertar(l);
            }
            ma = new int[cant, cant];//prepara la matriz
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
                    if (j == i + 1)//conexion con el siguiente nodo
                                   // garantiza que siempre se pueda llegar a la meta
                                   //si eliminamos esto, el grafo podría no tener un camino hacia la meta
                    {
                        ma[i, j] = 1; // salto obligado al siguiente: garantiza que siempre se pueda llegar a la meta
                    }
                    else if (j > i + 1)
                    {
                        // ~40% de probabilidad de atajo extra
                        int numero = r.Next(0, 10);
                        if (numero < 4)
                        {
                            ma[i, j] = 1;
                        }
                        else
                        {
                            ma[i, j] = 0;
                        }
                    }
                    else
                    {
                        ma[i, j] = 0; //  evita ciclos infinitos
                                      // para ir siempre hasta la cumbre
                                      //pero revisar la posibilidad de regresar a un nodo anteriora

                    }
                }
            }
        }
        public void CrearGarfo()
        {
            //si eliminamos esto, el grafo no tendrá aristas reales y no se podrá recorrer
            Random r = new Random();
            Vertice temp_i = l_vertices.primero;
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                Vertice temp_j = l_vertices.primero;
                for (int j = 0; j < ma.GetLength(1); j++)
                {
                    if (ma[i, j] == 1)
                    {
                        //traduce la matriz de adyacencia en aristas, con pesos aleatorios entre 10 y 50
                        temp_i.ls.Insertar(temp_j, r.Next(10, 50));
                        //si borramos esto, ningún vertice tendría aristas
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }
        public void RecorrerGrafo(Vertice v, ref float total, ref string ruta)
        {
            ruta += " -> " + v.dato.nombre+"\n";

            Console.Clear();
            Console.WriteLine("======================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" * UBICACIÓN ACTUAL: " + v.dato.nombre);
            Console.ResetColor();
            Console.WriteLine("   Estamina gastada: " + Math.Round(total, 2) + " Pts.");
            Console.WriteLine("======================================================\n");

            if (v.ls.primero == null)//significa que llego a la cumbre, ya que no hay más aristas que recorrer
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ¡HAS LLEGADO A LA CUMBRE!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Saltos disponibles:");
            v.ls.Mostrar();
            Console.WriteLine("------------------------------------------------------");
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
            //1. Preparación del terreno (Inicialización)
            Vertice temp = l_vertices.primero;
            while (temp != null)//recorremos los vertices
            {
                temp.distancia = float.MaxValue;//max.value = infinito (3.4x10^38)
                temp.visitado = false;
                temp.anterior = null;
                temp = temp.sig;
            }
            inicio.distancia = 0;

            //2. Buscar el paso más corto
            //ver si se puede usar COLA
            for (int i = 0; i < cantidad; i++)//se repite tantas veces como vértices haya
            {
                Vertice u = null;//buscamos el vértice no visitado con la menor distancia
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

                //3.Evaluar los caminos posibles
                Arista a = u.ls.primero;
                while (a != null)
                {
                    float nuevaDistancia = u.distancia + a.peso;//calculamos la distancia desde el inicio hasta el destino a través de u
                    if (nuevaDistancia < a.destino.distancia)//atajo
                    {
                        a.destino.distancia = nuevaDistancia;
                        a.destino.anterior = u;//guardamos el vértice anterior para reconstruir la ruta más tarde
                    }
                    a = a.sig;
                }
            }
            //4.Identificar la Meta
            Vertice destino = l_vertices.primero;
            while (destino.sig != null)
            {
                destino = destino.sig;
            }
            total = destino.distancia;

            // 5) Reconstruimos la ruta 
            Pila pila = new Pila();
            Vertice actual = destino;
            while (actual != null)
            {
                pila.Apilar(actual.dato.nombre);//metemos el camino al revés de meta a inicio
                actual = actual.anterior;
            }

            ruta = "";
            while (!pila.EstaVacia())
            {
                ruta += " -> " + pila.Desapilar();//salen los datos ordenados de inicio a meta
            }
        }
    }
}
