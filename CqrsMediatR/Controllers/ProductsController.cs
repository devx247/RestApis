using AutoMapper;
using CqrsMediatR.Commands;
using CqrsMediatR.Models;
using CqrsMediatR.Notifications;
using CqrsMediatR.Queries;
using CqrsMediatR.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            var productsList = _mapper.Map<IEnumerable<ProductViewModel>>(products); 
            
            return Ok(productsList);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            var productDto = _mapper.Map<ProductViewModel>(product);

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductViewModel? productViewModel)
        {
            if (productViewModel == null) return BadRequest($"{nameof(productViewModel)} is null");

            var product = _mapper.Map<Product>(productViewModel);

            product= await _mediator.Send(new AddProductCommand(product));

            await _mediator.Publish(new ProductAddedNotification(product));

            productViewModel = _mapper.Map<ProductViewModel>(product);

            return CreatedAtRoute("GetProductById", new {id = productViewModel.Id}, productViewModel);
        }
    }
}
