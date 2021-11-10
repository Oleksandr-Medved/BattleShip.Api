using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
   
    public class TestController : ControllerBase
    {

        public string TestProperty { get; set; } = "Test";


        [HttpGet]
        [Authorize]
        public string Get()
        {
            return this.TestProperty;
        }
    }
}
