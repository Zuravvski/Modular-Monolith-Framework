using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zuravvski.Infrastructure.Web
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class HitMeAppController : ControllerBase
    {
    }
}
