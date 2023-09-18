using _1p_atom_carmanager.backend.core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace _1p_atom_carmanager.backend.api.Controllers
{
    [ApiController]
    [Route("info")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;
        private readonly ICarManagerService _carManagerService;
        public InfoController(ILogger<InfoController> logger, ICarManagerService carManagerService)
        {
            _logger = logger;
            _carManagerService = carManagerService;
        }

        [HttpGet(nameof(Get))]
        public string Get()
        {
            _logger.LogInformation("Pong");
            return "Pong";
        }
    }
}
