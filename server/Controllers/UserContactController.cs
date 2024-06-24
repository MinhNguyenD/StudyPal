using Microsoft.AspNetCore.Mvc;

namespace server.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class UserContactController : ControllerBase {
    [HttpPost]
    public JsonResult CreateContact(Models.UserContactData data) {
        Console.WriteLine(data.Email, data.Message, data.Name);
        return new JsonResult(Ok("hello"));
    }
}