class Program
{
    static void Main()
    {
        VendingMachine vendingMachine = new VendingMachine();
        


        // Agrega productos y monedas iniciales a la máquina expendedora

        bool isRunning = true;
        while (isRunning)
        {

            Console.WriteLine("\n\n-----------------\n");
            Console.WriteLine("MENU PRINCIPAL-----\n");
            Console.WriteLine("1. Comprar producto");
            Console.WriteLine("2. Acceder como usuario surtidor");
            Console.WriteLine("3. Salir");
            Console.Write("Selecciona una opción:\n ");
            string choice = Console.ReadLine();
            Console.WriteLine("\n\n");
            switch (choice)
            {
                case "1":                   
                    vendingMachine.BuyProduct();
                    break;
                case "2":                   
                    vendingMachine.AccessSurtidorMode();
                    break;
                case "3":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    break;
            }
        }
    }
}
