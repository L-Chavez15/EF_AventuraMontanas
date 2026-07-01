using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecucion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cantidadNodos = 10;
            Grafo montaña= new Grafo(cantidadNodos);

            montaña.GenerarMatriz();
            montaña.CrearGarfo();

            Vertice inicio = montaña.GetInicio();

            // Variables del Jugador
            float estaminaJugador = 0;
            string rutaJugador = "";

            // Variables del Sistema (Dijkstra)
            float estaminaOptima = 0;
            string rutaOptima = "";

            int opcion = 0;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("           AVENTURA EN LAS MONTAÑAS  ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╠════════════════════════════════════════════════╣");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   1. Jugar");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("   2. Salir");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╚═══════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("Elige una opción: ");
                opcion=int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        MostrarBienvenida();

                        montaña.RecorrerGrafo(inicio, ref estaminaJugador, ref rutaJugador);

                        // El sistema calcula el camino REALMENTE más óptimo con Dijkstra
                        montaña.CalcularRutaOptima(inicio, ref estaminaOptima, ref rutaOptima);

                        MostrarResultados(rutaJugador, estaminaJugador, rutaOptima, estaminaOptima);

                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para finalizar...");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\n¡Gracias por jugar! Hasta la próxima escalada. 🏔️");
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("INGRESE UNA OPCIÓN VÁLIDA");
                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (opcion!=0);

        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("        *  SIMULADOR DE ESCALADA CELESTE  *");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("  Elige tu camino con cuidado: cada salto consume");
            Console.WriteLine("  estamina. Al final, el sistema te dirá si tomaste");
            Console.WriteLine("  la ruta más óptima usando el algoritmo de Dijkstra.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n Presiona ENTER para empezar a escalar...");
            Console.ResetColor();
            Console.ReadLine();
        }

        static void MostrarResultados(string rutaJugador, float estaminaJugador, string rutaOptima, float estaminaOptima)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                  *  RESULTADOS  *");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==================================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*  TU PARTIDA:");
            Console.ResetColor();
            Console.WriteLine("   Camino que tomaste:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + rutaJugador.Substring(4));
            Console.ResetColor();
            Console.Write("   Estamina total consumida: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(estaminaJugador + " Pts.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n--------------------------------------------------\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("* RECOMENDACIÓN DEL SISTEMA :");
            Console.ResetColor();
            Console.WriteLine("   El camino más óptimo era:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + rutaOptima.Substring(4));
            Console.ResetColor();
            Console.Write("   Estamina mínima posible: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(estaminaOptima + " Pts.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n==================================================");
            Console.ResetColor();

            if (estaminaJugador <= estaminaOptima)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n ¡Felicidades! Encontraste la ruta perfecta.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Sobreviviste, pero el sistema encontró una ruta mejor.");
            }
            Console.ResetColor();
        }
    }
}
