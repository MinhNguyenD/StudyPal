using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace server;

[ApiController]
[Authorize]
[Route("test")]
public class TestController : ControllerBase
{
    public TestController()
    {

    }

    [HttpGet]
    public IActionResult GetTests()
    {
        var x = new List<string>{
            "x","y","z"
        };
        return Ok(x);
    }
}
