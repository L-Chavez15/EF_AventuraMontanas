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
        ListaSimple l_vertices = new ListaSimple();//guarda los vertices del grafo, no como se conectan
        int[,] ma;//matriz de adyacencia
        int cantidad;//guarda la cantidad de vertices

        string[] nom_puntos = {"Faldas de la Montaña", "Bosque Susurrante", "Ciudad Olvidada", "Puente Colgante",
                                 "Resort Celestial", "Cueva de Cristal", "Templo del Espejo", "Glaciar Eterno",
                                 "Refugio del Águila", "La Cumbre"  };
        string[] climas = { "Tranquilo", "Nublado", "Viento", "Neblina", "Fantasmal", "Húmedo", "Oscuridad", "Helado", "Ventisca", "Helado" };

        string[] descripciones = {
            "El sendero comienza suave, entre pastizales y piedras sueltas. Todavía se puede oler el pueblo a lo lejos.",
            "Los árboles se cierran sobre tu cabeza. El viento se filtra entre las ramas como si murmurara algo que no alcanzas a entender.",
            "Ruinas cubiertas de musgo asoman entre la niebla. Alguna vez alguien vivió aquí, y ahora solo quedan las piedras.",
            "Un puente de cuerdas cruje bajo tus pies, colgado sobre un abismo que se pierde en la bruma.",
            "Un antiguo refugio, alguna vez lujoso, ahora ofrece poco más que paredes rotas y algo de calor para descansar.",
            "Las paredes de la cueva brillan con cristales que devuelven tu propia luz, multiplicada en mil reflejos.",
            "Un templo silencioso. Los espejos en sus muros parecen seguir cada uno de tus pasos.",
            "El hielo cruje bajo tus botas. El frío ya no se siente en la piel, sino en los huesos.",
            "Un nido de piedra en lo alto, donde las águilas vigilan el valle entero como centinelas eternos.",
            "La cumbre. El aire es escaso, pero la vista que se abre ante ti hace que cada paso haya valido la pena."
        };
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
                l.descripcion = descripciones[i];

                l_vertices.Insertar(l);
            }
            ma = new int[cant, cant];//prepara la matriz
        }
        public Vertice GetInicio()
        {
            return l_vertices.primero;//devuelve el primer vértice de la lista, que es el inicio del recorrido
        }

        public void GenerarMatriz()
        {
            Random r = new Random();
            for (int i = 0; i < cantidad; i++)//i= fila
            {
                for (int j = 0; j < cantidad; j++)//j=columna
                {
                    if (j == i + 1)//conexion con el siguiente nodo
                                   // garantiza que siempre se pueda llegar a la meta
                                   //si eliminamos esto, el grafo podría no tener un camino hacia la meta
                    {
                        ma[i, j] = 1; // salto obligado al siguiente: garantiza que siempre se pueda llegar a la meta
                    }
                    else if (j > i + 1)//verifica si hay un posible atajo
                    {
                        // ~40% de probabilidad de atajo extra
                        int numero = r.Next(0, 10);
                        if (numero < 4)
                        {
                            ma[i, j] = 1;// si esto se cumple, crea un atajo. Si no se cumple, no hay conexión
                                         //depende del random, si no se cumple, no hay atajo 

                        }
                        else
                        {
                            ma[i, j] = 0;//impide quedarse en el mismo sitio o retrocede
                            //el grafo solo tiene aristas hacia adelante, no hacia atrás, para evitar ciclos infinitos
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
                    if (ma[i, j] == 1)//consulta la matriz de adyacencia para ver si hay una conexión 
                    {
                        //traduce la matriz de adyacencia en aristas, con pesos aleatorios entre 10 y 50
                        temp_i.ls.Insertar(temp_j, r.Next(10, 50));// genera la conexión entre los vértices, con un peso aleatorio
                        //si borramos esto, ningún vertice tendría aristas
                    }
                    temp_j = temp_j.sig;
                }
                temp_i = temp_i.sig;
            }
        }
        public void RecorrerGrafo(Vertice v, ref float total, ref string ruta)
        {
            ruta += " -> " + v.dato.nombre+"\n";//Cada vez que el jugador llega a un lugar, se agrega su nombre a la ruta.

            //mostramos la información del lugar actual
            Console.Clear();
            Console.WriteLine("======================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" * UBICACIÓN ACTUAL: " + v.dato.nombre);
            Console.ResetColor();
            Console.WriteLine("   Estamina gastada: " + Math.Round(total, 2) + " Pts.");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("   Clima: " + v.dato.clima + "   |   Temperatura: " + v.dato.temperatura + "°C");
            Console.ResetColor();
            Console.WriteLine("   " + v.dato.descripcion);
            Console.WriteLine("======================================================\n");

            if (v.ls.primero == null)//significa que llego a la cumbre, ya que no hay más aristas que recorrer
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ¡HAS LLEGADO A LA CUMBRE!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Saltos disponibles:");
            v.ls.Mostrar();//muestra los caminos disponibles desde el lugar actual
            Console.WriteLine("------------------------------------------------------");

            int op = -1;//no es obligatorio, porque luego canbiara op
            //significa que aún no existe una seleccion
            // si no le colocamos un valor a op, el compilador se queja de que no se inicializó, aunque luego se le asigne un valor
            bool entradaValida = false;// no sabemos lo que va a escribir rl usuario
            while (!entradaValida)
            {
                Console.Write("Ingresa el número del camino que deseas tomar (0 para detenerte): ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out op))// tryparse intenta convertir un texto a un número entero, si no puede, devuelve false
                {
                    int cantidadOpciones = 0;//no sabemos la cantidad de caminos disponibles, así que la inicializamos en 0
                    Arista contador = v.ls.primero;
                    while (contador != null)//mientas exista una arista, sigue recorriendo la lista
                    {
                        //nos muestra la cantidad de caminos disponibles desde el lugar actual
                        cantidadOpciones++;
                        contador = contador.sig;
                    }

                    if (op == 0 || (op >= 1 && op <= cantidadOpciones))
                    {
                        entradaValida = true;//verifica si la opción ingresada es válida
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("   Ese camino no existe. Intenta de nuevo.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("   Ingresa solo un número.");
                    Console.ResetColor();
                }
            }
            if (op == 0)
            {
                return;
            }
            Arista temp = v.ls.primero;
            for (int i = 1; i < op; i++)//i empieza en 1 porque la primera arista es la opción 1, ya estamos parados en la primera opción.
            {
                if (temp != null)
                {
                    temp = temp.sig;
                    //cada que avancemos veremos los caminos disponibles
                }
            }

            if (temp != null)//si aun existe una arista, significa que el jugador eligió un camino válido
            {
                total = total + temp.peso;//va sumando el peso de cada arista que recorre
                RecorrerGrafo(temp.destino, ref total, ref ruta);//cada llamada "avanza un paso" en el grafo, hasta llegar a la cima
            }
        }

        //metodo de dijkstra para encontrar la ruta más corta
        public void CalcularRutaOptima(Vertice inicio, ref float total, ref string ruta)
        {
            //1. Preparación del terreno (Inicialización)
            Vertice temp = l_vertices.primero;//puntero temporal para recorrer la lista de vértices
            while (temp != null)//recorremos los vertices mientras exitan 
            {
                temp.distancia = float.MaxValue;//max.value = infinito (3.4x10^38) || al inicio no conocemos su valor, asi que es infinito
                temp.visitado = false;
                temp.anterior = null;//guarda desde qué vértice llegamos
                temp = temp.sig;
            }
            inicio.distancia = 0;//costo de estar en el vertice de inicio es 0, porque no hemos recorrido nada aún

            //2. Buscar el paso más corto
            //ver si se puede usar COLA
            for (int i = 0; i < cantidad; i++)//se repite tantas veces como vértices haya (BUSCA EL VERTICE NO VISITADO CON MENOR COSTO)
            {
                Vertice u = null;//buscamos el vértice no visitado con la menor distancia
                float menor = float.MaxValue;
                Vertice recorrido = l_vertices.primero;// CREAMOS UN PUNTERO, BUSCA EL MEJOR CANDIDATO
                while (recorrido != null)
                {
                    if (!recorrido.visitado && recorrido.distancia < menor)//recorrido.visitado=¿Faldas ya fue visitado?
                                                                           //recorrido.distancia < menor = 0<infinito      
                    {
                        menor = recorrido.distancia;//MENOR = 0
                        u = recorrido;//u = Faldas de la Montaña || HASTA EL MOMENTO FALDAS ES EL MEJOR CANDIDATO PARA AVANZAR.

                    }
                    recorrido = recorrido.sig;
                }

                if (u == null) break; // si ya no hay vértices, corta el bucle
                u.visitado = true;

                //3.Evaluar los caminos posibles
                Arista a = u.ls.primero;//apunra a la primera arista de u
                while (a != null)//mientras existan aristas, sigue evaluando
                {
                    float nuevaDistancia = u.distancia + a.peso;//calculamos la distancia desde el inicio hasta el destino a través de u
                    if (nuevaDistancia < a.destino.distancia)//valor de la suma < valor de la distancia del destino, si es menor, actualizamos la distancia del destino
                    {                                        //15<infinito, entonces actualizamos la distancia del destino
                        a.destino.distancia = nuevaDistancia;
                        a.destino.anterior = u;//guardamos el vértice anterior para reconstruir la ruta más tarde
                    }
                    a = a.sig;//pasamos a la siguiente arista de u, para evaluar si hay un camino más corto hacia el destino
                }
                //el for se vuelve a ejecutar || ¿Cuál es el menor que no está visitado?
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

            ruta = "";//limpia las variables
            while (!pila.EstaVacia())
            {
                ruta += "-> " + pila.Desapilar();//salen los datos ordenados de inicio a meta
            }
        }
    }
}
