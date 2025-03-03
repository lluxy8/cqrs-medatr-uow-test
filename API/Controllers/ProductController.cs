using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
