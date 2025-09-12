using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2025_yazmoyano23.Controllers;

[ApiController]
[Route("api/comida")]
public class ComidaController : ControllerBase
{
    public ComidaController()
    { 
        
    }

    [HttpGet]
    public IActionResult GetComida()
    {
        var prueba = new Comida();
        return Ok("Comida: " + prueba.GetNombre());
    }
}

