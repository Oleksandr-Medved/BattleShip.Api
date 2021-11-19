using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        private string TestProperty { get; set; } = "Test";


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            await Task.FromResult(0);
            return Ok(new { property = this.TestProperty });
        }
    }
}
