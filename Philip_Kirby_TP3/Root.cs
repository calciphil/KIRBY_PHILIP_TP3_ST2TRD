namespace WebAPIClient;

public class Root
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public List<List> list { get; set; }
    public string @base { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
    public City city { get; set; }
    public int cnt { get; set; }
    public int message { get; set; }
}