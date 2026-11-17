using System;
using System.Globalization;

namespace TallerAlgoritmosModulares
{
    static class Utilidades
    {
        public static int LeerEntero(string mensaje, int min, int max)
        {
            int n;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out n) || n < min || n > max)
                Console.Write("Dato invalido (" + min + " a " + max + "): ");
            return n;
        }

        public static double LeerDecimal(string mensaje, double min, double max)
        {
            double n;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out n) || n < min || n > max)
                Console.Write("Dato invalido (" + min + " a " + max + "): ");
            return n;
        }

        public static string LeerTexto(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(texto));
            return texto.Trim();
        }

        public static void Pausa()
        {
            Console.Write("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}