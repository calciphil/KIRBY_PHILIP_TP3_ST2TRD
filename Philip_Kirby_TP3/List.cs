namespace WebAPIClient;

public class List
{
    public Coord coord { get; set; }
    public Sys sys { get; set; }
    public List<Weather> weather { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public Components components { get; set; }
    
}