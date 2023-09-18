using System;
using System.Collections.Generic;

public class VendingMachine
{
    
    private List<Product> products;
    
    // Puesto para evitar el error de verificación solamente
    Coin coins500 = new Coin(500, 100);
    Coin coins200 = new Coin(200, 200);
    Coin coins100 = new Coin(100, 150);
    Coin coins50 = new Coin(50, 300);
    private Dictionary<int, Stack<Coin>> coins;
    
    
    public VendingMachine()
    {
        products = new List<Product>();
        coins = new Dictionary<int, Stack<Coin>>();

        // Agregar algunos productos iniciales
        products.Add(new Product("Refresco", 1500, 10));
        products.Add(new Product("Agua", 1150, 20));
        products.Add(new Product("Snacks", 1850, 15));
        products.Add(new Product("Dulces", 200, 100));
        

        // Agregar monedas iniciales al stock
        AddCoinsToStock(500, 10); // 10 monedas de 500 centavos
        AddCoinsToStock(200, 20); // 20 monedas de 200 centavos
        AddCoinsToStock(100, 15); // 15 monedas de 100 centavos
        AddCoinsToStock(50, 30);  // 30 monedas de 50 centavos
    }

    private void AddCoinsToStock(int denomination, int quantity)
    {
        if (!coins.ContainsKey(denomination))
        {
            coins[denomination] = new Stack<Coin>();
        }

        for (int i = 0; i < quantity; i++)
        {
            coins[denomination].Push(new Coin(denomination, 1));
        }
    }

    // Agregar un producto a la máquina expendedora
    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    // Agregar monedas a la máquina expendedora
    public void AddCoins(int value, int quantity)
    {
        if (!coins.ContainsKey(value))
        {
            coins[value] = new Stack<Coin>();
        }

        for (int i = 0; i < quantity; i++)
        {
            coins[value].Push(new Coin(value, 1));
        }
    }

