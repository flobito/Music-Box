using System;
using System.Collections.Generic;
/*
public class Program
{
    // Notas musicales válidas
    private static List<string> notasValidas = new List<string> { "Do", "Re", "Mi", "Fa", "Sol", "La", "Si" };
    
    // Figuras musicales válidas
    private static List<string> figurasValidas = new List<string> { "redonda", "blanca", "negra", "corchea", "semicorchea" };

    public static void Main(string[] args)
    {
        bool entradaValida = false;

        // Validar la entrada de la partitura
        while (!entradaValida)
        {
            Console.WriteLine("Ingrese las notas y duraciones en el formato (Nota, figura musical), separados por comas. Ejemplo: (Do, blanca), (Re, corchea)");
            string entrada = Console.ReadLine();
            entradaValida = true;
            string[] notasYDuraciones = entrada.Split(new string[] { "), (" }, StringSplitOptions.None);

            foreach (var item in notasYDuraciones)
            {
                string itemLimpio = item.Trim().TrimStart('(').TrimEnd(')');
                if (itemLimpio.Contains(","))
                {
                    string[] partes = itemLimpio.Split(',');

                    if (partes.Length == 2)
                    {
                        string nota = partes[0].Trim();
                        string figuraMusical = partes[1].Trim().ToLower();

                        // Validar nota
                        if (!notasValidas.Contains(nota))
                        {
                            Console.WriteLine($"Nota inválida: {nota}");
                            entradaValida = false;
                            break;
                        }

                        // Validar figura musical
                        if (!figurasValidas.Contains(figuraMusical))
                        {
                            Console.WriteLine($"Figura inválida: {figuraMusical}");
                            entradaValida = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Formato incorrecto: {itemLimpio}");
                        entradaValida = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"Formato incorrecto: {item}");
                    entradaValida = false;
                    break;
                }
            }

            if (!entradaValida)
            {
                Console.WriteLine("Entrada inválida. Intente nuevamente.");
            }
            else
            {
                Console.WriteLine(entradaValida);
            }
        }
    }
}
*/

using System;
using System.Collections.Generic;

class Program
{

    public static void Main(string[] args)
    {
        // Configurar el tiempo de la figura negra
        ConfigurarTiempoFiguraNegra();
    }

    // Función para pedir la duración de la figura negra
    public static void ConfigurarTiempoFiguraNegra()
    {
        double nuevaDuracionNegra;

        Console.WriteLine("Ingrese la duración de la figura negra en segundos (rango: 0.1 a 5):");
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out nuevaDuracionNegra) && nuevaDuracionNegra >= 0.1 && nuevaDuracionNegra <= 5)
            {
                // Duración válida, ajustamos las figuras musicales
                Console.WriteLine("Duración válida.");
                break;  // Salir del bucle si la entrada es válida
            }
            else
            {
                // Duración inválida
                Console.WriteLine("Duración inválida. Por favor, ingrese un valor entre 0.1 y 5 segundos.");
            }
        }
    }

 
}
