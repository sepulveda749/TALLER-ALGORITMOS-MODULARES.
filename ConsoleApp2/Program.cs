using System;

namespace TallerAlgoritmosModulares
{
    static class Caso1Ips
    {
        static int maximo = 30;
        static string[] tiposNombre = { "Urgencias", "Consulta General", "Prioritaria" };

        static string[] documentos = new string[30];
        static string[] nombres = new string[30];
        static int[] edades = new int[30];
        static int[] tipos = new int[30];
        static int[] prioridades = new int[30];
        static int cantidad = 0;

        public static void Ejecutar()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- IPS (cupos disponibles: " + (maximo - cantidad) + ") ---");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Mostrar listado general");
                Console.WriteLine("3. Buscar paciente por documento");
                Console.WriteLine("4. Pacientes por tipo de atencion");
                Console.WriteLine("5. Paciente con mayor prioridad");
                Console.WriteLine("0. Volver");
                opcion = Utilidades.LeerEntero("Opcion: ", 0, 5);

                if (opcion == 1) RegistrarPaciente();
                else if (opcion == 2) MostrarListado();
                else if (opcion == 3) BuscarPaciente();
                else if (opcion == 4) ContarPorTipo();
                else if (opcion == 5) MayorPrioridad();

                if (opcion != 0) Utilidades.Pausa();
            } while (opcion != 0);
        }

        static int BuscarPosicion(string documento)
        {
            for (int i = 0; i < cantidad; i++)
                if (documentos[i] == documento) return i;
            return -1;
        }

        static void RegistrarPaciente()
        {
            if (cantidad >= maximo)
            {
                Console.WriteLine("No hay cupos disponibles.");
                return;
            }

            string documento = Utilidades.LeerTexto("Numero de documento: ");
            if (BuscarPosicion(documento) != -1)
            {
                Console.WriteLine("Ese documento ya esta registrado.");
                return;
            }

            documentos[cantidad] = documento;
            nombres[cantidad] = Utilidades.LeerTexto("Nombre completo: ");
            edades[cantidad] = Utilidades.LeerEntero("Edad: ", 0, 120);
            Console.WriteLine("Tipo de atencion: 1. Urgencias, 2. Consulta General, 3. Prioritaria");
            tipos[cantidad] = Utilidades.LeerEntero("Tipo: ", 1, 3) - 1;
            prioridades[cantidad] = Utilidades.LeerEntero("Prioridad (1 a 5, el 5 es la mayor): ", 1, 5);
            cantidad++;
            Console.WriteLine("Paciente registrado.");
        }

        static void MostrarPaciente(int i)
        {
            Console.WriteLine("Documento: " + documentos[i] + " | Nombre: " + nombres[i] + " | Edad: " + edades[i] + " | Atencion: " + tiposNombre[tipos[i]] + " | Prioridad: " + prioridades[i]);
        }

        static void MostrarListado()
        {
            if (cantidad == 0) Console.WriteLine("No hay pacientes registrados.");
            for (int i = 0; i < cantidad; i++)
                MostrarPaciente(i);
        }

        static void BuscarPaciente()
        {
            int posicion = BuscarPosicion(Utilidades.LeerTexto("Documento a buscar: "));
            if (posicion == -1) Console.WriteLine("No se encontro el paciente.");
            else MostrarPaciente(posicion);
        }

        static void ContarPorTipo()
        {
            int[] conteo = new int[3];
            for (int i = 0; i < cantidad; i++)
                conteo[tipos[i]]++;
            for (int t = 0; t < 3; t++)
                Console.WriteLine(tiposNombre[t] + ": " + conteo[t]);
        }

        static void MayorPrioridad()
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            int posMayor = 0;
            for (int i = 1; i < cantidad; i++)
                if (prioridades[i] > prioridades[posMayor]) posMayor = i;

            Console.WriteLine("Paciente con mayor prioridad:");
            MostrarPaciente(posMayor);
        }
    }
}