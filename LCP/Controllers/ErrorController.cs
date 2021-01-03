using LCP.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCP.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] // ignore for swagger
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Handling any type of HTTP method, hence no http annotation
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
