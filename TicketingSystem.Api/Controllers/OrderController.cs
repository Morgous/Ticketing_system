using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketingService.Application.Services;
using TicketingSystem.Domain.Enums;
using TicketingService.Application.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace TicketingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public OrderController(OrderService orderService, IMapper mapper, IDistributedCache cache)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto newOrderDTO)
        {
            var newOrder = _mapper.Map<Order>(newOrderDTO);
            newOrder.Id = Guid.NewGuid();
            newOrder.Status = OrderStatus.Pending;
            newOrder.CreatedAt = DateTime.UtcNow;

            await _orderService.AddOrderAsync(newOrder);
            await _orderService.InvalidateEventCache(newOrder.EventId);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDto updatedOrderDTO)
        {
            if (id != updatedOrderDTO.Id)
            {
                return BadRequest("Order id mismatch.");
            }

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedOrderDTO, order);

            await _orderService.UpdateOrderAsync(order);
            await _orderService.InvalidateEventCache(order.EventId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("carts/{cartId}")]
        public async Task<IActionResult> GetCartItems(Guid cartId)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var cartItems = orders.Where(o => o.CartId == cartId);
            return Ok(cartItems);
        }

        [HttpPost("carts/{cartId}")]
        public async Task<IActionResult> AddToCart(string cartId, [FromBody] CreateOrderDto newOrderDTO)
        {
            try
            {
                if (!Guid.TryParse(cartId, out Guid id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new { message = "CartId should be Guid" });
                }

                var newOrder = _mapper.Map<Order>(newOrderDTO);
                newOrder.Id = Guid.NewGuid();
                newOrder.CartId = Guid.Parse(cartId);
                newOrder.Status = OrderStatus.Pending;
                newOrder.CreatedAt = DateTime.UtcNow;

                await _orderService.AddOrderAsync(newOrder);
                await _orderService.InvalidateEventCache(newOrder.EventId);
                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("carts/{cartId}/events/{eventId}/seats/{seatId}")]
        public async Task<IActionResult> RemoveFromCart(Guid cartId, Guid eventId, Guid seatId)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var orderToRemove = orders.FirstOrDefault(o => o.CartId == cartId && o.EventId == eventId && o.SeatId == seatId);
            if (orderToRemove != null)
            {
                await _orderService.DeleteOrderAsync(orderToRemove.Id);
                await _orderService.InvalidateEventCache(orderToRemove.EventId);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("carts/{cartId}/book")]
        public async Task<IActionResult> BookCart(Guid cartId)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var cartItems = orders.Where(o => o.CartId == cartId).ToList();

            if (!cartItems.Any())
            {
                return NotFound(new { Message = "Cart not found or empty." });
            }

            var paymentId = Guid.NewGuid();

            foreach (var order in cartItems)
            {
                order.Status = OrderStatus.Booked;
                order.PaymentId = paymentId;
                await _orderService.UpdateOrderAsync(order);
                await _orderService.InvalidateEventCache(order.EventId);
            }

            return Ok(paymentId);
        }

        [HttpPost("carts/{cartId}/pessimistic")]
        public async Task<IActionResult> AddToCartPessimistic(Guid cartId, [FromBody] CreateOrderDto newOrderDTO)
        {
            var newOrder = _mapper.Map<Order>(newOrderDTO);
            newOrder.Id = Guid.NewGuid();
            newOrder.CartId = cartId;
            newOrder.Status = OrderStatus.Booked;
            newOrder.CreatedAt = DateTime.UtcNow;

            var seatBooked = await _orderService.TryBookSeatPessimisticAsync(newOrder.EventId, newOrder.SeatId);
            if (!seatBooked)
            {
                return BadRequest("Seat is already booked or sold.");
            }

            await _orderService.AddOrderAsync(newOrder);
            await _orderService.InvalidateEventCache(newOrder.EventId);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPost("carts/{cartId}/optimistic")]
        public async Task<IActionResult> AddToCartOptimistic(Guid cartId, [FromBody] CreateOrderDto newOrderDTO)
        {
            var newOrder = _mapper.Map<Order>(newOrderDTO);
            newOrder.Id = Guid.NewGuid();
            newOrder.CartId = cartId;
            newOrder.Status = OrderStatus.Pending;
            newOrder.CreatedAt = DateTime.UtcNow;

            var seatBooked = await _orderService.TryBookSeatOptimisticAsync(newOrder.EventId, newOrder.SeatId,newOrder.Version);
            if (!seatBooked)
            {
                return Conflict("Concurrency conflict occurred.");
            }

            await _orderService.AddOrderAsync(newOrder);
            await _orderService.InvalidateEventCache(newOrder.EventId);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }
    }
}