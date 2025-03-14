using Azure;
using Microsoft.AspNetCore.Mvc;

namespace StudentAtendances.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
