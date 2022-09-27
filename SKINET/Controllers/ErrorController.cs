using Microsoft.AspNetCore.Mvc;
using SKINET.Errors;

namespace SKINET.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController: BaseApiController
    {
        [NonAction]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
