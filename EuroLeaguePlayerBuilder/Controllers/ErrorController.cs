using Microsoft.AspNetCore.Mvc;

namespace EuroLeaguePlayerBuilder.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                case 400:
                    return View("BadRequest");
                default:
                    return View("Error");
            }
        }
    }
}
