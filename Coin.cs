public class Coin
{
    public int Value { get; set; }
    public int Stock { get; set; }

    public Coin(int value, int stock)
    {
        Value = value;
        Stock = stock;
    }

    internal void Push(int v)
    {
        Stock += v;
    }


    internal void Pop()
    {
        if (Stock > 0)
        {
            Stock--;
        }
        else
        {
            // Maneja el caso en el que no hay monedas en el stock para eliminar
            Console.WriteLine("No hay monedas disponibles en el stock.");
        }
    }

    internal int Count()
    {
        return Stock;
    }
}
