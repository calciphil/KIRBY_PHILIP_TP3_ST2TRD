using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Threading.Channels;
using Newtonsoft.Json;
using WebAPIClient;

static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
{
    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
    dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
    return dtDateTime;
}

static string AirToQuality(int pollution)
{
    if (pollution == 1)
        return "good";
    else if (pollution == 2)
        return "fair";
    else if (pollution == 2)
        return "moderate";
    else if (pollution == 2)
        return "poor";
    else if (pollution == 2)
        return "very poor";
    return "no data";
}
/*
 
// --- Question 1 ---
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=Morocco,ma&exclude=lon&APPID=8aba9d73b0944ea50196ad82b055430c")
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    string body = await response.Content.ReadAsStringAsync();
    // Console.WriteLine(body);
    Root morocco = JsonConvert.DeserializeObject<Root>(body);
    string moroccoWeather = morocco.weather[0].description;
    Console.WriteLine("\nWhat’s the weather like in Morocco? : " + moroccoWeather + "\n");
}

// --- Question 2 ---

var molo = new HttpClient();
var sunstuff = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?&lat=59.91&lon=10.75&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response1 = await molo.SendAsync(sunstuff))
{
    response1.EnsureSuccessStatusCode();
    string body = await response1.Content.ReadAsStringAsync();
    Root oslo = JsonConvert.DeserializeObject<Root>(body);
    var osloSunRiseUTC = UnixTimeStampToDateTime(oslo.sys.sunrise);
    var osloSunSetUTC = UnixTimeStampToDateTime(oslo.sys.sunset);
    Console.WriteLine("Oslo Sunrise : " + osloSunRiseUTC + "\nOslo Sunset : " + osloSunSetUTC + "\n");
}

// Question 3 

var jaja = new HttpClient();
var temperature = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?&lat=-6.21&lon=106.84&units=metric&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response2 = await jaja.SendAsync(temperature))
{
    response2.EnsureSuccessStatusCode();
    string body = await response2.Content.ReadAsStringAsync();
    //Console.WriteLine(body);
    Root jakarta = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Temperature in Jakarta is : "+jakarta.main.temp+"°C\n");
}
//question 4
var wind = new HttpClient();
var question3 = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/group?id=5128581,1850147,2968815&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response3 = await wind.SendAsync(question3))
{
    response3.EnsureSuccessStatusCode();
    string body = await response3.Content.ReadAsStringAsync();
    var win = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Wind Speed in New York is : " + win.list[0].wind.speed);
    Console.WriteLine("Wind Speed in Tokyo is : " + win.list[1].wind.speed);
    Console.WriteLine("Wind Speed in Paris is : " + win.list[2].wind.speed);
    double high = new[] { win.list[0].wind.speed, win.list[1].wind.speed, win.list[2].wind.speed }.Max();
    Console.WriteLine("The highest wind Speed is : " + high +"\n");
}

//question 5 
var quest5 = new HttpClient();
var question5 = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/group?id=703448,524901,2950158&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response5 = await quest5.SendAsync(question5))
{
    response5.EnsureSuccessStatusCode();
    string body = await response5.Content.ReadAsStringAsync();
    var hum = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Humidity and Pressure in Kiev is respectively : " + hum.list[0].main.humidity + " - "+ hum.list[0].main.pressure);
    Console.WriteLine("Humidity and Pressure in Moscow is respectively : " + hum.list[1].main.humidity + " - "+ hum.list[1].main.pressure);
    Console.WriteLine("Humidity and Pressure in Berlin is respectively : : " + hum.list[2].main.humidity + " - "+ hum.list[2].main.pressure);
    
}
*/
// TP3

