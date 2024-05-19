using Microsoft.AspNetCore.Mvc;
using MyPoki.Services.Pokis;
using MyPoki.Services;

namespace PokemonExam.Controllers;

[ApiController]
[Route("[controller]")]
public class PokiController : Controller
{
    private readonly PokiServices _services;

    public PokiController(PokiServices services)
    {
        _services = services;
    }
    [HttpGet ("{PokiName}")]

    public async Task<ActionResult<Pokimon>> Get(string PokiName)
    {
        var Poki = await _services.GetPokifav(PokiName);
        return Ok(Poki);
    }
}