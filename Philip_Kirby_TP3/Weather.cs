namespace WebAPIClient;

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
    
    public int UserChoice()
    {
        int x;
        Console.Write("Rentrez le numéro de ville: ");
        while (!int.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("You entered an invalid number");
            Console.Write("Rentrez le numéro de ville:  ");
        }

        return x;
    }
    
    public void DisplayNameCities(Tuple<int, string, double, double>[] map)
    {
        Console.WriteLine("Choisir entre les villes suivantes : écrivez son numéro ! ");
        var i = 0;
        foreach (var ville in map)
        {
            i++;
            Console.WriteLine($"{i} ) {ville.Item2}");
        }
    }
    
    
}