    // Comprar un producto
    public void BuyProduct()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("Productos disponibles:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - Precio: {products[i].Price:C} - Stock: {products[i].Stock}");
        }
        Console.WriteLine("\n\n");
        Console.Write("Selecciona un producto (número): ");
        if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex >= 1 && productIndex <= products.Count)
        {
            Product selectedProduct = products[productIndex - 1];

            if (selectedProduct.Stock > 0)
            {
                decimal remainingAmount = selectedProduct.Price;

                Console.WriteLine($"Precio del producto: {selectedProduct.Price:C}");
                Console.WriteLine($"Ingresa monedas para pagar ({remainingAmount:C} restantes):");

                while (remainingAmount > 0)
                {
                    Console.Write("Ingresa moneda (ejemplo: 500, 200, 100, 50): ");
                    if (int.TryParse(Console.ReadLine(), out int coinValue) && coins.ContainsKey(coinValue))
                    {
                        if (coins[coinValue].Count > 0)
                        {
                            Coin coin = coins[coinValue].Pop();
                            remainingAmount -= coin.Value;
                            Console.WriteLine($"Has ingresado una moneda de {coin.Value:C}. Faltan {remainingAmount:C}.");
                        }
                        else
                        {
                            Console.WriteLine($"No quedan monedas de {coinValue:C} disponibles.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Moneda no válida.");
                    }
                }

                if (remainingAmount < 0)
                {
                    decimal changeAmount = Math.Abs(remainingAmount);
                    Console.WriteLine($"Has comprado {selectedProduct.Name}. Tu cambio es {changeAmount:C}");
                    selectedProduct.Stock--;
                    UpdateCoinsStock((int)selectedProduct.Price, (int)changeAmount);
                }
                else if (remainingAmount == 0)
                {
                    Console.WriteLine($"Has comprado {selectedProduct.Name}. No se requiere cambio.");
                    selectedProduct.Stock--;
                }
                else
                {
                    Console.WriteLine("El pago no es suficiente. Cancelando la compra.");
                }
            }
            else
            {
                Console.WriteLine($"Producto {selectedProduct.Name} agotado.");
            }
        }
        else
        {
            Console.WriteLine("Selección de producto no válida.");
        }

    }

    private void UpdateCoinsStock(int paymentAmount, int changeAmount)
{
    // Actualiza el stock de monedas con las monedas ingresadas por el usuario
    // Incrementa el stock de monedas correspondiente al pago
    // y decrementa el stock de monedas correspondiente al cambio dado.

    // Actualiza el stock de monedas de 500
    while (paymentAmount >= 500)
    {
        coins500.Push(500); // Agrega una moneda de 500 al stock
        paymentAmount -= 500;
    }

    // Actualiza el stock de monedas de 200
    while (paymentAmount >= 200)
    {
        coins200.Push(200); // Agrega una moneda de 200 al stock
        paymentAmount -= 200;
    }

    // Actualiza el stock de monedas de 100
    while (paymentAmount >= 100)
    {
        coins100.Push(100); // Agrega una moneda de 100 al stock
        paymentAmount -= 100;
    }

    // Actualiza el stock de monedas de 50
    while (paymentAmount >= 50)
    {
        coins50.Push(50); // Agrega una moneda de 50 al stock
        paymentAmount -= 50;
    }

    // Actualiza el stock de monedas de cambio
    while (changeAmount >= 500 && coins500.Count() > 0)
    {
        coins500.Pop(); // Elimina una moneda de 500 del stock
        changeAmount -= 500;
    }

    while (changeAmount >= 200 && coins200.Count() > 0)
    {
        coins200.Pop(); // Elimina una moneda de 200 del stock
        changeAmount -= 200;
    }

    while (changeAmount >= 100 && coins100.Count() > 0)
    {
        coins100.Pop(); // Elimina una moneda de 100 del stock
        changeAmount -= 100;
    }

    while (changeAmount >= 50 && coins50.Count() > 0)
    {
        coins50.Pop(); // Elimina una moneda de 50 del stock
        changeAmount -= 50;
    }
}



    // Acceder al modo usuario surtidor
    public void AccessSurtidorMode()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("Modo Usuario Surtidor:");
        Console.WriteLine("1. Cargar stock productos");
        Console.WriteLine("2. Cargar nuevo producto");
        Console.WriteLine("3. Salir del modo usuario surtidor");
        Console.Write("Selecciona una opción: ");
        string surtidorChoice = Console.ReadLine();
        Console.WriteLine("\n\n");
        switch (surtidorChoice)
        {
            case "1":
                LoadProducts();
                break;
            case "2":
                NewProduct();
                break;
            case "3":
                Console.WriteLine("Saliendo del modo usuario surtidor.");
                break;
            default:
                Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                break;
        }
    }

    private void NewProduct()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("Ingresar un nuevo producto:");

        Console.Write("Nombre del producto: ");
        string productName = Console.ReadLine();

        Console.Write("Precio del producto: ");
        if (int.TryParse(Console.ReadLine(), out int productPrice) && productPrice > 0)
        {
            Console.Write("Stock inicial del producto: ");
            if (int.TryParse(Console.ReadLine(), out int productStock) && productStock >= 0)
            {
                // Crea una nueva instancia de Product y agrégala a la lista de productos
                Product newProduct = new Product(productName, (decimal)productPrice / 100, productStock);
                products.Add(newProduct);

                Console.WriteLine($"Se ha agregado el producto: {newProduct.Name} - Precio: {newProduct.Price:C} - Stock: {newProduct.Stock}");
            }
            else
            {
                Console.WriteLine("Cantidad de stock no válida. Debe ser un número positivo o cero.");
            }
        }
        else
        {
            Console.WriteLine("Precio no válido. Debe ser un número mayor que cero.");
        }
    }

    


    private void LoadProducts()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("Cargar productos en la máquina expendedora:");

        // Muestra la lista de productos disponibles para cargar
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - Stock actual: {products[i].Stock}");
        }

        Console.Write("Selecciona el producto que deseas cargar (número): ");
        if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex >= 1 && productIndex <= products.Count)
        {
            Console.Write("Ingresa la cantidad a cargar: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity >= 0)
            {
                Product selectedProduct = products[productIndex - 1];
                selectedProduct.Stock += quantity;
                Console.WriteLine($"Se han cargado {quantity} unidades de {selectedProduct.Name} en la máquina expendedora.");
            }
            else
            {
                Console.WriteLine("Cantidad no válida. Debe ser un número positivo.");
            }
        }
        else
        {
            Console.WriteLine("Selección de producto no válida.");
        }
    }




}
