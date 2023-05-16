using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
