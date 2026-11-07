class MainClass{
    public static void Main(string [] args)
    {
        Menu menu = new Menu();
        bool salir = false;
        while (salir != true)
        {
            Console.WriteLine("\nFLUJO DE FICHEROS\n");
            Console.WriteLine("¿Qué quieres hacer?\n");
            Console.WriteLine("1. De CSV a XML.\n");
            Console.WriteLine("2. De XML a CSV.\n");
            Console.WriteLine("3. Cerrar la aplicación.\n");
            Console.WriteLine("Pulse un número del 1 al 3:\n");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    menu.MenuCsv();
                    break;
                case "2":
                    menu.MenuXml();
                    break;
                case "3":
                    Console.WriteLine("Saliendo, hasta pronto...");
                    Thread.Sleep(1200);
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida, prueba otra vez o sal usando el 3");
                    Thread.Sleep(1200);
                    break;
            }
        }
    }
}