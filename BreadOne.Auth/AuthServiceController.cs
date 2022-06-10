//WebAPI
//JsonSerializer가 내장되어 있음

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreadOne.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthServiceController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<ClaimDto> Get() =>
            HttpContext.User.Claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value });
    }
}
