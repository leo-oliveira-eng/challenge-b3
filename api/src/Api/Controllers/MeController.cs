using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class MeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { name = "CDB Yield Simulator", version = "1.0.0" });
}
