using MyPoki.Services.Pokis;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyPoki.Services;

public class PokiServices
{
    private readonly HttpClient _httpClient;

    public PokiServices (HttpClient httpClient)
    {  _httpClient = httpClient; }

    public async Task<Pokimon> GetPokifav(string PokiName)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{PokiName}");
        response.EnsureSuccessStatusCode();
        
        var PokiData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var Poki = new Pokimon
        {
            Name = PokiName,
            Type = PokiData.GetProperty("types").EnumerateArray().First().GetProperty("type").GetProperty("name").GetString(),
            URL = PokiData.GetProperty("sprites").GetProperty("front_default").GetString(),
            Moves = PokiData.GetProperty("moves").EnumerateArray().Select(m => m.GetProperty("move").GetProperty("name").GetString()).ToList()
        };
        return Poki;
    }
}