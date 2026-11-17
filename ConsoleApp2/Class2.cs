using System;

namespace TallerAlgoritmosModulares
{
    static class Caso3Notas
    {
        static int maximo = 25;

        static string[] codigos = new string[25];
        static string[] nombres = new string[25];
        static double[,] notas = new double[25, 3];
        static double[] asistencias = new double[25];
        static int total = 0;

        public static void Ejecutar()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- CONTROL ACADEMICO ---");
                Console.WriteLine("1. Registrar estudiante");
                Console.WriteLine("2. Calcular definitiva y estado");
                Console.WriteLine("3. Promedio general del grupo");
                Console.WriteLine("4. Mejor y peor promedio");
                Console.WriteLine("0. Volver");
                opcion = Utilidades.LeerEntero("Opcion: ", 0, 4);

                if (opcion == 1) RegistrarEstudiante();
                else if (opcion == 2) MostrarDefinitivas();
                else if (opcion == 3) PromedioGeneral();
                else if (opcion == 4) MejorYPeor();

                if (opcion != 0) Utilidades.Pausa();
            } while (opcion != 0);
        }

        static void RegistrarEstudiante()
        {
            if (total >= maximo)
            {
                Console.WriteLine("Ya se registraron los " + maximo + " estudiantes.");
                return;
            }

            string codigo = Utilidades.LeerTexto("Codigo: ");
            for (int i = 0; i < total; i++)
            {
                if (codigos[i] == codigo)
                {
                    Console.WriteLine("Ese codigo ya existe.");
                    return;
                }
            }

            codigos[total] = codigo;
            nombres[total] = Utilidades.LeerTexto("Nombre: ");
            for (int j = 0; j < 3; j++)
                notas[total, j] = Utilidades.LeerDecimal("Nota " + (j + 1) + " (0.0 a 5.0): ", 0.0, 5.0);
            asistencias[total] = Utilidades.LeerDecimal("Porcentaje de asistencia (0 a 100): ", 0, 100);
            total++;
            Console.WriteLine("Estudiante registrado.");
        }

        static double CalcularDefinitiva(int i)
        {
            return (notas[i, 0] + notas[i, 1] + notas[i, 2]) / 3;
        }

        static string DeterminarEstado(double definitiva, double asistencia)
        {
            if (asistencia < 80) return "Reprueba";
            if (definitiva >= 3.0) return "Aprobado";
            if (definitiva >= 2.0) return "Habilita";
            return "Reprueba";
        }

        static void MostrarDefinitivas()
        {
            if (total == 0) Console.WriteLine("No hay estudiantes registrados.");
            for (int i = 0; i < total; i++)
            {
                double def = CalcularDefinitiva(i);
                Console.WriteLine(codigos[i] + " | " + nombres[i] + " | Definitiva: " + Math.Round(def, 2) + " | Asistencia: " + asistencias[i] + "% | Estado: " + DeterminarEstado(def, asistencias[i]));
            }
        }

        static void PromedioGeneral()
        {
            if (total == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            double suma = 0;
            for (int i = 0; i < total; i++)
                suma += CalcularDefinitiva(i);
            Console.WriteLine("Promedio general del grupo: " + Math.Round(suma / total, 2));
        }

        static void MejorYPeor()
        {
            if (total == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            int mejor = 0, peor = 0;
            for (int i = 1; i < total; i++)
            {
                if (CalcularDefinitiva(i) > CalcularDefinitiva(mejor)) mejor = i;
                if (CalcularDefinitiva(i) < CalcularDefinitiva(peor)) peor = i;
            }

            Console.WriteLine("Mejor promedio: " + nombres[mejor] + " (" + Math.Round(CalcularDefinitiva(mejor), 2) + ")");
            Console.WriteLine("Peor promedio: " + nombres[peor] + " (" + Math.Round(CalcularDefinitiva(peor), 2) + ")");
        }
    }
}