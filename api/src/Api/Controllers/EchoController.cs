using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class EchoController : ControllerBase
{
    [HttpGet, ProducesResponseType(typeof(object), 200)]
    public IActionResult Get() => Ok(new { name = "CDB Yield Simulator", version = "1.0.0" });
}
