using System.Net.Http.Json;
namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        httpClient = new HttpClient();
    }
    List<Monkey> monkeyList = new();
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList?.Count() > 0)
            return monkeyList;

        //var url = "https://gist.github.com/harshmangalamv/8d7252a9e0cd41287cf7560d34faf295#file-monkeydata-json:~:text=%5B,%5D";
        //var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

        //var response = await httpClient.GetAsync(url);

        /*if (response.IsSuccessStatusCode) 
        {
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }
        */

        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);     

        return monkeyList;
    }
}