Tuple<int, string, double, double>[] Villes = 
{   Tuple.Create(1, "Paris", 48.8534, 2.3488),
    Tuple.Create(2, "Tokyo", 35.6895, 139.6917), 
    Tuple.Create(3, "Belgrade", 44.804, 20.4651),
    Tuple.Create(4, "Moscow", 55.7522, 37.6156), 
    Tuple.Create(5, "New York City", 40.7143, -74.006),
    Tuple.Create(6, "Chicago", 41.85, -87.65),
    Tuple.Create(7, "Lisbon", 38.7167, -9.1333),
    Tuple.Create(8, "Copenhagen", 55.6759, 12.5655),
    Tuple.Create(9, "London", 51.5085, -0.1257),
    Tuple.Create(10, "Rome", 34.257, -85.1647),
    Tuple.Create(11, "Madrid", 40.4165, -3.7026),
    Tuple.Create(12, "Lomé", 6.1375, 1.2123),
    Tuple.Create(13, "Cape Town", -33.9258, 18.4232),
    Tuple.Create(14, "Montreal", 45.5088, -73.5878),
    Tuple.Create(15, "San Antonio", 29.4241, -98.4936) };

var weather = new Weather();
weather.DisplayNameCities(Villes);
int choice = weather.UserChoice();
Console.WriteLine("You choose to read infos about : "+Villes[choice-1].Item2+"\nHere is today's meteorologic information : ");



var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?lat="+Villes[choice-1].Item3+"&lon="+Villes[choice-1].Item4+"&units=metric&APPID=8aba9d73b0944ea50196ad82b055430c")
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    string body = await response.Content.ReadAsStringAsync();
    //Console.WriteLine(body);
    Root city = JsonConvert.DeserializeObject<Root>(body);
    string choiceWeather = city.weather[0].description;
    double choiceTemp = city.main.temp;
    double choiceWindSpeed = city.wind.speed;
    double choiceSunset = city.sys.sunset;
    double choiceSunrise = city.sys.sunrise;
    double choicePressure = city.main.pressure;


    Console.WriteLine("\nWhat’s the weather like in " + Villes[choice - 1].Item2 + "? : " + choiceWeather + "\n" +
                      "Current Temperature :" + choiceTemp + " °C\n" +
                      "Current Wind Speed : " + choiceWindSpeed + " km/h\n" +
                      "Current Sunrise Time : " + UnixTimeStampToDateTime(choiceSunrise) + "\n" +
                      "Current Sunset Time : " + UnixTimeStampToDateTime(choiceSunset) + "\n" +
                      "Current Pressure : " + choicePressure + " hPa\n"
    );
}
Console.WriteLine("Here is the overall forecast information for the next 5 days in "+Villes[choice-1].Item2+":");
var clientforecast = new HttpClient();
var requestforecast = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/forecast?lat="+Villes[choice-1].Item3+"&lon="+Villes[choice-1].Item4+"&units=metric&appid=8aba9d73b0944ea50196ad82b055430c")
};
using (var responseforecast = await clientforecast.SendAsync(requestforecast))
{
    responseforecast.EnsureSuccessStatusCode();
    string bodyforecast = await responseforecast.Content.ReadAsStringAsync();
    //Console.WriteLine(bodyforecast);
    Root cityforecast = JsonConvert.DeserializeObject<Root>(bodyforecast);
    for (int i = 0; i <= 39; i++)
    {
        Console.WriteLine((i+1)+") In "+Villes[choice-1].Item2+", at time : "+ UnixTimeStampToDateTime(cityforecast.list[i].dt)+", temperature is "+cityforecast.list[i].main.temp+"°C / Weather : "+cityforecast.list[i].weather[0].description);
    }

    Console.WriteLine("\n");
}

Console.WriteLine("Here is today's pollution information in "+Villes[choice-1].Item2+":");
var clientpollution = new HttpClient();
var requestpollution = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("http://api.openweathermap.org/data/2.5/air_pollution?lat="+Villes[choice-1].Item3+"&lon="+Villes[choice-1].Item4+"&appid=8aba9d73b0944ea50196ad82b055430c")
};
using (var responsepollution = await clientpollution.SendAsync(requestpollution))
{
    responsepollution.EnsureSuccessStatusCode();
    string bodypollution = await responsepollution.Content.ReadAsStringAsync();
    //Console.WriteLine(bodypollution);
    Root citypollution = JsonConvert.DeserializeObject<Root>(bodypollution);
    Console.WriteLine("Currently, the Air quality in "+Villes[choice-1].Item2+" is "+AirToQuality(citypollution.list[0].main.aqi)+".\nThere is "+citypollution.list[0].components.co+"μg/m3 of carbon monoxyde in the air.");
    System.Threading.Thread.Sleep(60000);
}


