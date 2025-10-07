using Microsoft.AspNetCore.Mvc;
using espacioCadeteria;
using espacioCadete;
using espacioPedido;
using espacioJSON;

namespace tl2_tp4_2025_yazmoyano23.Controllers;

[ApiController]
[Route("api/cadeteria")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private IAccesoADatos acceso;
    public CadeteriaController()
    {
        acceso = new AccesoADatosJSON();
        List<Cadete> listaCadetes = acceso.LeerCadetes("DatosCadetes");
        cadeteria = acceso.LeerCadeteria("DatosCadeteria");
        List<Pedido> listaPedidos = acceso.LeerPedidos();
        cadeteria.SetCadetes(listaCadetes);
        cadeteria.SetPedidos(listaPedidos);
    }

    [HttpGet("GetCadetes")]
    public IActionResult GetCadetes()
    {
        List<Cadete> cadetes = cadeteria.GetCadetes();
        if (cadetes.Count == 0) { return NotFound("No se encontraron cadetes."); }
        return Ok(cadetes);
    }

    [HttpGet("GetPedidos")]
    public IActionResult GetPedidos()
    {
        List<Pedido> pedidos = cadeteria.GetPedidos();
        if (pedidos.Count == 0) { return NotFound("No se encontraron pedidos."); }
        return Ok(pedidos);
    }

    [HttpPost("AddPedido")]
    public IActionResult AddPedido([FromBody] PedidoRequest request)
    {
        if (request == null)
        {
            return BadRequest("La solicitud no puede estar vacía.");
        }

        int id = cadeteria.TomarPedido(
            request.Nombre,
            request.Direccion,
            request.Telefono,
            request.ReferenciaDireccion,
            request.Observacion
        );
        acceso.Guardar(cadeteria.GetPedidos());
        return Ok($"Pedido Nº{id} agregado exitosamente");
    }

    [HttpGet("GetInforme")]
    public ActionResult<IEnumerable<string>> GetInforme()
    {
        return Ok(cadeteria.ObtenerInforme());
    } 

    [HttpPut("AsignarPedido/{idCadete}/{idPedido}")]
    public ActionResult AsignarPedido(int idCadete, int idPedido)
    {
        if (cadeteria.AsignarPedidoACadete(idCadete, idPedido))
        {
            acceso.Guardar(cadeteria.GetPedidos());
            return Ok("Pedido asignado exitosamente");
        }
        else
        {
            return BadRequest("ID/s invalido/s");
        }
    }

    [HttpPut("CambiarEstadoPedido/{idPedido}/{numEstado}")]
    public ActionResult CambiarEstadoPedido(int idPedido, int numEstado)
    { 
        if (cadeteria.CambiarEstadoDePedidoPorId(idPedido,numEstado))
        {
            acceso.Guardar(cadeteria.GetPedidos());
            return Ok("Estado modificado correctamente");
        }else
        {
            return BadRequest("No se pudo cambiar el estado");
        }
    }
    
}