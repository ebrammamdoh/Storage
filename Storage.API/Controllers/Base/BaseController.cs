using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Models;

namespace Storage.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Ok(string errorCode, string errorMessage)
        {
            var response = new GeneralResponse
            {
                Code = errorCode,
                Message = errorMessage
            };
            return Ok(response);
        }

        [NonAction]
        public IActionResult BadRequest(string errorCode, string errorMessage)
        {
            var response = new GeneralResponse
            {
                Code = errorCode,
                Message = errorMessage
            };
            return BadRequest(response);
        }
    }
}
