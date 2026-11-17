using System;

namespace TallerAlgoritmosModulares
{
    static class Caso2Cafeteria
    {
        static int maximo = 20;
        static string[] categoriasNombre = { "Bebidas", "Panaderia", "Snacks" };

        static string[] codigos = new string[20];
        static string[] nombres = new string[20];
        static double[] precios = new double[20];
        static int[] cantidades = new int[20];
        static int[] categorias = new int[20];
        static int total = 0;
        static double totalVendido = 0;

        public static void Ejecutar()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- CAFETERIA ---");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Realizar venta");
                Console.WriteLine("3. Actualizar inventario");
                Console.WriteLine("4. Mostrar productos agotados");
                Console.WriteLine("5. Total vendido del dia");
                Console.WriteLine("6. Ver todos los productos");
                Console.WriteLine("0. Volver");
                opcion = Utilidades.LeerEntero("Opcion: ", 0, 6);

                if (opcion == 1) RegistrarProducto();
                else if (opcion == 2) RealizarVenta();
                else if (opcion == 3) ActualizarInventario();
                else if (opcion == 4) MostrarProductos(true);
                else if (opcion == 5) Console.WriteLine("Total vendido del dia: $" + totalVendido);
                else if (opcion == 6) MostrarProductos(false);

                if (opcion != 0) Utilidades.Pausa();
            } while (opcion != 0);
        }

        static int BuscarPosicion(string codigo)
        {
            for (int i = 0; i < total; i++)
                if (codigos[i].ToUpper() == codigo.ToUpper()) return i;
            return -1;
        }

        static void RegistrarProducto()
        {
            if (total >= maximo)
            {
                Console.WriteLine("Ya no caben mas productos (maximo " + maximo + ").");
                return;
            }

            string codigo = Utilidades.LeerTexto("Codigo: ");
            if (BuscarPosicion(codigo) != -1)
            {
                Console.WriteLine("Ese codigo ya existe.");
                return;
            }

            codigos[total] = codigo;
            nombres[total] = Utilidades.LeerTexto("Nombre: ");
            precios[total] = Utilidades.LeerDecimal("Precio unitario: ", 1, 1000000);
            cantidades[total] = Utilidades.LeerEntero("Cantidad disponible: ", 0, 10000);
            Console.WriteLine("Categoria: 1. Bebidas, 2. Panaderia, 3. Snacks");
            categorias[total] = Utilidades.LeerEntero("Categoria: ", 1, 3) - 1;
            total++;
            Console.WriteLine("Producto registrado.");
        }

        static void RealizarVenta()
        {
            int posicion = BuscarPosicion(Utilidades.LeerTexto("Codigo del producto: "));
            if (posicion == -1)
            {
                Console.WriteLine("No existe ese producto.");
                return;
            }
            if (cantidades[posicion] == 0)
            {
                Console.WriteLine("Producto agotado, no se puede vender.");
                return;
            }

            int cantidadVenta = Utilidades.LeerEntero("Cantidad a vender: ", 1, 10000);
            if (cantidadVenta > cantidades[posicion])
            {
                Console.WriteLine("No hay suficiente stock. Solo quedan " + cantidades[posicion]);
                return;
            }

            cantidades[posicion] -= cantidadVenta;
            totalVendido += cantidadVenta * precios[posicion];
            Console.WriteLine("Venta realizada. Subtotal: $" + cantidadVenta * precios[posicion]);
        }

        static void ActualizarInventario()
        {
            int posicion = BuscarPosicion(Utilidades.LeerTexto("Codigo del producto: "));
            if (posicion == -1)
            {
                Console.WriteLine("No existe ese producto.");
                return;
            }

            cantidades[posicion] += Utilidades.LeerEntero("Unidades que llegaron: ", 1, 10000);
            Console.WriteLine("Ahora hay " + cantidades[posicion] + " unidades.");
        }

        static void MostrarProductos(bool soloAgotados)
        {
            bool hayAlguno = false;
            for (int i = 0; i < total; i++)
            {
                if (soloAgotados && cantidades[i] != 0) continue;
                Console.WriteLine("Codigo: " + codigos[i] + " | " + nombres[i] + " | " + categoriasNombre[categorias[i]] + " | Precio: $" + precios[i] + " | Disponible: " + cantidades[i]);
                hayAlguno = true;
            }
            if (!hayAlguno) Console.WriteLine("No hay productos para mostrar.");
        }
    }
}