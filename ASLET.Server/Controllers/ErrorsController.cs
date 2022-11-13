using Microsoft.AspNetCore.Mvc;

namespace ASLET.Server.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}