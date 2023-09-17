using Microsoft.AspNetCore.Mvc;

namespace _1p_atom_carmanager.backend.api.Controllers
{
    [ApiController]
    [Route("info")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;
        public InfoController(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(nameof(Get))]
        public string Get()
        {
            _logger.LogInformation("Ping");
            return "pong";
        }
    }
}
