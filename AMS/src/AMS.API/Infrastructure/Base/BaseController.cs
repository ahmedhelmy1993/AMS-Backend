using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AMS.API.Infrastructure.Base
{
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; }
        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
