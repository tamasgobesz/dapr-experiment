using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersApp.Models;
using OrdersApp.Services;

namespace OrdersApp.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger _logger;

        public OrdersController(OrderService orderService, 
            ILogger<OrdersController> logger)
        {
            _orderService = orderService;

            _logger = logger;
        }

        [HttpGet("/orders/{userId}")]
        public async Task<IActionResult> GetUserWithOrders(Guid userId)
        {
            var user = await _orderService.GetUserWithOrders(userId);

            return Ok(user);
        }

        [Topic("orders")]
        [HttpPost("/orders")]
        public async Task CreateOrder(OrderEvent orderEvent)
        {
            await _orderService.CreateOrder(Guid.Parse(orderEvent.UserId), orderEvent.Data);
        }

        [HttpPost("/orders/{userId}")]
        public async Task<IActionResult> CreateOrder(Guid userId, [FromBody]OrderData orderData)
        {
            await _orderService.CreateOrder(userId, orderData.Data);

            return Ok();
        }
    }
}