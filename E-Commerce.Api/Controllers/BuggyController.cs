using E_Commerce.Api.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("/unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("/badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("not a good request");
        }

        [HttpGet("/notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpPost("/validationerror")]
        public IActionResult GetValidationError(CreateProductDto product)
        {
            return Ok();
        }

        [HttpGet("/internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a Test Exception");
        }
    }
}
