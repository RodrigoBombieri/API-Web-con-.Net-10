using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaActivosDigitales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Sistema Activos Digitales API");
        }

        [HttpGet("status")] // La ruta sería api/home/status
        public IActionResult GetStatus()
        {
            return Ok(new { estado = "Online", version = "10.0" });
        }
    }
}
