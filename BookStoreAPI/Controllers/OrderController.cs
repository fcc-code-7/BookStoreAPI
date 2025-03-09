using AutoMapper;
using BookStoreAPI.Models.Domain;
using BookStoreAPI.Models.DTO;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderAsync([FromQuery] string? FilterOn, [FromQuery] string? FilterBy, [FromQuery] string? SortBy, [FromQuery] bool isAscending, [FromQuery] int PageNo = 1, [FromQuery] int PageSize = 1000)
        {

            var orders = await orderRepository.GetAllOrderAsync(FilterOn, FilterBy, SortBy, isAscending, PageNo, PageSize);
            var ordersDto = mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersDto);
        }

        [HttpPost]
        public async Task<IActionResult> Addorder([FromBody] AddRequestOrderDTO addRequest)
        {
            var order = mapper.Map<Order>(addRequest);
            order = await orderRepository.CreateOrderAsync(order);
            var orderDto = mapper.Map<OrderDTO>(order);
            return Ok(orderDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetorderById([FromRoute] Guid id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = mapper.Map<OrderDTO>(order);
            return Ok(orderDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Updateorder([FromRoute] Guid id, [FromBody] UpdateRequestOrderDTO updateRequest)
        {
            var order = mapper.Map<Order>(updateRequest);
            var updatedorder = await orderRepository.UpdateOrder(id, order);
            if (updatedorder == null)
            {
                return NotFound();
            }
            var orderDto = mapper.Map<OrderDTO>(updatedorder);
            return Ok(orderDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deleteorder([FromRoute] Guid id)
        {
            var order = await orderRepository.DeleteOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = mapper.Map<OrderDTO>(order);
            return Ok(orderDto);
        }
    }
}
