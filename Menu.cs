using System;
using System.Text;

class Menu
{
    public void MenuCsv()
    {
        bool volver = false;
        Pelicula peli = new Pelicula("", "", "", 0, 0, "");
        while (volver != true)
        {
            Console.Clear();
            Console.WriteLine("\nCSV A XML\n");
            Console.WriteLine("1. Exportar todo el documento a XML.");
            Console.WriteLine("2. Exportar por género.");
            Console.WriteLine("3. Exportar por año (año seleccionado y superiores).");
            Console.WriteLine("4. Exportar por lenguaje.");
            Console.WriteLine("5. Volver al menú principal.");
            Console.Write("\nElige una opción (1-5): ");

            String opcionCsv = Console.ReadLine();
            switch (opcionCsv)
            {
                case "1":
                    peli.ExportarTodoElXml();
                    break;
                case "2":
                    peli.ExportarPorGeneroXml();
                    break;
                case "3":
                    peli.ExportarPorAnioXml();
                    break;
                case "4":
                    peli.ExportarPorIdiomaXml();
                    break;
                case "5":
                    Console.WriteLine("\nVolviendo al menú principal...");
                    Thread.Sleep(1000);
                    volver = true;
                    break;
                default:
                    Console.WriteLine("\nOpción no válida. Introduce un número del 1 al 5.");
                    break;
            }
            Console.ReadKey();
        }
    }
    public void MenuXml()
    {
        bool volver = false;
        Pelicula peli = new Pelicula("", "", "", 0, 0, "");

        while (volver != true)
        {
            Console.Clear();

            Console.WriteLine("\nXML A CSV\n");
            Console.WriteLine("1. Exportar todo el documento a CSV.");
            Console.WriteLine("2. Exportar por género.");
            Console.WriteLine("3. Exportar por año.");
            Console.WriteLine("4. Exportar por lenguaje.");
            Console.WriteLine("5. Volver al menú principal.");
            Console.Write("\nElige una opción (1-5): ");

            string opcionXml = Console.ReadLine();

            switch (opcionXml)
            {
                case "1":
                    peli.ExportarTodoElCsv();
                    break;

                case "2":
                    peli.ExportarPorGeneroCsv();
                    break;

                case "3":
                    peli.ExportarPorAnioCsv();
                    break;

                case "4":
                    peli.ExportarPorIdiomaCsv();
                    break;

                case "5":
                    Console.WriteLine("\nVolviendo al menú principal...");
                    Thread.Sleep(1000);
                    volver = true;
                    break;

                default:
                    Console.WriteLine("\nOpción no válida. Introduce un número del 1 al 5.");
                    break;
            }
            Console.ReadKey();
        }
    }
}
