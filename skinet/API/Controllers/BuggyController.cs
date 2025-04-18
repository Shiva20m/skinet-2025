using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

// this controller is only use for the errrorhandling purpose
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }   

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a test exception");
        }

        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDto productDto)
        {
            return Ok();
        }
    }
    
}