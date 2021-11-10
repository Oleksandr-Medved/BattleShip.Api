using Microsoft.AspNetCore.Mvc;

namespace BattleShip.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        public string TestProperty { get; set; } = "Test";


        [HttpGet]
        public string Get()
        {
            return this.TestProperty;
        }
    }
}
