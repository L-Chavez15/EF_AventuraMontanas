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
            Grafo montaña = new Grafo(cantidadNodos);

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
                Console.Write("║");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("           AVENTURA EN LAS MONTAÑAS  ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("           ║");
                Console.WriteLine("╠════════════════════════════════════════════════╣");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("║                                                ║");
                Console.Write("║");
                Console.ResetColor();
                Console.Write("   \t\t1. Jugar");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                         ║");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("║");
                Console.ResetColor();
                Console.Write("   \t\t2. Salir");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("                         ║");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("║                                                ║");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╚════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        MostrarBienvenida();

                        montaña.RecorrerGrafo(inicio, ref estaminaJugador, ref rutaJugador);

                        // El sistema calcula el camino REALMENTE más óptimo con Dijkstra
                        montaña.CalcularRutaOptima(inicio, ref estaminaOptima, ref rutaOptima);

                        MostrarResultados(rutaJugador, estaminaJugador, rutaOptima, estaminaOptima);

                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\n¡Gracias por jugar! Hasta la próxima escalada. 🏔️");
                        Console.ResetColor();
                        opcion = 0; // arreglo: fuerza la salida del do-while
                        break;
                    default:
                        Console.WriteLine("INGRESE UNA OPCIÓN VÁLIDA");
                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                }

            } while (opcion != 0);

        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=====================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("        *  SIMULADOR DE ESCALADA CELESTE  *");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=====================================================");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("  Dicen que en la cima de esta montaña vive un");
            Console.WriteLine("  silencio que ningún viajero ha logrado describir.");
            Console.WriteLine("  Muchos han partido a buscarlo; pocos han vuelto");
            Console.WriteLine("  para contarlo.");
            Console.WriteLine();
            Console.WriteLine("  Elige tu camino con cuidado: cada salto consume");
            Console.WriteLine("  estamina. Al final, el sistema te dirá si tomaste");
            Console.WriteLine("  la ruta más óptima.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=====================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n Presiona ENTER para empezar a escalar...");
            Console.ResetColor();
            Console.ReadLine();
        }

        static void MostrarResultados(string rutaJugador, float estaminaJugador, string rutaOptima, float estaminaOptima)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=====================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                  *  RESULTADOS  *");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=====================================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*  TU PARTIDA:");
            Console.ResetColor();
            Console.WriteLine("* Camino que tomaste:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(rutaJugador);
            Console.ResetColor();
            Console.Write("   Estamina total consumida: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(estaminaJugador + " Pts.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n-----------------------------------------------------\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("* RECOMENDACIÓN DEL SISTEMA :");
            Console.ResetColor();
            Console.WriteLine("   El camino más óptimo era:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   " + rutaOptima);
            Console.ResetColor();
            Console.Write("   Estamina mínima posible: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Math.Round(estaminaOptima, 2) + " Pts.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n=====================================================");
            Console.ResetColor();

            if (estaminaJugador <= estaminaOptima)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n ¡Felicidades! Encontraste la ruta perfecta.");
                Console.WriteLine(" La montaña recordará tu nombre entre sus vientos.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Sobreviviste, pero el sistema encontró una ruta mejor.");
                Console.WriteLine("  Quizás en tu próxima escalada, la montaña sea más generosa.");
            }
            Console.ResetColor();
        }
    }
}
