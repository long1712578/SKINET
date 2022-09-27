using Intrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using SKINET.Errors;

namespace SKINET.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);
            if(thing is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("badrequest")]
        public ActionResult GeBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("badrequest/{id}")]
        public ActionResult GeBadRequest(int id)
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}
